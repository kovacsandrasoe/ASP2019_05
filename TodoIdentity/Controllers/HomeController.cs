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

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
