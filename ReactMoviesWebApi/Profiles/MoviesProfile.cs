using AutoMapper;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Profiles
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            // CreateMap<TSource, TDestination>
            CreateMap<CreateUpdate_MovieDTO, Movie>()
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.Poster, options => options.Ignore())
                .ForMember(m => m.MoviesGenres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(m => m.MovieTheatersMovies, options => options.MapFrom(MapMovieTheatersMovies))
                .ForMember(m => m.MoviesActors, options => options.MapFrom(MapMoviesActors));

            CreateMap<Movie, MovieDTO>()
                .ForMember(m => m.Genres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(m => m.MovieTheaters, options => options.MapFrom(MapMovieTheatersMovies))
                .ForMember(m => m.Actors, options => options.MapFrom(MapMoviesActors));
        }

        #region Mapping for Create Movie - MapFrom Private Methods
        private List<GenreDTO> MapMoviesGenres(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<GenreDTO>();

            if (movie.MoviesGenres != null)
            {
                foreach (var genre in movie.MoviesGenres)
                {
                    result.Add(new GenreDTO()
                    {
                        Id = genre.GenreId,
                        Name = genre.Genre.Name
                    });
                }
            }

            return result;
        }

        private List<MovieTheaterDTO> MapMovieTheatersMovies(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<MovieTheaterDTO>();

            if (movie.MovieTheatersMovies != null)
            {
                foreach (var movieTheater in movie.MovieTheatersMovies)
                {
                    result.Add(new MovieTheaterDTO()
                    {
                        Id = movieTheater.MovieTheaterId,
                        Name = movieTheater.MovieTheater.Name,
                        Latitude = movieTheater.MovieTheater.Location.Y,
                        Longitude = movieTheater.MovieTheater.Location.X,
                    });
                }
            }

            return result;
        }

        private List<ActorsMovieDTO> MapMoviesActors(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<ActorsMovieDTO>();

            if (movie.MoviesActors != null)
            {
                foreach (var moviesActors in movie.MoviesActors)
                {
                    result.Add(new ActorsMovieDTO()
                    {
                        Id = moviesActors.ActorId,
                        Name = moviesActors.Actor.Name,
                        Character = moviesActors.Character,
                        Picture = moviesActors.Actor.Picture,
                        Order = moviesActors.Order
                    });
                }
            }

            return result;
        }
        #endregion

        #region Mapping for Movie - MapFrom Private Methods
        private List<MoviesGenres> MapMoviesGenres(CreateUpdate_MovieDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MoviesGenres>();

            if (movieCreationDTO.GenresIds == null)
            {
                return result;
            }

            foreach (var id in movieCreationDTO.GenresIds)
            {
                result.Add(new MoviesGenres()
                {
                    GenreId = id
                });
            }

            return result;
        }

        private List<MovieTheatersMovies> MapMovieTheatersMovies(CreateUpdate_MovieDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MovieTheatersMovies>();

            if (movieCreationDTO.MovieTheatersIds == null)
            {
                return result;
            }

            foreach (var id in movieCreationDTO.MovieTheatersIds)
            {
                result.Add(new MovieTheatersMovies()
                {
                    MovieTheaterId = id
                });
            }

            return result;
        }

        private List<MoviesActors> MapMoviesActors(CreateUpdate_MovieDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MoviesActors>();

            if (movieCreationDTO.Actors == null)
            {
                return result;
            }

            foreach (var actor in movieCreationDTO.Actors)
            {
                result.Add(new MoviesActors()
                {
                    ActorId = actor.Id,
                    Character = actor.Character
                });
            }

            return result;
        }
        #endregion
    }
}
