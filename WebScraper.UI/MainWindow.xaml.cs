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

        private async void GetRankings_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RankingMessage = "Fetching results...";
            var rankings = await _googleRanker.GetRankingsAsync();
            _viewModel.RankingMessage = rankings.Any() ?
                $"Result(s) found at position(s): {string.Join(',', rankings)}"
                : "No results in the top 100";
        }
    }
}
