using System.Transactions;
using MediatR;
using Microsoft.Extensions.Logging;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.PipelineBehavior;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;

    public TransactionBehaviour(
        IUnitOfWork unitOfWork,
        ILogger<TransactionBehaviour<TRequest, TResponse>> logger
    )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (IsNotCommand(request))
        {
            return await next();
        }

        //If any operation fails or an exception is thrown, the transaction is rolled back automatically when the scope is disposed.
        // The TransactionScopeAsyncFlowOption.Enabled option allows the transaction scope to flow across asynchronous calls. This is necessary if you’re using async/await in your methods.
        // Scope-Based: The transaction is automatically committed or rolled back when the TransactionScope is disposed, which happens when the scope exits, usually at the end of a using block.
        using var transactionScoop = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var response = await next();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        transactionScoop.Complete();

        return response;
    }


    private static bool IsNotCommand(TRequest request)
    {
        return request is IQuery<TResponse>;
    }
}
