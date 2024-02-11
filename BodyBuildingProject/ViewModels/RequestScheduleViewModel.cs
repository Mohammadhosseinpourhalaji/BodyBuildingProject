using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.ViewModels
{
    public class RequestScheduleViewModel
    {
        [Required(ErrorMessage = "فیلد بالا را پر کنید")]
        [RegularExpression(@"^[^./w/s]+$", ErrorMessage = "سن یک عدد صحیح است")]
        public int Age { get; set; }
        [Required(ErrorMessage = "فیلد بالا را پر کنید")]
        [RegularExpression(@"^[0-9]+([\/]?[0-9]+)*$", ErrorMessage = "الگو برای وارد کردن عدد اعشاری: عدد/عدد")]
        public string Height { get; set; }
        [Required(ErrorMessage = "فیلد بالا را پر کنید")]
        [RegularExpression(@"^[0-9]+([\/]?[0-9]+)*$", ErrorMessage = "الگو برای وارد کردن عدد اعشاری: عدد/عدد")]
        public string Weight { get; set; }
        [Required(ErrorMessage = "فیلد بالا را پر کنید")]
        [RegularExpression(@"^[0-9]+([\/]?[0-9]+)*$", ErrorMessage = "الگو برای وارد کردن عدد اعشاری: عدد/عدد")]
        public string Waist { get; set; } // Dore Kamar
        [Required(ErrorMessage = "فیلد بالا را پر کنید")]
        [RegularExpression(@"^[0-9]+([\/]?[0-9]+)*$", ErrorMessage = "الگو برای وارد کردن عدد اعشاری: عدد/عدد")]
        public string Chest { get; set; } // Dore Sine
        [Required(ErrorMessage = "فیلد بالا را پر کنید")]
        [RegularExpression(@"^[0-9]+([\/]?[0-9]+)*$", ErrorMessage = "الگو برای وارد کردن عدد اعشاری: عدد/عدد")]
        public string Bracer { get; set; } // Dore Moch
        [Required(ErrorMessage = "فیلد بالا را پر کنید")]
        [RegularExpression(@"^[0-9]+([\/]?[0-9]+)*$", ErrorMessage = "الگو برای وارد کردن عدد اعشاری: عدد/عدد")]
        public string Arm { get; set; } // Dore Bazoo
        [Required(ErrorMessage = "فیلد بالا را پر کنید")]
        [RegularExpression(@"^[0-9]+([\/]?[0-9]+)*$", ErrorMessage = "الگو برای وارد کردن عدد اعشاری: عدد/عدد")]
        public string Thighs { get; set; } //Ran Pa

        public string Comment { get; set; }

        public int TrainTypeId { get; set; }
    }
}
