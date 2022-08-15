using System;
using MusicStore.Domain;
using MusicStore.Web.Tests.Extensions;

namespace MusicStore.Web.Tests.Builders
{
    internal class AlbumBuilder : BuilderBase
    {
        private readonly Album _album;

        public AlbumBuilder()
        {
            _album = new Album
            {
                Id = RandomGenerator.NextPositive(),
                Title = RandomGenerator.NextString(),
                Artist = RandomGenerator.NextString()
            };
        }

        public AlbumBuilder WithGenreId(int genreId)
        {
            _album.GenreId = genreId;
            return this;
        }

        public Album Build()
        {
            return _album;
        }
    }
}