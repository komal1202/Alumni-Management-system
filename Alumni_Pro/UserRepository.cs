using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni_Pro
{
    public class UserRepository
    {
        private ADBEntities db;

        public UserRepository()
        {
            this.db = new ADBEntities();
        }

        public bool IsEmailExist(string email)
        {
            return db.UserTbls.Any(u => u.EmailAddress == email);
        }
    }
}