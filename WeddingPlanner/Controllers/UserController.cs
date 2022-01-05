using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Context;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class UserController : Controller
    {

        private WeddingContext _DBContext;

        public UserController(WeddingContext context)
        {
            _DBContext = context;
        }

        public void setSession(int id)
        {
            HttpContext.Session.SetInt32("UserID", id);
        }

        public User GetUser()
        {
            return _DBContext.Users.FirstOrDefault(us => us.UserId == HttpContext.Session.GetInt32("UserID"));
        }
        public IActionResult Index()
        {
            User userInDb = GetUser();
            ViewBag.User = userInDb.UserId;
            return View(userInDb);
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        // also can be create for get and post
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_DBContext.Users.Any(em => em.EMail == user.EMail))
                {
                    ModelState.AddModelError("Email", "E-Mail is already in Database");

                    return View();
                }

                PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, user.Password);
                _DBContext.Users.Add(user);
                _DBContext.SaveChanges();

                setSession(user.UserId);

                ViewBag.User = user.UserId;
                return RedirectToAction("Dashboard","Wedding");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            User userinDB = _DBContext.Users.FirstOrDefault(em => em.EMail == login.LoginEMail);
            if (ModelState.IsValid)
            {
                if (userinDB == null)
                {
                    ModelState.AddModelError("Email", "don't exist");
                    return View();
                }
                PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(userinDB, userinDB.Password, login.LoginPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("Passwor", "Password is wrong");
                    return View();
                }

                setSession(userinDB.UserId);
                ViewBag.User = userinDB.UserId;

                return RedirectToAction("Dashboard", "Wedding");

            }

            return View();
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
   
    }
    
}
