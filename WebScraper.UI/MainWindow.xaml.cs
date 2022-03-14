using System.Linq;
using System.Net;
using System.Windows;
using WebScraper.Logic;

namespace WebScraper.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private readonly IGoogleRanker _googleRanker;
        private readonly GoogleRankingViewModel _viewModel;

        public MainWindow(IGoogleRanker googleRanker)
        {
            _googleRanker = googleRanker;

            _viewModel = new GoogleRankingViewModel();
            DataContext = _viewModel;

            InitializeComponent();
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

  

        private void ChangeRankingMessage_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RankingMessage = "La de da look at me!";
        }

        private async void GetRankings_Click(object sender, RoutedEventArgs e)
        {
            var rankings = await _googleRanker.GetRankingsAsync();
            if (rankings.Any())
            {
                _viewModel.RankingMessage = $"Results found at positions: {string.Join(',', rankings)}";
            } else
            {
                _viewModel.RankingMessage = "No results in the top 100";
            }
        }

        // https://www.codeguru.com/dotnet/managing-non-blocking-calls-on-the-ui-thread-with-async-await/
        // According to this, calling just await is good for IO heavy asynchronous tasks
    }
}
