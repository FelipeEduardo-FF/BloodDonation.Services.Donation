using AutoMapper;
using BloodDonation.Services.Donations.Application.DTO.InputModels;
using BloodDonation.Services.Donations.Application.DTO.ViewModels;
using BloodDonation.Services.Donations.Domain.Entities;

namespace BloodDonation.Services.Donations.Application.DTO
{
    public class EntityToDTOMapper:Profile
    {
        public EntityToDTOMapper()
        {
            CreateMap<DonationInputModel, Donation>().ReverseMap();
            CreateMap<DonationViewModel, Donation>().ReverseMap();

        }
    }
}
