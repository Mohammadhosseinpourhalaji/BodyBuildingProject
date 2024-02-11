using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.Models
{
    public class UserTrainSchedule
    {
        public int Id { get; set; }

        public int? TrainScheduleId { get; set; }
        [ForeignKey("TrainScheduleId")]
        public TrainSchedule TrainSchedule { get; set; }

        public int? DayId { get; set; }
        [ForeignKey("DayId")]
        public DaysOfWeek DaysOfWeek { get; set; }

        public string Time { get; set; }

        public string TrainText { get; set; }
    }
}
