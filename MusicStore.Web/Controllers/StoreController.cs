using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MusicStore.AppLogic;
using MusicStore.Domain;
using MusicStore.Web.Models;

namespace MusicStore.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IAlbumViewModelFactory _albumViewModelFactory;

        public StoreController(IGenreRepository genreRepository, 
            IAlbumRepository albumRepository,
            IAlbumViewModelFactory albumViewModelFactory)
        {
            _genreRepository = genreRepository;
            _albumRepository = albumRepository;
            _albumViewModelFactory = albumViewModelFactory;
        }

        public IActionResult Index()
        {
            IReadOnlyList<Genre> genres = _genreRepository.GetAll();
            return View(genres);
        }

        // GET: Store/Browse/5
        public IActionResult Browse(int id)
        {
            IReadOnlyList<Album> albums = _albumRepository.GetByGenre(id);

            if (albums == null) return NotFound();

            ViewBag.Genre = _genreRepository.GetById(id).Name;

            return View(albums);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            var album = _albumRepository.GetById(id);

            if (album == null) return NotFound();

            Genre genre = _genreRepository.GetById(album.GenreId);

            AlbumViewModel model = _albumViewModelFactory.Create(album, genre);

            return View(model);
        }
    }
}