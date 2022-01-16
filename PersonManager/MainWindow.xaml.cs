using System.Windows;
using System.Windows.Controls;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Frame.Navigate(new ListMoviePage(new ViewModels.MovieViewModel()) { Frame = Frame });
        }
    }
}
