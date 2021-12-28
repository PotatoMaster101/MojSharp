using MojSharp.RequestSender;
using MojSharp.Response;

namespace MojSharp.Request;

/// <summary>
/// A base implementation for <see cref="IRequest{T}"/>.
/// </summary>
/// <typeparam name="T">The response type.</typeparam>
public abstract class BaseRequest<T> : IRequest<T> where T : IResponse
{
    /// <inheritdoc cref="IRequest{T}.Address"/>
    public Uri Address { get; }

    /// <summary>
    /// Gets the request sender.
    /// </summary>
    /// <value>The request sender.</value>
    protected IRequestSender RequestSender { get; }

    /// <summary>
    /// Gets or sets the POST request content.
    /// </summary>
    /// <value>The POST request content.</value>
    protected string? PostData { get; init; }

    /// <summary>
    /// Constructs a new instance of <see cref="BaseRequest{T}"/>.
    /// </summary>
    /// <param name="address">The API endpoint address.</param>
    /// <param name="sender">The HTTP request sender.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="sender"/> or <paramref name="address"/> is <see langword="null"/>.</exception>
    protected BaseRequest(IRequestSender sender, Uri address)
    {
        RequestSender = sender ?? throw new ArgumentNullException(nameof(sender));
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }

    /// <inheritdoc cref="IRequest{T}.Request(CancellationToken)"/>
    public abstract Task<T> Request(CancellationToken cancellation = default);
}
