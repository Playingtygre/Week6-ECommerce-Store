﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Emusic.Models.ViewModels
{
        public class LoginViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

             [Required]
             [DataType(DataType.Password)]
              public string Password { get; set; }

             [Display(Name = "Keep Me Signed In")]
              public bool KeepSignedIn;
        }       
    
}
