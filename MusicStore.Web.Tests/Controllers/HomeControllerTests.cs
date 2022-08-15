using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using MusicStore.Web.Controllers;
using MusicStore.Web.Services;
using MusicStore.Web.Tests.Extensions;
using NUnit.Framework;

namespace MusicStore.Web.Tests.Controllers
{
    public class HomeControllerTests : TestBase
    {
        private HomeController _controller;
        private string _routeControllerName;
        private string _routeActionName;
        private Mock<IFileProvider> _fileProviderMock;

        [SetUp]
        public void Setup()
        {
            _fileProviderMock = new Mock<IFileProvider>();
            _controller = new HomeController(_fileProviderMock.Object);
            SetRandomRouteDataForController();
        }

        [Test]
        public void Index_ShouldReturnDefaultView()
        {
            ViewResult result = _controller.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.Null);
        }

        [Test]
        public void About_ShouldReturnContentContainingControllerNameAndActionName()
        {
            //Act
            ContentResult result = _controller.About() as ContentResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EqualTo($"{_routeControllerName}:{_routeActionName}"));
        }

        [Test]
        public void Details_ShouldReturnContentContainingControllerNameActionNameAndParamName()
        {
            // Arrange
            var id = RandomGenerator.Next(1, int.MaxValue);
            _controller.RouteData.Values["id"] = id;

            // Act
            var contentResult = _controller.Details(id) as ContentResult;

            // Assert
            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.EqualTo($"{_routeControllerName}:{_routeActionName}:{id}"));
        }

        [Test]
        public void Search_Rock_ShouldReturnPermanentRedirect()
        {
            // Act
            var redirectResult = _controller.Search("Rock") as RedirectResult;

            // Assert
            Assert.That(redirectResult, Is.Not.Null);
            Assert.That(redirectResult.Permanent, Is.True);
            Assert.That(redirectResult.Url, Is.EqualTo(HomeController.RockUrl));
        }

        [Test]
        public void Search_Jazz_ShouldRedirectToIndexAction()
        {
            // Act
            var redirectToRouteResult = _controller.Search("Jazz") as RedirectToActionResult;

            // Assert
            Assert.That(redirectToRouteResult, Is.Not.Null);
            Assert.That(redirectToRouteResult.Permanent, Is.False);
            Assert.That(redirectToRouteResult.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public void Search_Metal_ShouldRedirectToDetailsActionWithARandomId()
        {
            // Act
            var redirectToActionResult = _controller.Search("Metal") as RedirectToActionResult;

            // Assert
            Assert.That(redirectToActionResult, Is.Not.Null);
            Assert.That(redirectToActionResult.Permanent, Is.False);
            Assert.That(redirectToActionResult.ActionName, Is.EqualTo("Details"));
            Assert.That(Convert.ToInt32(redirectToActionResult.RouteValues["id"]), Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void Search_Classic_ShouldReturnContentOfSiteCssFile()
        {
            //Arrange
            var fileBytes = RandomGenerator.NextByteArray();
            _fileProviderMock.Setup(provider => provider.GetFileBytes(It.IsAny<string>())).Returns(fileBytes);

            // Act
            var fileResult = _controller.Search("Classic") as FileContentResult;

            // Assert
            Assert.That(fileResult, Is.Not.Null);
            Assert.That(fileResult.ContentType, Is.EqualTo("text/css"));
            Assert.That(fileResult.FileContents, Is.EquivalentTo(fileBytes));
            Assert.That(fileResult.FileDownloadName, Is.EqualTo("site.css"));
            _fileProviderMock.Verify(provider => provider.GetFileBytes(@"wwwroot\css\site.css"), Times.Once);
        }

        [Test]
        public void Search_UnknownGenre_ShouldReturnContentContainingControllerNameActionNameAndGenreParameter()
        {
            // Arrange
            var genre = RandomGenerator.NextString();

            _controller.RouteData.Values["genre"] = genre;

            // Act
            var contentResult = _controller.Search(genre) as ContentResult;

            // Assert
            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.EqualTo($"{_routeControllerName}:{_routeActionName}:{genre}"));
        }

        private void SetRandomRouteDataForController()
        {
            var routeData = new RouteData();

            _routeControllerName = RandomGenerator.NextString();
            routeData.Values["controller"] = _routeControllerName;

            _routeActionName = RandomGenerator.NextString();
            routeData.Values["action"] = _routeActionName;

            _controller.ControllerContext = new ControllerContext
            {
                RouteData = routeData
            };
        }
    }
}