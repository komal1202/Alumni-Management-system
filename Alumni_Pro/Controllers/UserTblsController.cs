using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Alumni_Pro.ViewModels;
using DatabaseContext;
using Microsoft.Ajax.Utilities;

namespace Alumni_Pro.Controllers
{
    public class UserTblsController : Controller
    {
        private ADBEntities db = new ADBEntities();

        // GET: UserTbls
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userTbls = db.UserTbls.Include(u => u.UserTypeTbl);
            return View(userTbls.ToList());
        }

        //GET: UserTbls/Details/5
        public ActionResult Details(int? id)
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
            return View(userTbl);
        }

        // GET: UserTbls/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.UserTypeID = new SelectList(db.UserTypeTbls, "UserTypeID", "UserTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserTbl userTbl)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            string filename = Path.GetFileName(userTbl.File.FileName);
            string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
            string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
            userTbl.Photo = "~/Images/" + _filename;
            userTbl.File.SaveAs(path);

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

            // Add the user record to the database
            db.UserTbls.Add(userTbl);
            if (db.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }


            ViewBag.UserTypeID = new SelectList(db.UserTypeTbls, "UserTypeID", "TypeName", userTbl.UserTypeID);
            return View(userTbl);

        }

        // GET: UserTbls/Edit/5
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
        public ActionResult Edit(UserTbl userTbl)
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

        // GET: UserTbls/Delete/5
        public ActionResult Delete(int? id)
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
            return View(userTbl);
        }

        // POST: UserTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            UserTbl userTbl = db.UserTbls.Find(id);
            db.UserTbls.Remove(userTbl);
            db.SaveChanges();
            return RedirectToAction("Index");
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

        public ActionResult VerifyEmail()
        {
            return View();
        }

        // POST: User/VerifyEmail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                ViewBag.ErrorMessage = "Please enter an email address.";
                return View();
            }

            var user = db.UserTbls.FirstOrDefault(u => u.EmailAddress == emailAddress);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Email address not found.";
                return View();
            }
          
        
            // If email exists, redirect to the page to verify other details or change password
            return RedirectToAction("ResetPassword", new { userId = user.UserID });
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordVM obj, int mobile)
        {
            var user = db.UserTbls.FirstOrDefault(u => u.MobileNo == mobile);

            if (user != null && user.MobileNo == obj.Mobile)
            {
                // Check if the old password matches the one in the database
                if (user.MobileNo == obj.Mobile)
                {
                    // Update the password to the old password
                    user.Password = obj.NewPassword;
                    user.ConfirmPassword = obj.ConfirmNewPassword;

                    // Save changes to the database
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.Message = "Your password has been updated successfully.";
                }
                else
                {
                    ViewBag.Message = "not match to password";
                }
            }
            else
            {
                ViewBag.Message = "Invalid mobile number.";
            }

            return RedirectToAction("Login", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
