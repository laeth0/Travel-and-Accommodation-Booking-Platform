
//public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : class
//    where TResponse : Result
//{
//    private readonly IUnitOfWork _unitOfWork;


//    public TransactionBehaviour(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

//    public async Task<TResponse> Handle(
//        TRequest request,
//        RequestHandlerDelegate<TResponse> next,
//        CancellationToken cancellationToken
//    )
//    {
//        
//        if (IsNotCommand(request))
//        {
//            return await next();
//        }

//        //If any operation fails or an exception is thrown, the transaction is rolled back automatically when the scope is disposed.
//        // The TransactionScopeAsyncFlowOption.Enabled option allows the transaction scope to flow across asynchronous calls. This is necessary if you’re using async/await in your methods.
//        // Scope-Based: The transaction is automatically committed or rolled back when the TransactionScope is disposed, which happens when the scope exits, usually at the end of a using block.
//        using var transactionScoop = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

//        var response = await next();

//        await _unitOfWork.SaveChangesAsync(cancellationToken);

//        if (response.IsSuccess)
//        {
//            transactionScoop.Complete();
//            return response;
//        }

//        transactionScoop.Dispose();
//        return response;

//    }


//    private static bool IsNotCommand(TRequest request)
//    {
//        return request is IQuery<TResponse>;
//    }
//}
using MediatR;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Application.PipelineBehavior;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionBehaviour(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

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

        var strategy = _unitOfWork.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(
            state: (request, next),
            operation: async (context, state, ct) =>
            {
                var (req, nxt) = state;
                using var transaction = await _unitOfWork.BeginTransactionAsync(ct);
                try
                {
                    var response = await nxt();
                    if (response.IsSuccess)
                    {
                        await _unitOfWork.SaveChangesAsync(ct);
                        await transaction.CommitAsync(ct);
                    }
                    else
                    {
                        await transaction.RollbackAsync(ct);
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(ct);
                    throw;
                }
            },
            verifySucceeded: null,
            cancellationToken: cancellationToken
        );
    }


    private static bool IsNotCommand(TRequest request)
    {
        return request is IQuery<TResponse>;
    }
}
