using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WeddingPlanner.Context;
using WeddingPlanner.Models;
using WeddingPlanner.ViewModel;

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

        private void DeleteOldWedding()
        {
            
            foreach (Wedding date in _DBContext.Weddings.ToList())
            {
                var result = DateTime.Compare(date.WedDate , DateTime.Now);
                // r < 0 before this date and time, r=0 both date and time exactly the same, r> 0 It is after this date or time
                if (result < 0)
                {
                    _DBContext.Weddings.Remove(date);
                    _DBContext.SaveChanges();
                }
            }
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            DeleteOldWedding();
            User userInDB = GetUser();
            if (userInDB == null)
            {
                return RedirectToAction("Logout", "User");
            }


            List<Wedding> AllWeddings = _DBContext.Weddings
                                        .Include(cr => cr.Creator)
                                        .Include(g => g.RSVPs)
                                        .ThenInclude(guest => guest.Guests)
                                        .ToList();

            ViewBag.User = userInDB.UserId;
            return View(AllWeddings);
        }

      

        [HttpGet]
        public IActionResult AddWedding()
        {
            User userInDB = GetUser();
            ViewBag.User = userInDB.UserId;

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
                    wed.Creator = userInDB;
                    _DBContext.Weddings.Add(wed);
                    _DBContext.SaveChanges();
                    ViewBag.User = userInDB.UserId;
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
            User userInDB = GetUser();
            ViewBag.User = userInDB.UserId;

            return View(wed);
        }


        // this is to delete the wedding by user's choice

        [HttpGet]
        public IActionResult Delete(int wedId)
        {
            Wedding Canceled = _DBContext.Weddings
                                .Include(rs => rs.RSVPs) // I forgot this
                                .FirstOrDefault(wed => wed.WedId == wedId);


            if (Canceled == null)
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


        [HttpGet]
        public IActionResult Edit(int wedID)
        {
            Wedding wed = _DBContext.Weddings.FirstOrDefault(w => w.WedId == wedID);
            WedUpdate wu = new WedUpdate();

            if (wed == null)
            {
                return RedirectToAction("Logout", "User");
            }

            wu.WedId = wedID;
            wu.WedOne = wed.WedOne;
            wu.WedAddy = wed.WedAddy;
            wu.WedTwo = wed.WedTwo;
            wu.WedDate = wed.WedDate;
            return View(wu);
        }

        [HttpPost]
        public IActionResult Edit(WedUpdate update)
        {
            Wedding wed = _DBContext.Weddings.FirstOrDefault(w => w.WedId == update.WedId);

            if (!ModelState.IsValid)
            {
                return View(update);
            }

            wed.WedDate = update.WedDate;
            wed.WedAddy = update.WedAddy;
            wed.UpdatedOn = DateTime.Now;
            wed.WedOne = update.WedOne;
            wed.WedTwo = update.WedTwo;

            _DBContext.Weddings.Update(wed);
            _DBContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
    }
}
