using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Core.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
