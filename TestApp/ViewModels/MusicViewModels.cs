using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class MusicCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Link { get; set; }

        public string Comment { get; set; }

        public string [] Tags { get; set; }
    
    }

    public class MusicDisplayViewModel
    {
        
    }
    public class MusicIndexViewModel
    {
        public string SongName { get; set; }
        public string UserName { get; set; }

        public string Comment { get; set; }
        public string FormattedLink { get; set; }

        public string SearchTerm { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}