using System.Net;

namespace MojSharp.RequestSender;

/// <summary>
/// Represents an implementation of a HTTP request sender.
/// </summary>
public interface IRequestSender
{
    /// <summary>
    /// Sends a HTTP GET request.
    /// </summary>
    /// <param name="url">The URL to send the request to.</param>
    /// <param name="cancellation">The cancellation token for cancelling the request.</param>
    /// <returns>The HTTP status code and the response from the endpoint.</returns>
    Task<(HttpStatusCode, string)> Get(Uri url, CancellationToken cancellation = default);

    /// <summary>
    /// Sends a HTTP POST request.
    /// </summary>
    /// <param name="url">The URL to send the request to.</param>
    /// <param name="content">The content for the request.</param>
    /// <param name="bearer">The bearer authentication token to use.</param>
    /// <param name="cancellation">The cancellation token for cancelling the request.</param>
    /// <returns>The HTTP status code and the response from the endpoint.</returns>
    Task<(HttpStatusCode, string)> Post(Uri url, string content, string? bearer = null, CancellationToken cancellation = default);
}
