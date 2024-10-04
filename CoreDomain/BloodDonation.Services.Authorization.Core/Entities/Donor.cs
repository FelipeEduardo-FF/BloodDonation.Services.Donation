using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Services.Donations.Domain.Entities
{
    public class Donor
    {
        public Donor(DateTime birthDate, string gender, double weight, string bloodType, string rhFactor)
        {
            BirthDate = birthDate;
            Gender = gender;
            Weight = weight;
            BloodType = bloodType;
            RhFactor = rhFactor;
        }

        public DateTime BirthDate { get;  set; }
        public string Gender { get;  set; }
        public double Weight { get;  set; }
        public string BloodType { get;  set; }
        public string RhFactor { get;  set; }
    }
}
