using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModifaedPhoneBook.Services
{
    public interface IDialogService
    {
        /// Показать информационное сообщение
        void ShowInfo(string message, string title = "Информация");

        /// <summary>
        /// Показать предупреждение
        void ShowWarning(string message, string title = "Предупреждение");
        /// Показать сообщение об ошибке
        void ShowError(string message, string title = "Ошибка");

        /// Запросить подтверждение действия (Да/Нет)
        /// <returns>True, если пользователь нажал "Да"</returns>
        bool ShowConfirmation(string message, string title = "Подтверждение");
    }
}
