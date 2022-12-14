using System.Collections.Generic;
using System.Linq;
using MusicStore.AppLogic;
using MusicStore.Domain;

namespace MusicStore.Data
{
    internal class GenreDummyRepository : IGenreRepository
    {
        private static readonly List<Genre> _genres = new List<Genre> {
            new Genre
            {
                Id = 1,
                Name = "Metal"
            },
            new Genre
            {
                Id = 2,
                Name = "Pop"
            },
            new Genre
            {
                Id = 3,
                Name = "Jazz"
            }
        };

        public IReadOnlyList<Genre> GetAll()
        {
            return _genres;
        }

        public Genre GetById(int id)
        {
            return _genres.FirstOrDefault(g => g.Id == id);
        }
    }
}