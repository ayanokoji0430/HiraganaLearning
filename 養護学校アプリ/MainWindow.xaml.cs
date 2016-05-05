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

namespace 養護学校アプリ
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            _navi = this.myFrame.NavigationService;

        }
        private NavigationService _navi;

        private void myFrame_Loaded(object sender, RoutedEventArgs e)
        {
            _navi.Navigate(new Uri("GamePAge.xaml", UriKind.Relative));
        }
    }
}
