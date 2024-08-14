

using Booking.Core.Interfaces.Persistence;
using MediatR;
using System.Transactions;


namespace Booking.Application.Mediatr
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {

        private readonly IUnitOfWork _unitOfWork;


        public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
            )
        {
            if (IsNotCommand())
                return await next();

            //If any operation fails or an exception is thrown, the transaction is rolled back automatically when the scope is disposed.
            // The TransactionScopeAsyncFlowOption.Enabled option allows the transaction scope to flow across asynchronous calls. This is necessary if you’re using async/await in your methods.
            // Scope-Based: The transaction is automatically committed or rolled back when the TransactionScope is disposed, which happens when the scope exits, usually at the end of a using block.
            using var transactionScoop = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            
            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transactionScoop.Complete();

            return response;


         
        }


        private static bool IsNotCommand()
        {
            return !typeof(TRequest).Name.EndsWith("Command");
        }
    }
}
