using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TestApp.Models
{
    public class MyUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HomeTown { get; set; }

    }

    public class Tag
    {
        public int Id { get; set; }
        [Required]
        public  string Name { get; set; }
        public virtual ICollection<Music> Music { get; set; }

       public string ApplicationUserId { get; set; }
        public virtual  ApplicationUser User { get; set; }
    }
}