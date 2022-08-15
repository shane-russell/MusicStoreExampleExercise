using System.Collections.Generic;
using MusicStore.Domain;

namespace MusicStore.AppLogic
{
    public interface IAlbumRepository
    {
        IReadOnlyList<Album> GetByGenre(int genreId);
        Album GetById(int id);
    }
}