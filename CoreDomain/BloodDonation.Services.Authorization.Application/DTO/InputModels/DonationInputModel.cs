namespace BloodDonation.Services.Donations.Application.DTO.InputModels
{
    public class DonationInputModel
    {
        public int DonorId { get;  set; }
        public string DonorName { get; set; } = string.Empty;
        public int QuantityML { get;  set; }
    }
}
