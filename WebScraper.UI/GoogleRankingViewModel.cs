namespace WebScraper.UI
{
    public class GoogleRankingViewModel : ViewModelBase
    {

        private string _rankingMessage;

        public string RankingMessage
        {
            get { return _rankingMessage; }
            set { SetProperty(ref _rankingMessage, value); }
        }
    }
}
