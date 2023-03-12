using MojSharp.Response;

namespace MojSharp.Request;

/// <summary>
/// Represents a request to the Mojang API.
/// </summary>
/// <typeparam name="T">The response type.</typeparam>
public interface IRequest<T> where T : IResponse
{
    /// <summary>
    /// Gets the endpoint address.
    /// </summary>
    Uri Address { get; }

    /// <summary>
    /// Sends a request to the endpoint and retrieve the response asynchronously.
    /// </summary>
    /// <param name="cancellation">The cancellation token for cancelling the request.</param>
    /// <returns>The response from the endpoint.</returns>
    Task<T> Request(CancellationToken cancellation = default);
}
