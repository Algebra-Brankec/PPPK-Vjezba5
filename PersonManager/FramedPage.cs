using System.Windows.Controls;
using Zadatak.ViewModels;

namespace Zadatak
{

    public class FramedPage : Page
    {
        public FramedPage(MovieViewModel movieViewModel, int? movieId = null)
        {
            MovieViewModel = movieViewModel;
            ActorViewModel = movieId != null ? new ActorViewModel((int)movieId) : null;
            DirectorViewModel = movieId != null ? new DirectorViewModel((int)movieId) : null;
        }
        public MovieViewModel MovieViewModel { get; }
        public ActorViewModel ActorViewModel { get; }
        public DirectorViewModel DirectorViewModel { get; }
        public Frame Frame { get; set; }
    }
}
