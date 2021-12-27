using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Context;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {

        private WeddingContext _DBContext;

        public WeddingController(WeddingContext context)
        {
            _DBContext = context;
        }
        public User GetUser()
        {
            return _DBContext.Users.FirstOrDefault(us => us.UserId == HttpContext.Session.GetInt32("UserID"));
        }


        [HttpGet]
        public IActionResult Dashboard()
        {
            User userInDB = GetUser();
            if(userInDB == null)
            {
                return RedirectToAction("Logout", "User");
            }
            ViewBag.User = userInDB;

            List<Wedding> AllWeddings = _DBContext.Weddings
                                        .Include(cr => cr.Creator)
                                        .Include(g => g.RSVPs)
                                        .ThenInclude(guest => guest.Guests)
                                        .ToList();

            return View(AllWeddings);
        }


        [HttpGet]
        public IActionResult AddWedding()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWedding(Wedding wed)
        {

            User userInDB = GetUser();
            if (userInDB != null)
            {
                if (ModelState.IsValid)
                {
                    wed.UserID = userInDB.UserId;
                    _DBContext.Weddings.Add(wed);
                    _DBContext.SaveChanges();
                    return RedirectToAction("ShowWed", new { id = wed.WedId });
                }

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ShowWed(int id)
        {
            var wed = _DBContext.Weddings
                .Include(R => R.RSVPs)
                .ThenInclude(g => g.Guests)
                .Include(cr => cr.Creator)
                .FirstOrDefault(wedid => wedid.WedId == id);
                        

            if (wed == null)
            {
                return RedirectToAction("Dashboard", "Wedding");
            }
            


            return View(wed);
        }

        [HttpGet]
        public IActionResult Delete(int wedId)
        {
            Wedding Canceled = _DBContext.Weddings
                                .FirstOrDefault(wed => wed.WedId == wedId);
            if(Canceled == null)
            {
                return RedirectToAction("Logout", "User");
            }
            _DBContext.Weddings.Remove(Canceled);
            _DBContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult NotGoing(int userID, int WedID)
        {
            RSVP notGoing = _DBContext.RSVRs
                            .FirstOrDefault(ng => ng.UserID == userID && ng.WeddingID == WedID);

            _DBContext.RSVRs.Remove(notGoing);
            _DBContext.SaveChanges();


            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult Going(int userID, int WedID)
        {
            RSVP Going = new RSVP();
            Going.UserID = userID;
            Going.WeddingID = WedID;
            _DBContext.RSVRs.Add(Going);
            _DBContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }

    }
}
