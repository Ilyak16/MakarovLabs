using ModifaedPhoneBook.Models;
using ModifaedPhoneBook.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModifaedPhoneBook.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        // Зависимость от сервиса диалогов (внедряется через конструктор)
        private readonly IDialogService _dialogService;

        public ObservableCollection<Contact> Contacts { get; }

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

        /// <summary>
        /// Constructor Injection: DI-контейнер автоматически передаёт 
        /// реализацию IDialogService при создании ViewModel.
        /// Lifetime ViewModel: Transient (новый экземпляр при каждом запросе).
        /// </summary>
        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ??
                throw new ArgumentNullException(nameof(dialogService));

            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand(DeleteContact, CanDeleteContact);
        }

        private void AddContact()
        {
            try
            {
                // Проверка на дубликат по номеру телефона
                if (Contacts.Any(c => c.Phone == Phone))
                {
                    _dialogService.ShowWarning(
                        "Контакт с таким номером телефона уже существует!",
                        "Дубликат контакта");
                    return; // Отменяем добавление
                }

                var contact = new Contact(Name, Phone);
                Contacts.Add(contact);

                // Очистка полей ввода
                Name = string.Empty;
                Phone = string.Empty;

                // Информационное сообщение об успехе
                _dialogService.ShowInfo(
                    $"Контакт \"{contact.Name}\" успешно добавлен!",
                    "Добавление контакта");
            }
            catch (ArgumentException ex)
            {
                _dialogService.ShowError(ex.Message, "Ошибка валидации");
            }
            catch (Exception ex)
            {
                _dialogService.ShowError($"Не удалось добавить контакт: {ex.Message}", "Ошибка");
            }
        }

        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);
        }

        private void DeleteContact()
        {
            if (SelectedContact != null)
            {
                // Запрос подтверждения удаления
                bool confirmed = _dialogService.ShowConfirmation(
                    $"Вы действительно хотите удалить контакт \"{SelectedContact.Name}\"?",
                    "Подтверждение удаления");

                if (confirmed)
                {
                    Contacts.Remove(SelectedContact);
                }
                // Если пользователь отказался — контакт не удаляется
            }
        }

        private bool CanDeleteContact()
        {
            return SelectedContact != null;
        }
    }
}
