using PhoneBookDB.Models;
using PhoneBookDB.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace PhoneBookDB.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IDbContextFactory<PhoneBookDbKupriyanov2307a1Context> _contextFactory;
        public ObservableCollection<Contact> Contacts { get; set; }

        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand NormalizeAllCommand { get; }

        public MainViewModel(IDbContextFactory<PhoneBookDbKupriyanov2307a1Context> contextFactory)
        {
            _contextFactory = contextFactory;
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(OpenAddContactWindow, () => true);
            EditCommand = new RelayCommand(OpenEditContactWindow, CanEditContact);
            DeleteCommand = new RelayCommand(DeleteContact, CanDeleteContact);
            RefreshCommand = new RelayCommand(() => Task.Run(async () => await LoadContactsAsync()), () => true);
            NormalizeAllCommand = new RelayCommand(async () => await NormalizeAllPhonesAsync(), () => true);

            Task.Run(async () => await LoadContactsAsync());
        }

        // Чтение из БД с использованием локального контекста
        private async Task LoadContactsAsync()
        {
            try
            {
                using (var context = await _contextFactory.CreateDbContextAsync())
                {
                    // Ас NoTracking использовать не обязательно, так как контекст сразу уничтожается, 
                    // но это увеличивает производительность чтения.
                    var contacts = await context.Contacts.AsNoTracking().ToListAsync();

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Contacts.Clear();
                        foreach (var contact in contacts)
                            Contacts.Add(contact);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenAddContactWindow()
        {
            var viewModel = new ContactEditViewModel(_contextFactory, onContactSaved: RefreshContacts);
            var window = new ContactEditWindow(viewModel);
            window.ShowDialog();
        }

        private void OpenEditContactWindow()
        {
            if (SelectedContact == null)
            {
                MessageBox.Show("Выберите контакт для редактирования", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var viewModel = new ContactEditViewModel(_contextFactory, SelectedContact, RefreshContacts);
            var window = new ContactEditWindow(viewModel);
            window.ShowDialog();
        }

        // Удаление сущности в новом контексте по первичному ключу ID
        private async void DeleteContact()
        {
            if (SelectedContact == null)
            {
                MessageBox.Show("Выберите контакт для удаления", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Удалить контакт \"{SelectedContact.Name}\"?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                int idToDelete = SelectedContact.Id;

                using (var context = await _contextFactory.CreateDbContextAsync())
                {
                    var contactToDelete = await context.Contacts.FindAsync(idToDelete);
                    if (contactToDelete != null)
                    {
                        context.Contacts.Remove(contactToDelete);
                        await context.SaveChangesAsync();
                    }
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    var item = Contacts.FirstOrDefault(c => c.Id == idToDelete);
                    if (item != null) Contacts.Remove(item);
                    SelectedContact = null;
                });

                MessageBox.Show("✅ Контакт удалён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Нормализация всех записей через локальный контекст
        private async Task NormalizeAllPhonesAsync()
        {
            try
            {
                using (var context = await _contextFactory.CreateDbContextAsync())
                {
                    var contacts = await context.Contacts.ToListAsync();
                    int updated = 0;

                    foreach (var contact in contacts)
                    {
                        string normalized = Contact.NormalizePhone(contact.Phone);
                        if (contact.Phone != normalized)
                        {
                            contact.Phone = normalized;
                            updated++;
                        }
                    }

                    if (updated > 0)
                    {
                        await context.SaveChangesAsync();
                        MessageBox.Show($"✅ Обновлено номеров: {updated}", "Нормализация", MessageBoxButton.OK, MessageBoxImage.Information);
                        await LoadContactsAsync();
                    }
                    else
                    {
                        MessageBox.Show("Все номера уже в едином формате", "Нормализация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshContacts()
        {
            Task.Run(async () => await LoadContactsAsync());
        }

        private bool CanEditContact() => SelectedContact != null;
        private bool CanDeleteContact() => SelectedContact != null;
    }
}
