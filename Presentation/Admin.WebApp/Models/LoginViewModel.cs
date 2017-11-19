﻿using System.ComponentModel.DataAnnotations;
using Admin.WebApp.Resources;

namespace Admin.WebApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Label_Login", ResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Label_Password", ResourceType = typeof(Resource))]
        public string Password { get; set; }

        [Display(Name = "Label_RememberMe", ResourceType = typeof(Resource))]
        public bool RememberMe { get; set; }
    }
}
