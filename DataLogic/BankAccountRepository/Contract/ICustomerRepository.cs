using DataLogic.Generic.Contract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.BankAccountRepository.Contract
{
    public interface ICustomerRepository : IDataRepository<Customer>
    {
        Task<ICollection<Customer>> GetPaginatedResults(int PageSize, int PageNumber);
    }
}
