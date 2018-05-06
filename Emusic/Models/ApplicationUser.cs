﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Emusic.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

       
        [Display(Name = "Rock Roll TYpe")]
        public string MusicType { get; set; }

        public long? CurrentBasketId { get; set; }
    }

    public static class ApplicationRoles
    {
        public static string Admin => "Admin";
        public static string AdminNormalized => Admin.ToUpper();

        public static string Member => "Member";
        public static string MemberNormalized => Member.ToUpper();
    }

    public enum MusicType : Int32
    {
        Rock,
        Roll,
    }

}
