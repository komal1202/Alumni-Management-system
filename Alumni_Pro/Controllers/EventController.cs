using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni_Pro.Controllers
{
    public class EventController : Controller
    {
        ADBEntities db = new ADBEntities();
        // GET: Event
        public ActionResult Index()
        {
            int currentDay = DateTime.Today.Day;
            int currentMonth = DateTime.Today.Month;

            // Retrieve users whose marriage date matches the current day and month
            var birthdaysToday = db.UserTbls
                .Where(u => u.DateOfBirth.HasValue && u.DateOfBirth.Value.Day == currentDay && u.DateOfBirth.Value.Month == currentMonth)
                .ToList();

            // Retrieve users whose anniversary date matches the current day and month
            var anniversariesToday = db.UserTbls
                .Where(u => u.MarriageDate.HasValue && u.MarriageDate.Value.Day == currentDay && u.MarriageDate.Value.Month == currentMonth)
                .ToList();

           
            var model = new Tuple<List<UserTbl>, List<UserTbl>>(birthdaysToday, anniversariesToday);

            return View(model);

        }
        public ActionResult upcoming()
        {
            int currentDay = DateTime.Today.Day;
            int currentMonth = DateTime.Today.Month;

            // Fetch all users from the database
            var allUsers = db.UserTbls.ToList();

            // Retrieve users whose birthdays match within the next 15 days
            var upcomingBirthdays = allUsers
                .Where(u => u.DateOfBirth.HasValue &&
                            ((u.DateOfBirth.Value.Month == currentMonth && u.DateOfBirth.Value.Day >= currentDay) ||  // Birthdays in the current month
                            (u.DateOfBirth.Value.Month == currentMonth + 1 && u.DateOfBirth.Value.Day <= 15 - (DateTime.DaysInMonth(DateTime.Today.Year, currentMonth) - currentDay))))  // Birthdays in the next month
                .ToList();

            // Retrieve users whose anniversaries match within the next 15 days
            var upcomingAnniversaries = allUsers
                .Where(u => u.MarriageDate.HasValue &&
                            ((u.MarriageDate.Value.Month == currentMonth && u.MarriageDate.Value.Day >= currentDay) ||  // Anniversaries in the current month
                            (u.MarriageDate.Value.Month == currentMonth + 1 && u.MarriageDate.Value.Day <= 15 - (DateTime.DaysInMonth(DateTime.Today.Year, currentMonth) - currentDay))))  // Anniversaries in the next month
                .ToList();

            var model = new Tuple<List<UserTbl>, List<UserTbl>>(upcomingBirthdays, upcomingAnniversaries);

            return View(model);

        }
    }
}