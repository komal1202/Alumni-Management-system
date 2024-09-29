using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Alumni_Pro.Controllers
{
    public class UserLoginController : Controller
    {
        ADBEntities db = new ADBEntities();
        // GET: UserLogin
        public ActionResult Index()
        {
            var userTbls = db.UserTbls.Include(u => u.UserTypeTbl);
            return View(userTbls.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTbl userTbl = db.UserTbls.Find(id);
            if (userTbl == null)
            {
                return HttpNotFound();
            }
            return View(userTbl);
        }
        public ActionResult UserDetails()
        {
            // Check if the user is logged in
            if (Session["UserID"] != null)
            {
                int loggedInUserId = Convert.ToInt32(Session["UserID"]);

                // Retrieve user details based on the logged-in user's ID
                var userDetails = db.UserTbls.FirstOrDefault(u => u.UserID == loggedInUserId);

                // Check if user details exist
                if (userDetails != null)
                {
                    // Pass the user details to the view for display
                    return View(userDetails);
                }
                else
                {
                    // User details not found
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                // User is not logged in
                return View("LoginUser");
            }
        }
        public ActionResult About(string cityCount)
        {
            int totalRegisteredUsers = db.UserTbls.Count(); // Assuming "Users" is your DbSet representing users

            // Pass the total count to the view
            ViewBag.TotalRegisteredUsers = totalRegisteredUsers;




            var cityCounts = new Dictionary<string, int>();
            var professionCounts = new Dictionary<string, int>();

            // Calculate city-wise counts
            cityCounts["Pune"] = db.UserTbls.Count(u => u.City == "Pune");
            cityCounts["Mumbai"] = db.UserTbls.Count(u => u.City == "Mumbai");
            cityCounts["Nashik"] = db.UserTbls.Count(u => u.City == "Nashik");
            cityCounts["Nagpur"] = db.UserTbls.Count(u => u.City == "Nagpur");

            // Calculate profession-wise counts
            professionCounts["Software Developer"] = db.UserTbls.Count(u => u.Profession == "Software Developer");
            professionCounts["Designer"] = db.UserTbls.Count(u => u.Profession == "Designer");
            professionCounts["Accountant"] = db.UserTbls.Count(u => u.Profession == "Accountant");
            professionCounts["Engineer"] = db.UserTbls.Count(u => u.Profession == "Engineer");

            // Pass the counts for each city and profession to the view
            ViewBag.CityCounts = cityCounts;
            ViewBag.ProfessionCounts = professionCounts;

            return View();

        }
        public ActionResult City(string city)
        {
            var cityCounts = new Dictionary<string, int>();

            cityCounts["Pune"] = db.UserTbls.Count(u => u.City == "Pune");
            cityCounts["Mumbai"] = db.UserTbls.Count(u => u.City == "Mumbai");
            cityCounts["Nashik"] = db.UserTbls.Count(u => u.City == "Nashik");
            cityCounts["Nagpur"] = db.UserTbls.Count(u => u.City == "Nagpur");

            // Pass the counts for each city to the view
            ViewBag.CityCounts = cityCounts;
            return View(cityCounts);

        }
        public ActionResult Profession(string profession)
        {
            var professionCounts = new Dictionary<string, int>();

            professionCounts["Software Developer"] = db.UserTbls.Count(u => u.Profession == "Software Developer");
            professionCounts["Designer"] = db.UserTbls.Count(u => u.Profession == "Designer");
            professionCounts["Accountant"] = db.UserTbls.Count(u => u.Profession == "Accountant");
            professionCounts["Engineer"] = db.UserTbls.Count(u => u.Profession == "Engineer");

            // Pass the counts for each city to the view
            ViewBag.ProfessionCounts = professionCounts;
            return View(professionCounts);

        }
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTbl userTbl = db.UserTbls.Find(id);
            if (userTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserTypeID = new SelectList(db.UserTypeTbls, "UserTypeID", "UserTypeName", userTbl.UserTypeID);
            return View(userTbl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserTbl userTbl, HttpPostedFileBase imageFile)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Entry(userTbl).State = EntityState.Modified;
                if (userTbl.File != null && userTbl.File.ContentLength > 0)
                {

                    string filename = Path.GetFileName(userTbl.File.FileName);
                    string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                    userTbl.Photo = "~/Images/" + _filename;
                    userTbl.File.SaveAs(path);
                }
                // Save the college photo
                if (userTbl.CollegePhotoFile != null && userTbl.CollegePhotoFile.ContentLength > 0)
                {
                    string collegeFilename = Path.GetFileName(userTbl.CollegePhotoFile.FileName);
                    string collegePath = Path.Combine(Server.MapPath("~/CollegePhoto/"), collegeFilename);
                    userTbl.CollegePhoto = "~/CollegePhoto/" + collegeFilename;
                    userTbl.CollegePhotoFile.SaveAs(collegePath);
                }

                // Save the couple photo
                if (userTbl.CouplePhotoFile != null && userTbl.CouplePhotoFile.ContentLength > 0)
                {
                    string coupleFilename = Path.GetFileName(userTbl.CouplePhotoFile.FileName);
                    string couplePath = Path.Combine(Server.MapPath("~/CouplePhoto/"), coupleFilename);
                    userTbl.CouplePhoto = "~/CouplePhoto/" + coupleFilename;
                    userTbl.CouplePhotoFile.SaveAs(couplePath);
                }

                // Save the family photo
                if (userTbl.FamilyPhotoFile != null && userTbl.FamilyPhotoFile.ContentLength > 0)
                {
                    string familyFilename = Path.GetFileName(userTbl.FamilyPhotoFile.FileName);
                    string familyPath = Path.Combine(Server.MapPath("~/FamilyPhoto/"), familyFilename);
                    userTbl.FamilyPhoto = "~/FamilyPhoto/" + familyFilename;
                    userTbl.FamilyPhotoFile.SaveAs(familyPath);
                }
                
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.UserTypeID = new SelectList(db.UserTypeTbls, "UserTypeID", "UserTypeName", userTbl.UserTypeID);
            return View(userTbl);
        }

    }
}