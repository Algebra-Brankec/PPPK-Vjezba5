using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak.Dal;
using Zadatak.Models;

namespace Zadatak.ViewModels
{
    public class MovieViewModel
    {
        public ObservableCollection<Movie> Movie { get; }
        public MovieViewModel()
        {
            Movie = new ObservableCollection<Movie>(RepositoryFactory.GetRepository().GetMovies());
            Movie.CollectionChanged += Movie_CollectionChanged;
        }

        private void Movie_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddMovie(Movie[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteMovie(e.OldItems.OfType<Movie>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateMovie(e.NewItems.OfType<Movie>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Movie movie) => Movie[Movie.IndexOf(movie)] = movie;
    }
}
