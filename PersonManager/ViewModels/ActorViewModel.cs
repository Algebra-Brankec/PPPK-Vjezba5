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
    public class ActorViewModel
    {
        public ObservableCollection<Actor> Actor { get; }
        public ActorViewModel(int movieId)
        {
            Actor = new ObservableCollection<Actor>(RepositoryFactory.GetRepository().GetActors(movieId));
            Actor.CollectionChanged += Movie_CollectionChanged;
        }

        private void Movie_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddActor(Actor[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteActor(e.OldItems.OfType<Actor>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateActor(e.NewItems.OfType<Actor>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Actor actor) => Actor[Actor.IndexOf(actor)] = actor;
    }
}
