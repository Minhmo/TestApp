using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class PlayerInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Embeded { get; set; }

    }

    public class PlayerInfoDBContext : DbContext
    {
        public PlayerInfoDBContext() : base("DefaultConnection")
        {

        }
        public DbSet<PlayerInfo> PlayerInfos { get; set; }
    }
}