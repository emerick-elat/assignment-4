using MediatR;

namespace Bank.UseCases.Customer.CommandUpdateCustomer
{
    public class UpdateCustomerCommand : IRequest
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
