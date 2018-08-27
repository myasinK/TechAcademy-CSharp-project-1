using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using TACSProject.Controllers;

namespace TACSProject.ViewModels
{

    public class CustomerConfirm
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int Tickets { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public int BirthYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string FullCoverage { get; set; }
        public string Dui { get; set; }
        public string Email { get; set; }
        public int Age => CalcAge(Month, Day, BirthYear);
        public int Basic = 50;
        
        public int AgeFee => CheckAge(Basic, Month, Day, BirthYear);

        public int CarYearFee => CheckMakeYear(Year);

        public int MakeFee => ( Make.ToLower() =="porsche" ) ? 25 : 0;

        public int ModelFee => (Make.ToLower() == "porsche" && Model.ToLower() == "911 carrera") ? 25 : 0;

        public int TicketFee => TicketCheck(Tickets);

        public int TicketCheck(int tickets)
        {
            return tickets * 10;
        }

        public double DUIFee => ( Dui == "yes" ) ? (AgeFee + CarYearFee + MakeFee + ModelFee + TicketFee) * 0.25 : 0;

        public double CoverageFee => ( FullCoverage == "yes" ) ? ( AgeFee + CarYearFee + MakeFee + ModelFee + TicketFee + DUIFee ) * 0.5 : 0;

        public double Total => AgeFee + CarYearFee + MakeFee + ModelFee + TicketFee + DUIFee + CoverageFee + Basic;

        // following method takes dob input and converts to age in years
        public int CalcAge(string m, string d, int y)
        {
            DateTime born = new DateTime(y, Convert.ToInt32(m), Convert.ToInt32(d));
            DateTime now = DateTime.Now;
            return (now - born).Days / 365;
        }

        public int AgeInYears => CalcAge(Month, Day, BirthYear);

        public int CheckAge(int basic, string m, string d, int y, int add = 0)
        {
            var years = this.CalcAge(m, d, y);
            if (years > 0 && years < 18)
            {
                add += 100;
            }
            else if (years >= 18 && years < 25)
            {
                add += 25;
            }
            else if (years >= 25 && years < 100)
            {
                add += 0;
            }
            else if (years > 100)
            {
                add += 25;
            }

            return add; 
        }

        public int CheckMakeYear(int y)
        {
            if (y < 2000 || y > 2015)
            {
                return 25;
            }
            else
            {
                return 0;
            }
        }

    }
}

