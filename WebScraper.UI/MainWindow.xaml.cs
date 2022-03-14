using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebScraper.Logic;

namespace WebScraper.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class ViewModel : ViewModelBase
    {

        private string _rankingMessage;

        public string RankingMessage
        {
            get { return _rankingMessage; }
            set { SetProperty(ref _rankingMessage, value); }
        }

        // TODO: now _maybe_ from here, we click a button which then gets the result and sets rankingMessage
        // In which case, this ViewModel will reflect the change in the UI!


    }

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
    }


    public partial class MainWindow : Window
    {
        // TODO: make this its own GoogleRanker window or something??
        private readonly IGoogleRanker _googleRanker;

        private readonly ViewModel _viewModel;

        public MainWindow(IGoogleRanker googleRanker)
        {
            _googleRanker = googleRanker;

            _viewModel = new ViewModel();
            _viewModel.RankingMessage = "Test Ranking Message";
            DataContext = _viewModel;

            InitializeComponent();

            _viewModel.RankingMessage = "Mark";

        }

        // TODO: make this async
        private void HtmlParserTest_Click(object sender, RoutedEventArgs e)
        {
            // TODO: remove old code path
        }

        // I'm unsure if this is the magic i wanted it to be??
        // I feel like this still might be running on the same ui context thread??
        // In which case, do i have to call Task.Run(() => _googleRanker.GetRankingsAsync()) to somehow free up teh ui thread??
        private async void HtmlParserAsyncTest_Click(object sender, RoutedEventArgs e)
        {
            // Right so with await will "do stuff with the calling context UI thread in the meantime _until_ res is returned, in which the the result will be put back onto the calling context UI thread and that will resume...
            // So this kind of _is_ what I want to do??
            // TODO: does async work like I _think_ it does....?
            // How do we ever end up with the actual result of the task..?
            var res = await _googleRanker.GetRankingsAsync();
        }

        private async void GetAllRankingsTest_Click(object sender, RoutedEventArgs e)
        {
            //var res = await _googleRanker.GetAllRankingsAsync();
        }

        private void DownloadFreshHtml_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://www.google.com.au/search?num=100&q=conveyancing+software";
            var filePath = @"C:\Source\WebScraper\WebScraper\DownloadedFiles\FreshGoogleDownload.html";

            string htmlDoc = "";
            using (WebClient webClient = new WebClient())
            {
                // FUCK - how am I going to create an elgant serializer class lol?
                webClient.DownloadFile(url, filePath);
                //File.WriteAllText("Duh_test_html.txt", htmlDoc);
                var stop = true;
            }
        }

        private void ChangeRankingMessage_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RankingMessage = "La de da look at me!";
        }

        // https://www.codeguru.com/dotnet/managing-non-blocking-calls-on-the-ui-thread-with-async-await/
        // According to this, calling just await is good for IO heavy asynchronous tasks
    }
}
