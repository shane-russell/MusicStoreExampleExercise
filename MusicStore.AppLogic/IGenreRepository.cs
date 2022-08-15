using System.Collections.Generic;
using MusicStore.Domain;

namespace MusicStore.AppLogic
{
    public interface IGenreRepository
    {
        IReadOnlyList<Genre> GetAll();
        Genre GetById(int id);
    }
}
