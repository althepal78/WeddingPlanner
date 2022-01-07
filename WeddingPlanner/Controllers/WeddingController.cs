using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        [HttpGet]
        public IActionResult Dashboard()
        {
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
            Wedding wed = _DBContext.Weddings.FirstOrDefault(wed => wed.WedId == wedID);

            if (wed == null)
            {
                return RedirectToAction("Logout", "User");
            }
            User userInDB = GetUser();
            ViewBag.User = userInDB.UserId;
            UpdateWedding uw = new UpdateWedding();
          
            uw.Id = wedID;
            uw.WedDate = wed.WedDate;
            uw.WedAddy = wed.WedAddy;
            uw.WedOne = wed.WedOne;
            uw.WedTwo = wed.WedTwo;

            return View(uw);
        }

        [HttpPost]
        public IActionResult Update(int id, UpdateWedding update)
        {
            Wedding wed = _DBContext.Weddings.FirstOrDefault(wed => wed.WedId == id);

            if (wed == null)
            {
                Console.WriteLine("Inside the wed == null ");
                return RedirectToAction("Logout", "User");
            }
            if (update == null) {
                Console.WriteLine("Inside the update == null ");
                return RedirectToAction("Logout", "User");
            }
            update.Id = id;

            User userInDB = GetUser();
            ViewBag.User = userInDB.UserId;
            if (userInDB != null)
            {
                if (ModelState.IsValid)
                {
                    wed.WedOne = update.WedOne;
                    wed.WedTwo = update.WedTwo;
                    wed.WedAddy = update.WedAddy;
                    wed.WedDate = update.WedDate;

                    _DBContext.Weddings.Update(wed);
                    _DBContext.SaveChanges();

                    Console.WriteLine(id + " if model state is valid");

                    return RedirectToAction("ShowWed", new { id = id });
                }
            }


            Console.WriteLine(id + " if model state is wrong");
            return RedirectToAction("Edit", new { wedID = id });

        }

    }
}
