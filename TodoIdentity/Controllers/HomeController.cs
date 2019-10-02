using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoIdentity.Data;
using TodoIdentity.Models;

namespace TodoIdentity.Controllers
{
    public class HomeController : Controller
    {
        RoleManager<IdentityRole> rolemanager; //szerepkörkezelő
        UserManager<IdentityUser> usermanager; //felhasználókezelő
        ApplicationDbContext database; //adatbáziskezelés

        public HomeController(RoleManager<IdentityRole> rolemanager,
           UserManager<IdentityUser> usermanager,
           ApplicationDbContext database)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
            this.database = database;
        }


        //[HttpGet]
        //public async Task<IActionResult> FirstStep()
        //{
        //    //1. csinálunk egy admin szerepkört
        //    //2. beletesszük az első user-t

        //    IdentityRole adminrole = new IdentityRole()
        //    {
        //        Name = "admins"
        //    };
        //    await rolemanager.CreateAsync(adminrole);

        //    var firstuser = usermanager.Users.FirstOrDefault(); 

        //    await usermanager.AddToRoleAsync(firstuser, "admins");

        //    return RedirectToAction("Index");

        //}
        

        


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddJob(string name)
        {
            //kik vagyunk mi?
            var myself = this.User;
            string user = usermanager.GetUserId(myself);

            Job j = new Job()
            {
                Name = name,
                Owner = user
            };
            database.Jobs.Add(j);
            database.SaveChanges();
            return RedirectToAction(nameof(Dashboard));
        }

        [Authorize(Roles = "admins")]
        public IActionResult Admin()
        {
            return View(database.Jobs);
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            //kik vagyunk mi?
            var myself = this.User;
            string user = usermanager.GetUserId(myself);

            var ownjobs = database.Jobs.
                Where(u => u.Owner == user);

            return View(ownjobs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
