using Microsoft.AspNetCore.Mvc;
using MusicStore.Web.Models;

namespace MusicStore.Web.ViewComponents
{
    public class AlbumOfTheDay : ViewComponent
    {
        private readonly AlbumViewModel album;
        public AlbumOfTheDay()
        {
            album = new AlbumViewModel()
            {
                Artist = "A popular artist",
                Genre = "Some genre",
                Title = "Some title"
            };
        }
        public IViewComponentResult Invoke()
        {
            return View(album);
        }

    }
}