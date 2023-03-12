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
    protected IRequestSender RequestSender { get; }

    /// <summary>
    /// Gets or sets the POST request content.
    /// </summary>
    protected string? PostData { get; init; }

    /// <summary>
    /// Constructs a new instance of <see cref="BaseRequest{T}"/>.
    /// </summary>
    /// <param name="address">The API endpoint address.</param>
    /// <param name="sender">The HTTP request sender.</param>
    protected BaseRequest(IRequestSender sender, Uri address)
    {
        RequestSender = sender;
        Address = address;
    }

    /// <inheritdoc cref="IRequest{T}.Request(CancellationToken)"/>
    public abstract Task<T> Request(CancellationToken cancellation = default);
}
