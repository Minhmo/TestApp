using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestApp.Models;

namespace TestApp
{
    internal class MyDbInitializer : CreateDatabaseIfNotExists<MyDbContext>
    {
        private MyUser myinfo = new MyUser() {HomeTown = "VLN", Name = "TOM"};

        protected override void Seed(MyDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new
                UserStore<ApplicationUser>(context));

            var RoleManager = new RoleManager<IdentityRole>(new
                RoleStore<IdentityRole>(context));

            var name = "Admin";
            var password = "123456";


            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
            }

            //Create User=Admin with password=123456
            var user = new ApplicationUser();

            user.UserName = name;
            user.MyUserInfo = myinfo;         

            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)

            {
                var result = UserManager.AddToRole(user.Id, name);
            }

            base.Seed(context);
        }
    }
}