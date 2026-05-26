using PhoneBookDB.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace PhoneBookDB.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly PhoneBookDbKupriyanov2307a1Context _context;
        public ObservableCollection<Contact> Contacts { get; set; }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public MainViewModel(PhoneBookDbKupriyanov2307a1Context context)
        {
            _context = context;
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand(DeleteContact, CanDeleteContact);
            RefreshCommand = new RelayCommand(async () => await LoadContactsAsync(), () => true);

            // Загрузка данных при создании ViewModel
            Task.Run(async () => await LoadContactsAsync());
        }

        private async Task LoadContactsAsync()
        {
            try
            {
                var contacts = await _context.Contacts.ToListAsync();

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    Contacts.Clear();
                    foreach (var contact in contacts)
                    {
                        Contacts.Add(contact);
                    }
                });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private async void AddContact()
        {
            try
            {
                var contact = new Contact(Name, Phone);
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                Contacts.Add(contact);

                Name = string.Empty;
                Phone = string.Empty;
            }
            catch (ArgumentException ex)
            {
                System.Windows.MessageBox.Show($"Ошибка валидации: {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка добавления: {ex.Message}");
            }
        }

        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Phone);
        }

        private async void DeleteContact()
        {
            if (SelectedContact != null)
            {
                try
                {
                    _context.Contacts.Remove(SelectedContact);
                    await _context.SaveChangesAsync();

                    Contacts.Remove(SelectedContact);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Ошибка удаления: {ex.Message}");
                }
            }
        }

        private bool CanDeleteContact()
        {
            return SelectedContact != null;
        }
    }
}