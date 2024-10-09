using AutoMapper;
using DataLogic.BankAccountRepository.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Transaction.CommandCreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, bool>
    {
        private readonly IBankTransactionRepository _repo;
        private readonly IMapper _mapper;
        public CreateTransactionCommandHandler(IBankTransactionRepository repo, IMapper mapper) {
            _mapper = mapper;
            _repo = repo;
        }

        public Task<bool> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
