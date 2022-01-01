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
    /// Interaction logic for EditActorsPage.xaml
    /// </summary>
    public partial class EditActorsPage : FramedPage
    {
        private readonly Movie movie;
        private Actor selectedActor;

        public EditActorsPage(MovieViewModel movieViewModel, Movie movie = null) : base(movieViewModel, movie.IDMovie)
        {
            InitializeComponent();
            this.movie = movie ?? new Movie();
            DataContext = movie;
            selectedActor = new Actor();
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
            TbActorName.Text = "";
        }

        private void InitData()
        {
            
        }

        private void InitList()
        {
            lvActors.ItemsSource = ActorViewModel.Actor;
            lvActors.Items.Refresh();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                var actor = new Actor();
                actor.Name = TbActorName.Text.Trim();
                actor.MovieID = movie.IDMovie;
                ActorViewModel.Actor.Add(actor);

                TbActorName.Text = "";
                InitList();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedActor == null)
            {
                return;
            }

            if (FormValid())
            {
                selectedActor.Name = TbActorName.Text.Trim();
                selectedActor.MovieID = movie.IDMovie;
                ActorViewModel.Update(selectedActor);

                TbActorName.Text = "";
                InitList();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedActor == null)
            {
                return;
            }

            ActorViewModel.Actor.Remove(selectedActor);

            TbActorName.Text = "";
            InitList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.GoBack();
        }

        private void lvActors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedActor = lvActors.SelectedItem as Actor;
            TbActorName.Text = selectedActor?.Name ?? "";
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
