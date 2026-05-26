using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PhoneBookDB.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PhoneBookDB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            
            services.AddDbContext<PhoneBookDbKupriyanov2307a1Context>(options =>
                options.UseSqlServer("Data Source=DBSrv\\gor2025;Initial Catalog=PhoneBookDB_Kupriyanov_2307a1;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

            services.AddTransient<MainViewModel>();

            services.AddTransient<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }

}
