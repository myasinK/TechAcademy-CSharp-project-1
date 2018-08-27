using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TACSProject.Models
{
    public class ClientInfo
    {
        public int ID { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public int Tickets { get; set; }
        public string Dob { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public int CarYear { get; set; }
        public string FullCoverage { get; set; }
        public string Dui { get; set; }
        public string Email { get; set; }
        public string Total { get; set; }
    }
}