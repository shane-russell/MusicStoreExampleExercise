using MusicStore.Web.Models;
using MusicStore.Web.Tests.Builders;
using NUnit.Framework;

namespace MusicStore.Web.Tests.Models
{
    [TestFixture]
    public class AlbumViewModelFactoryTests
    {
        private AlbumViewModelFactory _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new AlbumViewModelFactory();
        }

        [Test]
        public void Create_ValidAlbumAndValidGenre_ShouldMapPropertiesCorrectly()
        {
            //Arrange
            var genre = new GenreBuilder().Build();
            var album = new AlbumBuilder().WithGenreId(genre.Id).Build();

            //Act
            var viewModel = _factory.Create(album, genre);

            //Assert
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.Artist, Is.EqualTo(album.Artist));
            Assert.That(viewModel.Title, Is.EqualTo(album.Title));
            Assert.That(viewModel.Genre, Is.EqualTo(genre.Name));
        }

        [Test]
        public void Create_MissingGenre_ShouldThrowException()
        {
            //Arrange
            var album = new AlbumBuilder().Build();

            //Act + Assert
            Assert.That(() => _factory.Create(album, null), Throws.ArgumentException);
        }

        [Test]
        public void Create_MissingAlbum_ShouldThrowException()
        {
            //Arrange
            var genre = new GenreBuilder().Build();

            //Act + Assert
            Assert.That(() => _factory.Create(null, genre), Throws.ArgumentException);
        }

        [Test]
        public void Create_MismatchBetweenAlbumAndGenre_ShouldThrowException()
        {
            //Arrange
            var genre = new GenreBuilder().Build();
            var album = new AlbumBuilder().WithGenreId(genre.Id + 1).Build();

            //Act + Assert
            Assert.That(() => _factory.Create(album, genre), Throws.ArgumentException);
        }
    }
}