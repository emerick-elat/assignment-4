using AutoMapper;
using Bank.UseCases.Transaction;
using Entities;

namespace Bank.ClientAPI.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ForMember(m => m.Type, src => src.MapFrom(t => t.Type == TransactionType.Deposit ? "Deposit" : "Withdrawal"))
                .ForMember(m => m.TransactionDate, src => src.MapFrom(dest => dest.TransactionDate.ToLongDateString()));


        }
    }
}
