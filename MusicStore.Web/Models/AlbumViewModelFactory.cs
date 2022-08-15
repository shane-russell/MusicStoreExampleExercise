using System;
using MusicStore.Domain;

namespace MusicStore.Web.Models
{
    public class AlbumViewModelFactory : IAlbumViewModelFactory
    {
        public AlbumViewModel Create(Album album, Genre genre)
        {
            if (album == null || genre == null || album.GenreId != genre.Id)
            {
                throw new ArgumentException();
            }

            return new AlbumViewModel
            {
                Artist = album.Artist,
                Title = album.Title,
                Genre = genre.Name
            };
        }
    }
}