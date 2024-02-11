using BodyBuildingProject.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        //public string Name { get; set; }
        //public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Ncode { get; set; }
        public string Address { get; set; }

    }
}
