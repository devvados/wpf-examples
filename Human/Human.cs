using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_Lab_D_1_V_1
{
    [Serializable]
    class Human
    {
        public String Name   { get; set; }
        public String Gender { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public DateTime Date { get; set; }

        public Human()
        { 
            Name = "defaulName";
            Gender = "М";
            Height = 1.5;
            Weight = 60;
            Date = new DateTime(1995, 09, 28);
        }


    }
}
