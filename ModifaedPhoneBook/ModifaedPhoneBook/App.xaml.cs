using Microsoft.Extensions.DependencyInjection;
using ModifaedPhoneBook.Services;
using ModifaedPhoneBook.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ModifaedPhoneBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 1. Создаём коллекцию для регистрации сервисов
            var services = new ServiceCollection();

            // 2. Регистрация сервисов с указанием Lifetime:

            // DialogService — Singleton:
            // Один экземпляр на всё время работы приложения.
            // Сервис не хранит состояние пользователя, поэтому безопасен для повторного использования.
            services.AddSingleton<IDialogService, DialogService>();

            // MainViewModel — Transient:
            // Новый экземпляр при каждом запросе.
            // Рекомендуется для ViewModel, чтобы избежать утечек памяти 
            // и обеспечить чистое состояние при навигации.
            services.AddTransient<MainViewModel>();

            // MainWindow — Singleton с ручной настройкой DataContext:
            // WPF требует, чтобы окно создавалось через параметрический конструктор (для InitializeComponent).
            // Лямбда-выражение позволяет создать стандартное окно и внедрить ViewModel из контейнера.
            services.AddSingleton<MainWindow>(serviceProvider =>
            {
                var window = new MainWindow();
                window.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
                return window;
            });

            // 3. Создаём контейнер зависимостей (ServiceProvider)
            var serviceProvider = services.BuildServiceProvider();

            // 4. Получаем главное окно и отображаем его
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
