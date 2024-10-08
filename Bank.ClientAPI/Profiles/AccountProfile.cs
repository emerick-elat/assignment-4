﻿using AutoMapper;
using Bank.UseCases.Account;
using Bank.UseCases.Account.CommandCreateAccount;
using Entities;

namespace Bank.ClientAPI.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountCommand, Account>();

            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.CustomerFullname, opt => opt.MapFrom(src => src.Customer != null 
                ? $"{src.Customer.FirstName} {src.Customer.LastName}" : string.Empty))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => $"EUR {src.GetBalance()}"));

/*            CreateMap<CreateAccountCommand, Account>()
                .ForMember(dest => dest.Customer.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Customer.LastName, opt => opt.MapFrom(src => src.LastName));*/
        }
    }
}
