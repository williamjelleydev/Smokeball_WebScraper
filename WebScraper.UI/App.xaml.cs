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
            // TODO: figure out if should be singletons or scoped??

            // TODO: set up dependencies of GoogleRanker

            // TODO: confirm that logging actually works as expected...
            services.AddLogging(configure => configure.AddConsole());




            services.AddSingleton<ITagFactory, TagFactory>();
            services.AddSingleton<IHtmlParser, HtmlParser>();
            services.AddSingleton<IHtmlDownloader, HtmlDownloader>();

            // GoogleRanker Business Logic
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
