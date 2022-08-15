using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicStore.AppLogic;
using MusicStore.Domain;
using MusicStore.Web.Controllers;
using MusicStore.Web.Models;
using MusicStore.Web.Tests.Builders;
using NUnit.Framework;

namespace MusicStore.Web.Tests.Controllers
{
    public class StoreControllerTests : TestBase
    {
        private StoreController _controller;
        private Mock<IGenreRepository> _genreRepositoryMock;
        private Mock<IAlbumRepository> _albumRepositoryMock;
        private Mock<IAlbumViewModelFactory> _albumViewModelFactoryMock;

        [SetUp]
        public void Setup()
        {
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _albumRepositoryMock = new Mock<IAlbumRepository>();
            _albumViewModelFactoryMock = new Mock<IAlbumViewModelFactory>();
            _controller = new StoreController(_genreRepositoryMock.Object, _albumRepositoryMock.Object,
                _albumViewModelFactoryMock.Object);
        }

        [Test]
        public void Index_ShouldShowListOfMusicGenres()
        {
            //Arrange
            var allGenres = new List<Genre>();

            _genreRepositoryMock.Setup(repo => repo.GetAll()).Returns(allGenres);

            //Act
            var viewResult = _controller.Index() as ViewResult;

            //Assert
            Assert.That(viewResult, Is.Not.Null);
            _genreRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.That(viewResult.Model, Is.SameAs(allGenres));
        }

        [Test]
        public void Browse_ShouldShowAlbumsOfGenre()
        {
            //Arrange
            Genre genre = new GenreBuilder().Build();
            var albums = new List<Album>();

            _genreRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(genre);
            _albumRepositoryMock.Setup(repo => repo.GetByGenre(It.IsAny<int>())).Returns(albums);

            //Act
            var viewResult = _controller.Browse(genre.Id) as ViewResult;

            //Assert
            Assert.That(viewResult, Is.Not.Null);
            _genreRepositoryMock.Verify(repo => repo.GetById(genre.Id), Times.Once);
            _albumRepositoryMock.Verify(repo => repo.GetByGenre(genre.Id), Times.Once);

            Assert.That(viewResult.Model, Is.SameAs(albums));
            Assert.That(_controller.ViewBag.Genre, Is.EqualTo(genre.Name));
           
        }

        [Test]
        public void Browse_InvalidGenreId_ShouldReturnNotFound()
        {
            //Arrange
            _albumRepositoryMock.Setup(repo => repo.GetByGenre(It.IsAny<int>())).Returns(() => null);

            //Act
            var notFoundResult = _controller.Browse(RandomGenerator.Next()) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);
        }

        [Test]
        public void Details_ShouldShowDetailsOfAlbum()
        {
            //Arrange
            var genre = new GenreBuilder().Build();
            var album = new AlbumBuilder().WithGenreId(genre.Id).Build();
            var albumViewModel = new AlbumViewModel();

            _albumRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(album);
            _genreRepositoryMock.Setup(repo => repo.GetById((It.IsAny<int>()))).Returns(genre);
            _albumViewModelFactoryMock.Setup(factory => factory.Create(It.IsAny<Album>(), It.IsAny<Genre>()))
                .Returns(albumViewModel);

            //Act
            var viewResult = _controller.Details(album.Id) as ViewResult;

            //Assert
            Assert.That(viewResult, Is.Not.Null);

            _albumRepositoryMock.Verify(repo => repo.GetById(album.Id), Times.Once);
            _genreRepositoryMock.Verify(repo => repo.GetById(album.GenreId), Times.Once);
            _albumViewModelFactoryMock.Verify(factory => factory.Create(album, genre), Times.Once);

            Assert.That(viewResult.Model, Is.SameAs(albumViewModel));
        }

        [Test]
        public void Details_InvalidId_ShouldReturnNotFound()
        {
            //Arrange
            _albumRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(() => null);

            //Act
            var notFoundResult = _controller.Details(RandomGenerator.Next()) as NotFoundResult;

            //Assert
            Assert.That(notFoundResult, Is.Not.Null);
        }
    }
}