using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.Models
{
    public class TrainType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int Price { get; set; }

        public ICollection<UserTrainInfo> UserTrainInfo { get; set; }
    }
}
