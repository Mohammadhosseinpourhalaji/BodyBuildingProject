using BodyBuildingProject.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.Models
{
    public class TrainSchedule
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public bool isPaid { get; set; } = false;
        public DateTime DateAdded { get; set; }
        public bool Status { get; set; } = false;

        public UserTrainInfo UserTrainInfo { get; set; }

        //public int DayId { get; set; }
        //[ForeignKey("DayId")]
        //public DaysOfWeek DaysOfWeek { get; set; }

        //public string Title { get; set; }
        //public string Text { get; set; }
        //public DateTime Time { get; set; }
    }
}
