using AutoMapper;
using Bank.ClientAPI.Models;
using Entities;

namespace Bank.ClientAPI.Profiles
{
    public class ScheduledPaymentProfile : Profile
    {
        public ScheduledPaymentProfile() { 
            CreateMap<ScheduledPayment, ScheduledPaymentDto>()
                .ForMember(src => src.NumberOfPayments, x => x.MapFrom(dest 
                => dest.ScheduledPaymentItems != null 
                ? dest.ScheduledPaymentItems.Count : 0))
                .ForMember(src => src.LastPaymentDate, opt => opt.MapFrom(dest => dest.ScheduledPaymentItems != null ?
                dest.ScheduledPaymentItems.LastOrDefault()!.PaymentDate : DateTime.Now));
        }
    }
}
