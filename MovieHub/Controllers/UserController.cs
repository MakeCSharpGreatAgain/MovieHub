using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieHub.Data;
using MovieHub.Models;
using MovieHub.Services.Interfaces;
using MovieHub.Services;

namespace MovieHub.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //GET : User/Profile
        public ActionResult ProfilePage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeProfilePic()
        {
            if (User.Identity.IsAuthenticated)
            {
                // To convert the user uploaded Photo as Byte Array before save to DB
                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["ProfilePicture"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }  
                }

                var store = new UserStore<ApplicationUser>(new MovieDbContext());
                var userManager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;

                IApplicationUserService userService = ServiceLocator.Instance.GetService<IApplicationUserService>();
                userService.AddUserProfilePicture(user.Id, imageData);
                //using (var db = new MovieDbContext())
                //{
                //    var u = db.Users.Find(user.Id);

                //    if (u != null)
                //    {
                //        u.ProfilePicture = imageData;
                //        db.SaveChanges();
                //    }
                //}
            }

            return RedirectToAction("ProfilePage", "User");
        }
    }
}