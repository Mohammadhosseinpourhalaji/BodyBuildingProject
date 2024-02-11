using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BodyBuildingProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string Description_1 { get; set; }
        public string Description_2 { get; set; }
        public string Description_3 { get; set; }

        public byte[] Img { get; set; }
    }
}
