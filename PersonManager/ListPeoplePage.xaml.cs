using System.Windows;
using System.Windows.Controls;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for ListMoviesPage.xaml
    /// </summary>
    public partial class ListPeoplePage : FramedPage
    {
        public ListPeoplePage(MovieViewModel movieViewModel) : base(movieViewModel)
        {
            InitializeComponent();
            LvUsers.ItemsSource = movieViewModel.Movie;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditMoviePage(MovieViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvUsers.SelectedItem != null)
            {
                Frame.Navigate(new EditMoviePage(MovieViewModel, LvUsers.SelectedItem as Movie) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvUsers.SelectedItem != null)
            {
                MovieViewModel.Movie.Remove(LvUsers.SelectedItem as Movie);
            }
        }
    }
}
