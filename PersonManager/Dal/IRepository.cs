using System.Collections.Generic;
using Zadatak.Models;

namespace Zadatak.Dal
{
    public interface IRepository
    {
        void AddMovie(Movie movie);
        void DeleteMovie(Movie movie);
        IList<Movie> GetMovies();
        Movie GetMovie(int idMovie);
        void UpdateMovie(Movie movie);

        void AddActor(Actor actor);
        void DeleteActor(Actor actor);
        IList<Actor> GetActors(int actorId);
        Actor GetActor(int idActor);
        void UpdateActor(Actor actor);

        void AddDirector(Director director);
        void DeleteDirector(Director director);
        IList<Director> GetDirectors(int directorId);
        Director GetDirector(int idDirector);
        void UpdateDirector(Director director);
    }
}