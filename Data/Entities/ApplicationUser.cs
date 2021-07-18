using System;
using Microsoft.AspNetCore.Identity;

namespace TestDev.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsRoot { get; set; }
        public string ConfirmToken { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsDemoLogin { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LoginExpire { get; set; }
        public string Company { get; set; }
        public int NumOfLogins { get; set; }
        
    }
}
