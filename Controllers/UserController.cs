using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YodiStore.YodiServiceRef;

namespace YodiStore.Controllers
{
  

    public class UserController : Controller
    {
        YodiServiceClient Users = new YodiServiceClient();


        public ActionResult LogOff()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult JsonUserSingIn(string UserName, string Password)
        {

            List<User> users = Users.ShowUser().ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SingIn(string username, string password)
        {


            try
            {

                User user = Users.GetUser(username, password);

                Session["User"] = user;
                ViewBag.User = Session["User"];
                return RedirectToActionPermanent("PrivatArea", "Home", new { id = user.UserId });
            }
            catch (Exception e)
            {

                ViewBag.Message = e.Message;
                return View();
            }








        }

        [HttpGet]
        public ActionResult SingIn()
        {

            return View();
        }

        public ActionResult JsonUser(string UserName)
        {

            List<User> users = Users.ShowUser().ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Join(string firstname, string lastname, string username, string email, string password)
        {

            User user = new User() { FirstName = firstname, LastName = lastname, Email = email, UserName = username, Password = password };
            Session["User"] = user;
            Users.AddUser(user);
            // added by orel


            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Join()
        {
            return View();
        }
    }
}