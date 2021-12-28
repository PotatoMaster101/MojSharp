using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MojSharp.RequestSender;

/// <summary>
/// A basic implementation of <see cref="IRequestSender"/> where content type is set to JSON.
/// </summary>
public class BasicJsonRequestSender : IRequestSender
{
    /// <summary>
    /// The HTTP client used for sending requests.
    /// </summary>
    private static readonly HttpClient Client = new();

    /// <inheritdoc cref="IRequestSender.Get(Uri, CancellationToken)"/>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url"/> is <see langword="null"/>.</exception>
    public async Task<(HttpStatusCode, string)> Get(Uri url, CancellationToken cancellation = default)
    {
        if (url is null)
            throw new ArgumentNullException(nameof(url));

        using var response = await Client.GetAsync(url, cancellation).ConfigureAwait(false);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellation).ConfigureAwait(false));
    }

    /// <inheritdoc cref="IRequestSender.Post(Uri, string, string?, CancellationToken)"/>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url"/> or <paramref name="content"/> is <see langword="null"/>.</exception>
    public async Task<(HttpStatusCode, string)> Post(Uri url, string content, string? bearer = null, CancellationToken cancellation = default)
    {
        if (url is null)
            throw new ArgumentNullException(nameof(url));
        if (content is null)
            throw new ArgumentNullException(nameof(content));

        using var requestMsg = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(content, Encoding.Default, "application/json")
        };
        if (bearer is not null)
            requestMsg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

        using var response = await Client.SendAsync(requestMsg, cancellation).ConfigureAwait(false);
        return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellation).ConfigureAwait(false));
    }
}
