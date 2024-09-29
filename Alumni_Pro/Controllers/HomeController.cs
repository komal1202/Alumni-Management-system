using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Alumni_Pro.Controllers
{
    public class HomeController : Controller
    {
        ADBEntities db = new ADBEntities();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            var userTbls = db.UserTbls.Include(u => u.UserTypeTbl);
            return View(userTbls.ToList());
        }

        [HttpPost]
        public ActionResult LoginUser(string email, string password)
        {
            try
            {
                if (email != null && password != null)
                {
                    var finduser = db.UserTbls.Where(u => u.EmailAddress == email && u.Password == password).ToList();
                    if (finduser.Count == 1)
                    {
                        //Session["UserID"] = db.UserTbls.Where(u => u.EmailAddress == email).Single().UserID;
                        Session["UserID"] = finduser[0].UserID;
                        Session["UserTypeID"] = finduser[0].UserTypeID;
                        Session["FirstName"] = finduser[0].FirstName;
                        Session["MiddleName"] = finduser[0].MiddleName;
                        Session["LastName"] = finduser[0].LastName;
                        Session["AdditionalName"] = finduser[0].AdditionalName;
                        Session["MobileNo"] = finduser[0].MobileNo;
                        Session["AlternateMobileNo"] = finduser[0].AlternateMobileNo;
                        Session["LandlineNo"] = finduser[0].LandlineNo;                             
                        Session["EmailAddress"] = finduser[0].EmailAddress;                        
                        Session["Password"] = finduser[0].Password;                                 
                        Session["ConfirmPassword"] = finduser[0].ConfirmPassword;                   
                        Session["Gender"] = finduser[0].Gender;                                     
                        Session["DateOfBirth"] = finduser[0].DateOfBirth;                           
                        Session["MaritalStatus"] = finduser[0].MaritalStatus;                       
                        Session["MarriageDate"] = finduser[0].MarriageDate;
                        Session["Address"] = finduser[0].Address;
                        Session["City"] = finduser[0].City;
                        Session["State"] = finduser[0].State;
                        Session["Country"] = finduser[0].Country;
                        Session["Description"] = finduser[0].Description;
                        Session["Profession"] = finduser[0].Profession;
                        Session["Hobbies"] = finduser[0].Hobbies;
                        Session["Experience"] = finduser[0].Experience;
                        Session["FamilyInfo"] = finduser[0].FamilyInfo;
                        Session["OtherInfo"] = finduser[0].OtherInfo;
                        Session["Photo"] = finduser[0].Photo;
                        Session["CollegePhoto"] = finduser[0].CollegePhoto;
                        Session["CouplePhoto"] = finduser[0].CouplePhoto;
                        Session["FamilyPhoto"] = finduser[0].FamilyPhoto;




                        string url = string.Empty;
                        if (finduser[0].UserTypeID == 2)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 3)
                        {
                            return RedirectToAction("Index");
                        }

                        else if (finduser[0].UserTypeID == 1)
                        {
                            url = "About";
                        }
                        else
                        {
                            url = "About";
                        }
                        return RedirectToAction(url);
                    }
                    else
                    {
                        Session["UserID"] = string.Empty;
                        Session["UserTypeID"] = string.Empty;
                        Session["FirstName"] = string.Empty;
                        Session["MiddleName"] = string.Empty;
                        Session["LastName"] = string.Empty;
                        Session["AdditionalName"] = string.Empty;
                        Session["MobileNo"] = string.Empty;
                        Session["AlternateMobileNo"] = string.Empty;
                        Session["LandlineNo"] = string.Empty;
                        Session["EmailAddress"] = string.Empty;
                        Session["Password"] = string.Empty;
                        Session["ConfirmPassword"] = string.Empty;
                        Session["Gender"] = string.Empty;
                        Session["DateOfBirth"] = string.Empty;
                        Session["MaritalStatus"] = string.Empty;
                        Session["MarriageDate"] = string.Empty;
                        Session["Address"] = string.Empty;
                        Session["City"] = string.Empty;
                        Session["State"] = string.Empty;
                        Session["Country"] = string.Empty;
                        Session["Description"] = string.Empty;
                        Session["Profession"] = string.Empty;
                        Session["Hobbies"] = string.Empty;
                        Session["Experience"] = string.Empty;
                        Session["FamilyInfo"] = string.Empty;
                        Session["OtherInfo"] = string.Empty;
                        Session["Photo"] = string.Empty;
                        Session["CollegePhoto"] = string.Empty;
                        Session["CouplePhoto"] = string.Empty;
                        Session["FamilyPhoto"] = string.Empty;


                        ViewBag.message = "User name and password is incorrect!";
                    }
                }
                else
                {
                    Session["UserID"] = string.Empty;
                    Session["UserTypeID"] = string.Empty;
                    Session["FirstName"] = string.Empty;
                    Session["MiddleName"] = string.Empty;
                    Session["LastName"] = string.Empty;
                    Session["AdditionalName"] = string.Empty;
                    Session["MobileNo"] = string.Empty;
                    Session["AlternateMobileNo"] = string.Empty;
                    Session["LandlineNo"] = string.Empty;
                    Session["EmailAddress"] = string.Empty;
                    Session["Password"] = string.Empty;
                    Session["ConfirmPassword"] = string.Empty;
                    Session["Gender"] = string.Empty;
                    Session["DateOfBirth"] = string.Empty;
                    Session["MaritalStatus"] = string.Empty;
                    Session["MarriageDate"] = string.Empty;
                    Session["Address"] = string.Empty;
                    Session["City"] = string.Empty;
                    Session["State"] = string.Empty;
                    Session["Country"] = string.Empty;
                    Session["Description"] = string.Empty;
                    Session["Profession"] = string.Empty;
                    Session["Hobbies"] = string.Empty;
                    Session["Experience"] = string.Empty;
                    Session["FamilyInfo"] = string.Empty;
                    Session["OtherInfo"] = string.Empty;
                    Session["Photo"] = string.Empty;
                    Session["CollegePhoto"] = string.Empty;
                    Session["CouplePhoto"] = string.Empty;
                    Session["FamilyPhoto"] = string.Empty;
                    ViewBag.message = "Some unexpected issue occure !";
                }
            }
            catch (Exception)
            {

                Session["UserID"] = string.Empty;
                Session["UserTypeID"] = string.Empty;
                Session["FirstName"] = string.Empty;
                Session["MiddleName"] = string.Empty;
                Session["LastName"] = string.Empty;
                Session["AdditionalName"] = string.Empty;
                Session["MobileNo"] = string.Empty;
                Session["AlternateMobileNo"] = string.Empty;
                Session["LandlineNo"] = string.Empty;
                Session["EmailAddress"] = string.Empty;
                Session["Password"] = string.Empty;
                Session["ConfirmPassword"] = string.Empty;
                Session["Gender"] = string.Empty;
                Session["DateOfBirth"] = string.Empty;
                Session["MaritalStatus"] = string.Empty;
                Session["MarriageDate"] = string.Empty;
                Session["Address"] = string.Empty;
                Session["City"] = string.Empty;
                Session["State"] = string.Empty;
                Session["Country"] = string.Empty;
                Session["Description"] = string.Empty;
                Session["Profession"] = string.Empty;
                Session["Hobbies"] = string.Empty;
                Session["Experience"] = string.Empty;
                Session["FamilyInfo"] = string.Empty;
                Session["OtherInfo"] = string.Empty;
                Session["Photo"] = string.Empty;
                Session["CollegePhoto"] = string.Empty;
                Session["CouplePhoto"] = string.Empty;
                Session["FamilyPhoto"] = string.Empty;
                ViewBag.message = "Some unexpected issue occure !";
            }
            return View("Login");
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

        public ActionResult Logout()
        {
            Session["UserID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["FirstName"] = string.Empty;
            Session["MiddleName"] = string.Empty;
            Session["LastName"] = string.Empty;
            Session["AdditionalName"] = string.Empty;
            Session["MobileNo"] = string.Empty;
            Session["AlternateMobileNo"] = string.Empty;
            Session["LandlineNo"] = string.Empty;
            Session["EmailAddress"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["ConfirmPassword"] = string.Empty;
            Session["Gender"] = string.Empty;
            Session["DateOfBirth"] = string.Empty;
            Session["MaritalStatus"] = string.Empty;
            Session["MarriageDate"] = string.Empty;
            Session["Address"] = string.Empty;
            Session["City"] = string.Empty;
            Session["State"] = string.Empty;
            Session["Country"] = string.Empty;
            Session["Description"] = string.Empty;
            Session["Profession"] = string.Empty;
            Session["Hobbies"] = string.Empty;
            Session["Experience"] = string.Empty;
            Session["FamilyInfo"] = string.Empty;
            Session["OtherInfo"] = string.Empty;
            Session["Photo"] = string.Empty;
            Session["CollegePhoto"] = string.Empty;
            Session["CouplePhoto"] = string.Empty;
            Session["FamilyPhoto"] = string.Empty;


            return RedirectToAction("Login");
        }
    }
}