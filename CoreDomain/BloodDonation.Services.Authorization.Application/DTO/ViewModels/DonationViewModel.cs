using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Services.Donations.Application.DTO.ViewModels
{
    public class DonationViewModel
    {
        public int Id { get; set; }
        public int DonorId { get;  set; }
        public DateTime DonationDate { get;  set; }
        public int QuantityML { get;  set; }
    }
}
