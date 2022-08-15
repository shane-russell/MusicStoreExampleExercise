using System;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Web.Services;

namespace MusicStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public const string RockUrl = @"https://www.youtube.com/watch?v=v2AC41dglnM";

        private readonly IFileProvider _fileProvider;
        
        public HomeController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return CreateRouteInfoContent();
        }

        public IActionResult Details(int id)
        {
            return CreateRouteInfoContent();
        }

        public IActionResult Search(string genre)
        {
            switch (genre.ToLower())
            {
                case "rock":
                    return RedirectPermanent(RockUrl);
                case "jazz":
                    return RedirectToAction("Index");
                case "metal":
                    return RedirectToAction("Details", new { id = new Random().Next() });
                case "classic":
                    return File(_fileProvider.GetFileBytes(@"wwwroot\css\site.css"), "text/css", "site.css");
                default:
                    return CreateRouteInfoContent();
            }
        }

        private ContentResult CreateRouteInfoContent()
        {
            var controllerName = RouteData.Values["controller"];
            var actionName = RouteData.Values["action"];
            var message = $"{controllerName}:{actionName}";

            var id = RouteData.Values["id"];
            if (id != null)
            {
                message += $":{id}";
            }

            var genreParam = RouteData.Values["genre"];
            if (genreParam != null)
            {
                message += $":{genreParam}";
            }

            return Content(message);
        }
    }
}
