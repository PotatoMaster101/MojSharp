using MojSharp.Request;
using MojSharp.RequestSender;

namespace MojSharp.BlockedServer;

/// <summary>
/// A request to the blocked servers endpoint.
/// </summary>
public class BlockedServerRequest : BaseRequest<BlockedServerResponse>
{
    /// <summary>
    /// Constructs a new instance of <see cref="BlockedServerRequest"/>.
    /// </summary>
    public BlockedServerRequest()
        : this(new BasicJsonRequestSender()) { }

    /// <summary>
    /// Constructs a new instance of <see cref="BlockedServerRequest"/>.
    /// </summary>
    /// <param name="sender">The HTTP request sender to use.</param>
    public BlockedServerRequest(IRequestSender sender)
        : base(sender, new Uri("https://sessionserver.mojang.com/blockedservers")) { }

    /// <inheritdoc cref="BaseRequest{T}.Request(CancellationToken)"/>
    public override async Task<BlockedServerResponse> Request(CancellationToken cancellation = default)
    {
        var (_, response) = await RequestSender.Get(Address, cancellation).ConfigureAwait(false);
        return new BlockedServerResponse(response, response.Split('\n'));
    }
}
