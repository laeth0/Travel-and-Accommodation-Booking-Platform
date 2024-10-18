namespace TravelAccommodationBookingPlatform.Domain.Shared.MaybePattern;
public static class MaybeExtensions
{



    /// <summary>
    /// Matches the current <see cref="Maybe{TIn}"/> object to a value of type <typeparamref name="TOut"/> 
    /// by executing one of two provided functions.
    /// If the <paramref name="maybe"/> has a value, the <paramref name="onSuccess"/> function is invoked 
    /// with the value and its result is returned. If the <paramref name="maybe"/> has no value, the <paramref name="onFailure"/> 
    /// function is invoked and its result is returned.
    /// </summary>
    /// <typeparam name="TIn">The type of the value contained in the <see cref="Maybe{TIn}"/>.</typeparam>
    /// <typeparam name="TOut">The return type of the match result.</typeparam>
    /// <param name="maybe">The <see cref="Maybe{TIn}"/> instance to match.</param>
    /// <param name="onSuccess">A function that processes the value of <typeparamref name="TIn"/> and returns a <typeparamref name="TOut"/> result when there is a value.</param>
    /// <param name="onFailure">A function that returns a <typeparamref name="TOut"/> result when the <paramref name="maybe"/> has no value.</param>
    /// <returns>The result of the <paramref name="onSuccess"/> function if the <paramref name="maybe"/> has a value; 
    /// otherwise, the result of the <paramref name="onFailure"/> function.</returns>
    public static TOut Match<TIn, TOut>(this Maybe<TIn> maybe, Func<TIn, TOut> onSuccess, Func<TOut> onFailure)
    {
        return maybe.HasValue ? onSuccess(maybe.Value) : onFailure();
    }




    /// <summary>
    /// Maps the value of the current <see cref="Maybe{TIn}"/> object to another value using the specified <paramref name="mapper"/> function.
    /// If the <paramref name="maybe"/> has a value, applies the <paramref name="mapper"/> function and returns a new <see cref="Maybe{TOut}"/> 
    /// containing the result. If the <paramref name="maybe"/> does not have a value, returns <see cref="Maybe{TOut}.None"/>.
    /// </summary>
    /// <typeparam name="TIn">The type of the input value contained in the <see cref="Maybe{TIn}"/>.</typeparam>
    /// <typeparam name="TOut">The type of the value returned by the <paramref name="mapper"/> function.</typeparam>
    /// <param name="maybe">The <see cref="Maybe{TIn}"/> instance to transform.</param>
    /// <param name="mapper">A function that takes a <typeparamref name="TIn"/> and returns a <typeparamref name="TOut"/>.</param>
    /// <returns>A <see cref="Maybe{TOut}"/> containing the result of the transformation if <paramref name="maybe"/> has a value; 
    /// otherwise, <see cref="Maybe{TOut}.None"/>.</returns>
    public static Maybe<TOut> Map<TIn, TOut>(this Maybe<TIn> maybe, Func<TIn, TOut> mapper)
    {
        return maybe.HasValue ? Maybe<TOut>.From(mapper(maybe.Value)) : Maybe<TOut>.None;
    }






    /// <summary>
    /// Binds the value of the current <see cref="Maybe{TIn}"/> object to another <see cref="Maybe{TOut}"/> 
    /// using the specified <paramref name="binder"/> function.
    /// If the <paramref name="maybe"/> has a value, applies the <paramref name="binder"/> function and returns 
    /// the result. If the <paramref name="maybe"/> does not have a value, returns <see cref="Maybe{TOut}.None"/>.
    /// </summary>
    /// <typeparam name="TIn">The type of the input value contained in the <see cref="Maybe{TIn}"/>.</typeparam>
    /// <typeparam name="TOut">The type of the value wrapped by the <see cref="Maybe{TOut}"/> returned by the <paramref name="binder"/> function.</typeparam>
    /// <param name="maybe">The <see cref="Maybe{TIn}"/> instance to bind.</param>
    /// <param name="binder">A function that takes a <typeparamref name="TIn"/> and returns a <see cref="Maybe{TOut}"/>.</param>
    /// <returns>A <see cref="Maybe{TOut}"/> returned by the <paramref name="binder"/> function if <paramref name="maybe"/> has a value; 
    /// otherwise, <see cref="Maybe{TOut}.None"/>.</returns>
    public static Maybe<TOut> Bind<TIn, TOut>(this Maybe<TIn> maybe, Func<TIn, Maybe<TOut>> binder)
    {
        return maybe.HasValue ? binder(maybe.Value) : Maybe<TOut>.None;
    }





    /// <summary>
    /// Filters the value of the current <see cref="Maybe{TValue}"/> based on a predicate function.
    /// If the <paramref name="maybe"/> has a value and the specified <paramref name="predicate"/> evaluates to true, 
    /// the same <see cref="Maybe{TValue}"/> is returned. If the predicate evaluates to false, or if the <paramref name="maybe"/> 
    /// does not have a value, returns <see cref="Maybe{TValue}.None"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the <see cref="Maybe{TValue}"/>.</typeparam>
    /// <param name="maybe">The <see cref="Maybe{TValue}"/> instance to filter.</param>
    /// <param name="predicate">A function that evaluates the value of <typeparamref name="TValue"/> to a boolean result.</param>
    /// <returns>The current <see cref="Maybe{TValue}"/> if it has a value and the <paramref name="predicate"/> evaluates to true; 
    /// otherwise, <see cref="Maybe{TValue}.None"/>.</returns>
    public static Maybe<TValue> Where<TValue>(this Maybe<TValue> maybe, Func<TValue, bool> predicate)
    {
        return maybe.HasValue && predicate(maybe.Value) ? maybe : Maybe<TValue>.None;
    }



    /// <summary>
    /// Returns the value contained in the current <see cref="Maybe{TValue}"/> if it exists, or throws the specified <paramref name="exception"/> if it does not.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the <see cref="Maybe{TValue}"/>.</typeparam>
    /// <param name="maybe">The <see cref="Maybe{TValue}"/> instance to retrieve the value from.</param>
    /// <param name="exception">The <see cref="Exception"/> to throw if the <paramref name="maybe"/> has no value.</param>
    /// <returns>The value contained in the <paramref name="maybe"/> if <see cref="Maybe{TValue}.HasValue"/> is true.</returns>
    /// <exception cref="Exception">The specified <paramref name="exception"/> is thrown if the <paramref name="maybe"/> has no value.</exception>
    public static TValue ValueOrThrow<TValue>(this Maybe<TValue> maybe, Exception exception)
    {
        return maybe.HasValue ? maybe.Value : throw exception;
    }
}
