using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Models
{
    public class AppUser : IdentityUser
    {
        public bool? Admin { get; set; }

        public string FirstName { get; set; }
    }
}
