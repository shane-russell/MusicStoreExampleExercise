using System;
using MusicStore.Domain;
using MusicStore.Web.Tests.Extensions;

namespace MusicStore.Web.Tests.Builders
{
    internal class GenreBuilder : BuilderBase
    {
        private readonly Genre _genre;

        public GenreBuilder()
        {
            _genre = new Genre
            {
                Id = RandomGenerator.NextPositive(),
                Name = RandomGenerator.NextString()
            };
        }

        public Genre Build()
        {
            return _genre;
        }
    }
}
