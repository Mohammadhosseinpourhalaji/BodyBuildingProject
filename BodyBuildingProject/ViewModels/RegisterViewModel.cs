using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام خود را وارد کنید.")]
        [RegularExpression(@"^[آ-یءچ]+([\s][آ-یءچ]+)*$", ErrorMessage = "فقط کاراکتر های فارسی مجاز است")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "تعداد کاراکترهای مجاز نام از {2} تا {1} کاراکتر میباشد")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی خود را وارد کنید.")]
        [RegularExpression(@"^[آ-یءچ]+([\s][آ-یءچ]+)*$", ErrorMessage = "فقط کاراکتر های فارسی مجاز است")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "تعداد کاراکترهای مجاز نام خانوادگی از {2} تا {1} کاراکتر میباشد")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "نام کاربری خود را وارد کنید.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "تعداد کاراکترهای مجاز نام‌کاربری از {2} تا {1} کاراکتر میباشد")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "فقط حروف انگلیسی و اعداد")]
        [Remote("VerifyUserName", "Account", ErrorMessage = "این نام کاربری قبلاً ثبت نام شده است")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "ایمیل خود را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر وارد کنید")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "تعداد کاراکترهای مجاز ایمیل از {2} تا {1} کاراکتر میباشد")]
        [Remote("VerifyEmail", "Account", ErrorMessage = "این ایمیل قبلاً ثبت شده است، از قسمت فراموشی رمز عبور بازیابی کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "پسورد خود را وارد کنید.")]
        [StringLength(20, ErrorMessage = "پسورد باید بین 4 تا 20 کاراکتر باشد", MinimumLength = 4)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,20}$", ErrorMessage = "پسورد باید شامل حداقل 4 حرف، حداقل یک حرف بزرگ انگلیسی، یک حرف کوچک انگلیسی و یک عدد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "پسورد را مجدداً وارد کنید.")]
        [Compare("Password", ErrorMessage = "دو پسورد مطابقت ندارد")]
        [StringLength(20, ErrorMessage = "پسورد باید بین 4 تا 20 کاراکتر باشد", MinimumLength = 4)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "شماره موبایل خود را وارد کنید")]
        [RegularExpression(@"^09[0-9]{9}$", ErrorMessage = "شماره موبایل 11 رقم و با 09 در ابتدای آن وارد شود")]
        public string PhoneNumber { get; set; }
    }
}
