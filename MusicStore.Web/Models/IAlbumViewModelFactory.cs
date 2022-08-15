using MusicStore.Domain;

namespace MusicStore.Web.Models
{
    public interface IAlbumViewModelFactory
    {
        AlbumViewModel Create(Album album, Genre genre);
    }
}