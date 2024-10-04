using Shared.Domain.Entities;

namespace BloodDonation.Services.Donations.Domain.Entities
{
    public class Donation:EntityIdInt
    {
        public Donation(int donorId, int quantityML,string donorName)
        {
            DonorId = donorId;
            DonationDate = DateTime.Now;
            QuantityML = quantityML;
            DonorName = donorName;
        }

        public int DonorId { get;private set; }
        public string DonorName { get; private set; }
        public DateTime DonationDate { get; private set; }
        public int QuantityML { get; private set; }
    }
}
