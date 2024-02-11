using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "یوزرنیم خود را وارد کنید")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "پسورد خود را وارد کنید")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
