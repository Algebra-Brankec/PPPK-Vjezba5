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
    public class DirectorViewModel
    {
        public ObservableCollection<Director> Director { get; }
        public DirectorViewModel(int movieId)
        {
            Director = new ObservableCollection<Director>(RepositoryFactory.GetRepository().GetDirectors(movieId));
            Director.CollectionChanged += Movie_CollectionChanged;
        }

        private void Movie_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddDirector(Director[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteDirector(e.OldItems.OfType<Director>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateDirector(e.NewItems.OfType<Director>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Director director) => Director[Director.IndexOf(director)] = director;
    }
}
