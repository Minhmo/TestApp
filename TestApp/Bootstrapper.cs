using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using TestApp.Controllers;
using TestApp.infrastructure;
using TestApp.Models;
using TestApp.Services;
using Unity.Mvc5;

namespace TestApp
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IMusicService, MusicService>();
            container.RegisterType<MyDbContext, MyDbContext>();
            container.RegisterType<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();


            container.RegisterType<IController, HomeController>("Music");
            container.RegisterType<IController, AccountController>("Account");

            DependencyResolver.SetResolver(new TestDependencyResolver(container));

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();            

            return container;
        }
    }
}