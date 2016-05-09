using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestApp.Models;
using TestApp.Services;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext db;
        private UserManager<ApplicationUser> manager;
        private IMusicService _musicService;
        public HomeController(IMusicService musicService)
        {
            _musicService = musicService;
            db = new MyDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        [HttpPost]
        public ActionResult LoadSearch(MusicIndexViewModel model)
        {
            var musicEntity = db.Musics.Where(m => m.Tags.Any( a => a.Name.StartsWith(model.SearchTerm))).Select( t => new MusicIndexViewModel()
            {
                FormattedLink = t.FormattedLink,
                Tags = t.Tags,
                Comment = t.Comment,
                UserName = t.User.UserName,
                SongName = t.Name
            }).First();
            return PartialView("RandomMusic", musicEntity);
        }
        public ActionResult Search(string term)
        {
            var tag = db.Tags.Where(t => t.Name.StartsWith(term)).Select(t => t.Name).Take(10);
            return Json(tag, JsonRequestBehavior.AllowGet);
            //var model = db.Musics.Where(t => t.Tags.Contains(tag)).Select( t=> t.);
        }
        public ActionResult Index()
        {
            //var maxId = data.Music.Max(Music => Music.Id);
            //var random = new Random().Next(1, maxId);
            //var music = data.Music.Find(random);
            //if (string.IsNullOrEmpty(music.FormattedLink))
            //{
            //    music.FormattedLink = playerService.FormatUri(music.Link);
            //}
            return RedirectToAction("RandomMusic");
        }
        [HttpPost]
        public JsonResult LikeSong(Music model)
        {
           var musicModel = db.Musics.Find(model.Id);
            var currentUser = manager.FindById(User.Identity.GetUserId());
           var change = manager.Users.FirstOrDefault(u => u.Id == currentUser.Id);
            change.LikedSongs.Add(musicModel);

            db.SaveChanges();
            return Json(new { Success = true });
        }

        public async Task<ActionResult> RandomMusic()
        {
            var random = new Random();
            ICollection<MusicIndexViewModel> userMusic = null;

            if (!Request.IsAjaxRequest())
            {
                if (Request.IsAuthenticated)
                {
                    userMusic = await _musicService.GetUserPreferedMusic(User.Identity.GetUserId());
                    if (userMusic.Count == 0)
                    {
                        userMusic = await _musicService.GetAllMusic();
                    }
                }
                else
                {
                    userMusic = await _musicService.GetAllMusic();
                }
                return View("Index", userMusic.AsEnumerable().ElementAt(random.Next(1, _musicService.GetMusicCount())));
            }
            else
            {
                if (Request.IsAuthenticated)
                {
                    userMusic = await _musicService.GetUserPreferedMusic(User.Identity.GetUserId());
                    if (userMusic.Count == 0)
                    {
                        userMusic = await _musicService.GetAllMusic();
                    }
                }
                else
                {
                    userMusic = await _musicService.GetAllMusic();
                }
                return PartialView("RandomMusic", userMusic.AsEnumerable().ElementAt(random.Next(1, db.Musics.Count())));
            }            
        }

        //public ActionResult SetPreferedTags()
        //{
            
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult MyMusic()
        {
            var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MyDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            return View();

        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}