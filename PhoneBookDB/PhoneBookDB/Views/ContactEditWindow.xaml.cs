using PhoneBookDB.ViewModels;
using System.Windows;

namespace PhoneBookDB.Views
{
    public partial class ContactEditWindow : Window
    {
        public ContactEditWindow(ContactEditViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Owner = Application.Current.MainWindow;
        }
    }
}