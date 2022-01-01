using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Zadatak.Models;
using Zadatak.Utils;

namespace Zadatak.Dal
{
    class SqlRepository : IRepository
    {
        private const string IDDirector = "@IDDirector";
        private const string ID = "@ID";
        private const string IDActor = "@IDActor";
        private const string MovieID = "@MovieID";
        private const string Name = "@Name";
        private const string Description = "@Description";
        private const string PictureParam = "@picture";
        private const string IdMovieParam = "@idMovie";

        // cannot be const
        //private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static readonly string cs = EncryptionUtils.Decrypt(ConfigurationManager.ConnectionStrings["cs"].ConnectionString, "fru1tc@k3");

        //Server=localhost,1433;Database=MoviesPPPK;User Id=sa;Password=Kobescak123!;
        //Server=tcp:algebrapppk.database.windows.net,1433;Initial Catalog=People;Persist Security Info=False;User ID=sas;Password=D0r1anL0ta;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

        public void AddMovie(Movie movie)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Name, movie.Name);
                    cmd.Parameters.AddWithValue(Description, movie.Description);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, movie.Picture.Length)
                    {
                        Value = movie.Picture
                    });
                    SqlParameter idMovie = new SqlParameter(IdMovieParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idMovie);
                    cmd.ExecuteNonQuery();
                    movie.IDMovie = (int)idMovie.Value;
                }
            }
        }

        public void DeleteMovie(Movie movie)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdMovieParam, movie.IDMovie);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Movie> GetMovies()
        {
            IList<Movie> people = new List<Movie>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            people.Add(ReadMovie(dr));
                        }
                    }
                }
            }
            return people;
        }

        public Movie GetMovie(int idMovie)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdMovieParam, idMovie);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadMovie(dr);
                        }
                    }
                }
            }
            throw new Exception("Movie does not exist");
        }

        private Movie ReadMovie(SqlDataReader dr) => new Movie
            {
                //IDMovie = (int)dr[nameof(Movie.IDMovie)],
                IDMovie = (int)dr[nameof(Movie.IDMovie)],
                Name = dr[nameof(Movie.Name)].ToString(),
                Description = dr[nameof(Movie.Description)].ToString(),
                Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 3) ?? new byte[0]

        };

        public void UpdateMovie(Movie movie)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Name, movie.Name);
                    cmd.Parameters.AddWithValue(Description, movie.Description);
                    cmd.Parameters.AddWithValue(IdMovieParam, movie.IDMovie);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, movie.Picture.Length)
                    {
                        Value = movie.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Actor ReadActor(SqlDataReader dr) => new Actor
        {
            IDActor = (int)dr[nameof(Actor.IDActor)],
            Name = dr[nameof(Actor.Name)].ToString(),
            MovieID = (int)dr[nameof(Actor.MovieID)]

        };

        public void AddActor(Actor actor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Name, actor.Name);
                    cmd.Parameters.AddWithValue(MovieID, actor.MovieID);
                    SqlParameter idActor = new SqlParameter(ID, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idActor);
                    cmd.ExecuteNonQuery();
                    actor.IDActor = (int)idActor.Value;
                }
            }
        }

        public void DeleteActor(Actor actor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDActor, actor.IDActor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Actor> GetActors(int movieId)
        {
            IList<Actor> actors = new List<Actor>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(MovieID, movieId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            actors.Add(ReadActor(dr));
                        }
                    }
                }
            }
            return actors;
        }

        public Actor GetActor(int idMovie)
        {
            throw new NotImplementedException();
        }

        public void UpdateActor(Actor actor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Name, actor.Name);
                    cmd.Parameters.AddWithValue(MovieID, actor.MovieID);
                    cmd.Parameters.AddWithValue(IDActor, actor.IDActor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Director ReadDirector(SqlDataReader dr) => new Director
        {
            IDDirector = (int)dr[nameof(Director.IDDirector)],
            Name = dr[nameof(Director.Name)].ToString(),
            MovieID = (int)dr[nameof(Director.MovieID)]

        };

        public void AddDirector(Director director)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Name, director.Name);
                    cmd.Parameters.AddWithValue(MovieID, director.MovieID);
                    SqlParameter idDirector = new SqlParameter(ID, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idDirector);
                    cmd.ExecuteNonQuery();
                    director.IDDirector = (int)idDirector.Value;
                }
            }
        }

        public void DeleteDirector(Director director)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDDirector, director.IDDirector);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Director> GetDirectors(int directorId)
        {
            IList<Director> directors = new List<Director>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(MovieID, directorId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            directors.Add(ReadDirector(dr));
                        }
                    }
                }
            }
            return directors;
        }

        public Director GetDirector(int idDirector)
        {
            throw new NotImplementedException();
        }

        public void UpdateDirector(Director director)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Name, director.Name);
                    cmd.Parameters.AddWithValue(MovieID, director.MovieID);
                    cmd.Parameters.AddWithValue(IDDirector, director.IDDirector);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
