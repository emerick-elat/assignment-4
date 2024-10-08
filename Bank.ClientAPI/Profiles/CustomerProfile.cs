using AutoMapper;
using Bank.UseCases.Customer;
using Bank.UseCases.Customer.CommandCreateCustomer;
using Entities;

namespace Bank.ClientAPI.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() { 
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CreateCustomerCommand, Customer>();
        }
    }
}
