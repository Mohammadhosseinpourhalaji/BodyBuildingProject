using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.Models
{
    public class UserBMI
    {
        public int Id { get; set; }
        public int UserInfoId { get; set; }
        [ForeignKey("UserInfoId")]
        public Users UserInfo { get; set; }

        public int Age { get; set; }
        public double Height { get; set; }

        public double Range_1 { get; set; }
        public double Range_2 { get; set; }
        public double Range_3 { get; set; }
    }
}
