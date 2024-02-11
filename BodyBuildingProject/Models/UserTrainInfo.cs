using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.Models
{
    public class UserTrainInfo
    {
        public int Id { get; set; }

        public int TrainScheduleId { get; set; }
        [ForeignKey("TrainScheduleId")]
        public TrainSchedule TrainSchedule { get; set; }

        public int Age { get; set; }
        //[Column(TypeName = "decimal(18,4)")]
        public decimal Height { get; set; }
        //[Column(TypeName = "decimal(18,4)")]
        public decimal Weight { get; set; }

        //[Column(TypeName = "decimal(18,4)")]
        public decimal Waist { get; set; } // Dore Kamar
        //[Column(TypeName = "decimal(18,4)")]
        public decimal Chest { get; set; } // Dore Sine
        //[Column(TypeName = "decimal(18,4)")]
        public decimal Bracer { get; set; } // Dore Moch
        //[Column(TypeName = "decimal(18,4)")]
        public decimal Arm { get; set; } // Dore Bazoo
        //[Column(TypeName = "decimal(18,4)")]
        public decimal Thighs { get; set; } //Ran Pa

        public string Comment { get; set; }

        public int TrainTypeId { get; set; }
        [ForeignKey("TrainTypeId")]
        public TrainType TrainType { get; set; }

    }
}
