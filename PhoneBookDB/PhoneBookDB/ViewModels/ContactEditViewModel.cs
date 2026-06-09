using PhoneBookDB.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PhoneBookDB.ViewModels
{
    public class ContactEditViewModel : ObservableObject
    {
        private readonly IDbContextFactory<PhoneBookDbKupriyanov2307a1Context> _contextFactory;
        private readonly int _contactId; // Сохраняем только ID, а не саму отслеживаемую сущность
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
            IDbContextFactory<PhoneBookDbKupriyanov2307a1Context> contextFactory,
            Contact? contact = null,
            Action? onContactSaved = null)
        {
            _contextFactory = contextFactory;
            _isEditMode = contact != null;
            _onContactSaved = onContactSaved;

            if (_isEditMode && contact != null)
            {
                _contactId = contact.Id;
                NameInput = contact.Name;
                PhoneInput = contact.Phone;
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

                // Создаем изолированный контекст для выполнения операции сохранения
                using (var context = await _contextFactory.CreateDbContextAsync())
                {
                    if (_isEditMode)
                    {
                        // Паттерн FETCH-MODIFY-SAVE
                        var dbContact = await context.Contacts.FindAsync(_contactId);
                        if (dbContact == null)
                        {
                            MessageBox.Show("❌ Контакт не найден в базе данных.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        dbContact.Name = NameInput.Trim();
                        dbContact.Phone = PhoneInput.Trim();

                        await context.SaveChangesAsync();
                        MessageBox.Show("✅ Контакт обновлён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // ДОБАВЛЕНИЕ (Create) через локальный контекст
                        var newContact = new Contact(NameInput.Trim(), PhoneInput.Trim());
                        context.Contacts.Add(newContact);

                        await context.SaveChangesAsync();
                        MessageBox.Show("✅ Контакт добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                _onContactSaved?.Invoke();
                CloseWindow();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"⚠️ {ex.Message}", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"❌ Ошибка БД: {ex.InnerException?.Message ?? ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(NameInput))
            {
                MessageBox.Show("Введите имя контакта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            var digits = new string(PhoneInput?.Where(char.IsDigit).ToArray() ?? Array.Empty<char>());
            if (digits.Length < 10 || digits.Length > 11)
            {
                MessageBox.Show("Введите корректный номер телефона (10-11 цифр)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private bool CanSaveContact() => !string.IsNullOrWhiteSpace(NameInput) && !string.IsNullOrWhiteSpace(PhoneInput);

        private void CancelEditing() => CloseWindow();

        private void CloseWindow()
        {
            if (Application.Current.Windows.Count > 0)
            {
                var window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.DataContext == this);
                window?.Close();
            }
        }
    }
}
