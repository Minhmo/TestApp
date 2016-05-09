using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TestApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser ()
        {
            this.LikedSongs = new HashSet<Music>();
        }
        public virtual ICollection<Music> MusicProvided { get; set; }

        public virtual ICollection<Tag> InterestTags { get; set; }

        public virtual ICollection<Music> LikedSongs { get; set; }
        public virtual MyUser MyUserInfo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Link { get; set; }

        public string Comment { get; set; }
        public string FormattedLink { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ApplicationUser> Users {get; set; } 
        public virtual  ICollection<Tag> Tags { get; set; }
    }

    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyDbContext()
            : base("DefaultConnection", false)
        {
        }

        public DbSet<Music> Musics { get; set; }

        public DbSet<Tag> Tags { get; set; } 

        public DbSet<MyUser> MyUserInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");

            modelBuilder.Entity<ApplicationUser>()
                   .HasMany<Music>(s => s.LikedSongs)
                   .WithMany(c => c.Users)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("UserRefId");
                       cs.MapRightKey("MusicRefId");
                       cs.ToTable("UserMusic");
                   });
        }

        public static MyDbContext Create()
        {
            return new MyDbContext();
        }

       // public System.Data.Entity.DbSet<TestApp.Models.ApplicationUser> IdentityUsers { get; set; }

        //public System.Data.Entity.DbSet<TestApp.Models.ApplicationUser> IdentityUsers { get; set; }
    }
}