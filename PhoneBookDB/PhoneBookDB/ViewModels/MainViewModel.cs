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
        private readonly PhoneBookDbKupriyanov2307a1Context _context;
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

        public MainViewModel(PhoneBookDbKupriyanov2307a1Context context)
        {
            _context = context;
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(OpenAddContactWindow, () => true);
            EditCommand = new RelayCommand(OpenEditContactWindow, CanEditContact);
            DeleteCommand = new RelayCommand(DeleteContact, CanDeleteContact);
            RefreshCommand = new RelayCommand(() => Task.Run(async () => await LoadContactsAsync()), () => true);
            NormalizeAllCommand = new RelayCommand(async () => await NormalizeAllPhonesAsync(), () => true);

            Task.Run(async () => await LoadContactsAsync());
        }

        private async Task LoadContactsAsync()
        {
            try
            {
                var contacts = await _context.Contacts.ToListAsync();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Contacts.Clear();
                    foreach (var contact in contacts)
                        Contacts.Add(contact);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка загрузки: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenAddContactWindow()
        {
            var viewModel = new ContactEditViewModel(_context, onContactSaved: RefreshContacts);
            var window = new ContactEditWindow(viewModel);
            window.ShowDialog();
        }

        private void OpenEditContactWindow()
        {
            if (SelectedContact == null)
            {
                MessageBox.Show("Выберите контакт для редактирования", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var viewModel = new ContactEditViewModel(_context, SelectedContact, RefreshContacts);
            var window = new ContactEditWindow(viewModel);
            window.ShowDialog();
        }

        private async void DeleteContact()
        {
            if (SelectedContact == null)
            {
                MessageBox.Show("Выберите контакт для удаления", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
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
                var contactToDelete = SelectedContact;

                _context.Contacts.Remove(contactToDelete);
                await _context.SaveChangesAsync();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Contacts.Remove(contactToDelete);
                    SelectedContact = null;
                });

                MessageBox.Show("✅ Контакт удалён!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка удаления: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Нормализует все телефоны в базе данных до единого формата
        /// </summary>
        private async Task NormalizeAllPhonesAsync()
        {
            try
            {
                var contacts = await _context.Contacts.ToListAsync();
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
                    await _context.SaveChangesAsync();
                    MessageBox.Show($"✅ Обновлено номеров: {updated}", "Нормализация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadContactsAsync();
                }
                else
                {
                    MessageBox.Show("️ Все номера уже в едином формате", "Нормализация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
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