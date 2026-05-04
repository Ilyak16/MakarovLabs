using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PhoneBook.ViewModels;

namespace PhoneBook.Models
{
    public class Contact : ObservableObject
    {
        private string _name = string.Empty;
        private string _phone = string.Empty;

        public Contact(string name, string phone)
        {
            _name = name;
            _phone = phone;

            if (!Validate())
                throw new ArgumentException("Некорректные данные контакта");
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            // Форматы: +7XXXXXXXXXX или 10-11 цифр
            var regex = new Regex(@"^(\+7\d{10}|\d{10,11})$");
            return regex.IsMatch(Phone);
        }
    }
}
