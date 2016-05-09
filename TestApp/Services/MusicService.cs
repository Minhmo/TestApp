using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Services
{
    public class MusicService:IMusicService
    {
        private MyDbContext _db;

        public MusicService(MyDbContext dbContext)
        {
            _db = dbContext;
        }
        public ICollection<Music> GetUserUploads(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<MusicIndexViewModel>> GetUserPreferedMusic(string userId)
        {
            var userTags = _db.Tags.Where(t => t.ApplicationUserId.Equals(userId)).ToListAsync();
            var userMusic = new List<MusicIndexViewModel>();

            foreach (var tag in await userTags)
            {
                var music = _db.Musics.Where(t => t.Tags.Any(m => m.Name.Equals(tag.Name))).Select( t => new MusicIndexViewModel()
                {
                    Tags = t.Tags,
                    UserName = t.User.UserName,
                    FormattedLink = t.FormattedLink,
                    Comment = t.Comment,
                    SongName = t.Name
                }).ToListAsync();
                userMusic.AddRange( await music);
            }
            return userMusic;
        }

        public async Task<ICollection<MusicIndexViewModel>> GetAllMusic()
        {
            return await _db.Musics.Take(1000).Select( t => new MusicIndexViewModel()
            {
                FormattedLink = t.FormattedLink,
                Comment = t.Comment,
                SongName = t.Name,
                Tags = t.Tags,
                UserName = t.User.UserName
            }).ToListAsync();
        }
        public async Task<ICollection<Tag>> ConvertStringToTags(UserSettingsUpdateViewModel model, string userId)
        {
            var tagList = new List<Tag>();
            foreach (var tag in model.tags)
            {
                var tagObj  = await _db.Tags.FirstOrDefaultAsync(t => t.Name.Equals(tag));
                if (tagObj == null)
                {
                    var newTag = new Tag()
                    {
                        ApplicationUserId = userId,
                        Name = tag,
                    };
                    _db.Tags.Add(newTag);
                    tagList.Add(newTag);
                }
                else
                {
                    tagList.Add(tagObj);
                }


            }
           await _db.SaveChangesAsync();
            return tagList;
        }

        public async Task AddMusic(MusicCreateViewModel model, ApplicationUser User)
        {
            var musicEnt = await FillBaseMusicModel(model);
            musicEnt.UserId = User.Id;
            _db.Musics.Add(musicEnt);
           await _db.SaveChangesAsync();

        }

        public int GetMusicCount()
        {
            return _db.Musics.Count();
        }

        private async Task<Music> FillBaseMusicModel(MusicCreateViewModel viewModel)
        {
            var tags = AddNewTags(viewModel.Tags);
            var model = new Music()
            {
                Comment = viewModel.Comment,
                Link = viewModel.Link,
                Name = viewModel.Name,
                FormattedLink = GetFormatedWidget(viewModel.Link),
                Tags = await tags
            };
            return model;
        }
        private async Task<List<Tag>> AddNewTags(string[] tags)
        {
            var tagList = new List<Tag>();

            foreach (var tag in tags)
            {
                tagList.Add(new Tag
                {
                    Name = tag,
                });

                if (!_db.Tags.Any(o => o.Name == tag))
                {
                    var ent = new Tag
                    {
                        Name = tag
                    };
                    _db.Tags.Add(ent);
                }
            }
           await _db.SaveChangesAsync();
            return tagList;
        }
        private string GetFormatedWidget(string uri)
        {

            if (uri.ToLower().Contains("youtube"))
            {


                const string pattern = @"(?:https?:\/\/)?(?:www\.)?(?:(?:(?:youtube.com\/watch\?[^?]*v=|youtu.be\/)([\w\-]+))(?:[^\s?]+)?)";
                const string replacement = "<iframe title='YouTube video player' width='480' height='390' src='http://www.youtube.com/embed/$1' frameborder='0' allowfullscreen='1'></iframe>";

                var rgx = new Regex(pattern);
                var result = rgx.Replace(uri, replacement);
                return result;
            }
            else if (uri.ToLower().Contains("soundcloud"))
            {
                string iframe =
                    $"<iframe id='sc-widget' src='https://w.soundcloud.com/player/?url={uri}' width='100%' height='465' scrolling='no' frameborder='no'></iframe>";
                return iframe;
            }

            return null;
        }

    }

    public interface IMusicService
    {
        ICollection<Music> GetUserUploads(string userId);
        Task<ICollection<MusicIndexViewModel>> GetUserPreferedMusic(string userId);
        Task<ICollection<MusicIndexViewModel>> GetAllMusic();
        Task<ICollection<Tag>> ConvertStringToTags(UserSettingsUpdateViewModel model, string userId);
        Task  AddMusic(MusicCreateViewModel model, ApplicationUser User);
        int GetMusicCount();




    }
}