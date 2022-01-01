using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditDirectorsPage.xaml
    /// </summary>
    public partial class EditDirectorsPage : FramedPage
    {
        private readonly Movie movie;
        private Director selectedDirector;

        public EditDirectorsPage(MovieViewModel movieViewModel, Movie movie = null) : base(movieViewModel, movie.IDMovie)
        {
            InitializeComponent();
            this.movie = movie ?? new Movie();
            DataContext = movie;
            selectedDirector = new Director();
            Init();
        }

        private void Init()
        {
            InitData();
            InitList();
            InitTextBoxes();
        }

        private void InitTextBoxes()
        {
            TbDirectorName.Text = "";
        }

        private void InitData()
        {

        }

        private void InitList()
        {
            lvDirectors.ItemsSource = DirectorViewModel.Director;
            lvDirectors.Items.Refresh();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                var director = new Director();
                director.Name = TbDirectorName.Text.Trim();
                director.MovieID = movie.IDMovie;
                DirectorViewModel.Director.Add(director);

                TbDirectorName.Text = "";
                InitList();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDirector == null)
            {
                return;
            }

            if (FormValid())
            {
                selectedDirector.Name = TbDirectorName.Text.Trim();
                selectedDirector.MovieID = movie.IDMovie;
                DirectorViewModel.Update(selectedDirector);

                TbDirectorName.Text = "";
                InitList();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDirector == null)
            {
                return;
            }

            DirectorViewModel.Director.Remove(selectedDirector);

            TbDirectorName.Text = "";
            InitList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.GoBack();
        }

        private void lvDirectors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDirector = lvDirectors.SelectedItem as Director;
            TbDirectorName.Text = selectedDirector?.Name ?? "";
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
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });
            return valid;
        }
    }
}
