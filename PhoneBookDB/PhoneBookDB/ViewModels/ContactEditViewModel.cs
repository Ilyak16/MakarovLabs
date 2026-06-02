using PhoneBookDB.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace PhoneBookDB.ViewModels
{
    public class ContactEditViewModel : ObservableObject
    {
        private readonly PhoneBookDbKupriyanov2307a1Context _context;
        private readonly Contact? _contact;
        private readonly bool _isEditMode;
        private readonly Action? _onContactSaved;

        private string _nameInput = string.Empty;
        public string NameInput
        {
            get => _nameInput;
            set => Set(ref _nameInput, value);
        }

        private string _phoneInput = string.Empty;
        public string PhoneInput
        {
            get => _phoneInput;
            set => Set(ref _phoneInput, value);
        }

        public string Title => _isEditMode ? "Редактирование контакта" : "Добавление контакта";

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ContactEditViewModel(
            PhoneBookDbKupriyanov2307a1Context context,
            Contact? contact = null,
            Action? onContactSaved = null)
        {
            _context = context;
            _contact = contact;
            _isEditMode = contact != null;
            _onContactSaved = onContactSaved;

            if (_isEditMode && _contact != null)
            {
                NameInput = _contact.Name;
                PhoneInput = _contact.Phone;
            }

            SaveCommand = new RelayCommand(SaveContact, CanSaveContact);
            CancelCommand = new RelayCommand(CancelEditing, () => true);
        }

        private async void SaveContact()
        {
            try
            {
                if (!ValidateInput())
                    return;
                string normalizedPhone = Contact.NormalizePhone(PhoneInput);

                if (_isEditMode && _contact != null)
                {
                    // РЕДАКТИРОВАНИЕ (Update)
                    _contact.Name = NameInput.Trim();
                    _contact.Phone = PhoneInput.Trim();

                    _context.Contacts.Update(_contact);
                    await _context.SaveChangesAsync();

                    MessageBox.Show("✅ Контакт обновлён!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // ДОБАВЛЕНИЕ (Create)
                    var newContact = new Contact(NameInput.Trim(), PhoneInput.Trim());

                    _context.Contacts.Add(newContact);
                    await _context.SaveChangesAsync();

                    MessageBox.Show("✅ Контакт добавлен!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }

                _onContactSaved?.Invoke();

                // Закрытие окна
                if (Application.Current.Windows.Count > 0)
                {
                    var window = Application.Current.Windows.Cast<Window>()
                        .FirstOrDefault(w => w.DataContext == this);
                    window?.Close();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"⚠️ {ex.Message}", "Ошибка валидации",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"❌ Ошибка БД: {ex.InnerException?.Message ?? ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка сохранения: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(NameInput))
            {
                MessageBox.Show("Введите имя контакта", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            var digits = new string(PhoneInput?.Where(char.IsDigit).ToArray() ?? Array.Empty<char>());
            if (digits.Length < 10 || digits.Length > 11)
            {
                MessageBox.Show("Введите корректный номер телефона (10-11 цифр)",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool CanSaveContact()
        {
            return !string.IsNullOrWhiteSpace(NameInput) &&
                   !string.IsNullOrWhiteSpace(PhoneInput);
        }

        private void CancelEditing()
        {
            var window = Application.Current.Windows.Cast<Window>()
                .FirstOrDefault(w => w.DataContext == this);
            window?.Close();
        }
    }
}