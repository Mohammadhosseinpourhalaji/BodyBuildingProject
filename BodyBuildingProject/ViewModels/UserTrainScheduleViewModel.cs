using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.ViewModels
{
    public class UserTrainScheduleViewModel
    {
        public List<int> DayId { get; set; }
        public List<string> Time { get; set; }
        public List<string> TrainText { get; set; }
    }
}
