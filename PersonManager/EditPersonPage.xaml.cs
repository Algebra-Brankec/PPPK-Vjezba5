using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zadatak.Models;
using Zadatak.Utils;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditMoviePage : FramedPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private readonly Movie movie;
        public EditMoviePage(MovieViewModel movieViewModel, Movie movie = null) : base(movieViewModel)
        {
            InitializeComponent();
            this.movie = movie ?? new Movie();
            DataContext = movie;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                movie.Name = TbFirstName.Text.Trim();
                movie.Description = TbLastName.Text.Trim();
                movie.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
                if (movie.IDMovie == 0)
                {
                    MovieViewModel.Movie.Add(movie);
                }
                else
                {
                    MovieViewModel.Update(movie);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            GridContainter.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim()))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                } else
                {
                    e.Background = Brushes.White;
                }
            });
            if (Picture.Source == null)
            {
                PictureBorder.BorderBrush = Brushes.LightCoral;
                valid = false;
            } 
            else
            {
                PictureBorder.BorderBrush = Brushes.WhiteSmoke;
            }
            return valid;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Picture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void BtnActors_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new EditActorsPage(MovieViewModel, movie) { Frame = Frame });
        }

        private void BtnDirectors_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new EditDirectorsPage(MovieViewModel, movie) { Frame = Frame });
        }
    }
}
