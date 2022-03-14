using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class MainWindow : Window
    {
        // TODO: make this its own GoogleRanker window or something??
        private readonly IGoogleRanker _googleRanker;

        public MainWindow(IGoogleRanker googleRanker)
        {
            InitializeComponent();
            _googleRanker = googleRanker;
        }

        // TODO: make this async
        private void HtmlParserTest_Click(object sender, RoutedEventArgs e)
        {
            // TODO: call out to business logic..?
            Console.WriteLine("Button Clicked!");
             var res = _googleRanker.GetRankings();
        }
    }
}
