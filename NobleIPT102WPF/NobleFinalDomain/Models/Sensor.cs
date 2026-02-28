using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NobleFinalDomain.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SensorType { get; set; }
        public string Location { get; set; }
    }
}
