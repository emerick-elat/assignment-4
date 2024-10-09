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

        public async Task<bool> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTransactionCommandValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var transaction = _mapper.Map<Entities.Transaction>(request);
                return await _repo.CreateTransaction(transaction);
            }
            return false;
        }
    }
}
