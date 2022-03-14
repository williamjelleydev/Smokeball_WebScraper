using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;
using WebScraper.Logic;
using WebScraper.Logic.HtmlParsers;

namespace WebScraper.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole());

            services.AddSingleton<IValidTagParser, ValidTagParser>();
            services.AddSingleton<IHtmlParser, DivAndAnchorFlattenedHtmlParser>();
            services.AddSingleton<IHtmlDownloader, HtmlDownloader>();

            services.AddSingleton<IGoogleRankerConfig, GoogleRankerDefaultConfig>();
            services.AddSingleton<IGoogleRanker, GoogleRanker>();

            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
