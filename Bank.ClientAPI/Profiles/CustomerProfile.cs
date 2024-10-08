using AutoMapper;
using Bank.UseCases.Customer;
using Bank.UseCases.Customer.CommandCreateCustomer;
using Bank.UseCases.Customer.CommandUpdateCustomer;
using Entities;

namespace Bank.ClientAPI.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() { 
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<CreateCustomerCommand, Customer>().ReverseMap();
            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<UpdateCustomerCommand, Customer>().ReverseMap();
        }
    }
}
