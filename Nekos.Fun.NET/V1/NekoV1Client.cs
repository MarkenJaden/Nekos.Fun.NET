using Microsoft.Extensions.Logging;
using Nekos.Fun.NET.Prototypes;
using Nekos.Fun.NET.V1.Endpoints;
using Nekos.Fun.NET.V1.Responses;

namespace Nekos.Fun.NET.V1;

/// <summary>
///     A client to interact with nekos-love.xyz API v1
/// </summary>
public class NekoV1Client : BaseNekoClient
{
    static NekoV1Client()
    {
        HostUrl = "http://api.nekos.fun:8080/api";
    }

    /// <summary>
    ///     <inheritdoc/>
    /// </summary>
    public NekoV1Client()
    {
    }

    /// <summary>
    ///     <inheritdoc/>
    /// </summary>
    public NekoV1Client(NekoV1Client other) : base(other)
    {
    }

    /// <summary>
    ///     <inheritdoc/>
    /// </summary>
    public NekoV1Client(ILogger logger, bool isLoggingAllowed = true) : base(logger, isLoggingAllowed)
    {
    }

    // segments
    private const string MediaRequestUrlSegment = "";

    /// <summary>
    ///     Get SFW results.
    /// </summary>
    /// <param name="endpoints">Members of <see cref="SfwEndpoint" /> enum representing the endpoints.</param>
    /// <param name="count">How many times EACH INDIVIDUAL REQUEST should be made.</param>
    /// <returns>Requested SFW results.</returns>
    /// <exception cref="ArgumentException">When the count is zero.</exception>
    public async Task<IEnumerable<NekoImage>> RequestSfwResultsAsync(SfwEndpoint endpoints, uint count = 1)
    {
        if (count == 0)
            throw new ArgumentException("\"count\" must not be zero", nameof(count));

        List<NekoImage> responses = new();
        IEnumerable<SfwEndpoint> availableFlags = Enum.GetValues<SfwEndpoint>();

        foreach (var endpoint in availableFlags)
        {
            var isSet = (endpoint & endpoints) != 0;
            if (!isSet) continue;

            if (endpoint == SfwEndpoint.All)
            {
                var images = await RequestAllSfwAsync(count).ConfigureAwait(false);
                responses.AddRange(images);
                continue;
            }

            SfwEndpoint tempEndpoint = endpoint;

            while (tempEndpoint == SfwEndpoint.Random)
            {
                var r = new Random();
                // random is 0
                // so simply ignore it
                var indexPick = r.Next(1, Enum.GetNames(typeof(SfwEndpoint)).Length - 1);
                tempEndpoint = availableFlags.ToArray()[indexPick];

                if (IsLoggingAllowed)
                    NekoLogger.LogWarning($"Replaced \"Random\" with \"{tempEndpoint.ToString().ToLower()}\"");
            }

            var dest = tempEndpoint.ToString().ToLower();

            for (var i = 0; i < count; ++i)
            {
                var response = await GetResponse<NekoImage>($"{HostUrl}{MediaRequestUrlSegment}/{dest}").ConfigureAwait(false);
                responses.Add(response);
            }
        }

        return responses;
    }

    /// <summary>
    ///     Get NSFW results.
    /// </summary>
    /// <param name="endpoints">Members of <see cref="NsfwEndpoint" /> enum representing the endpoints.</param>
    /// <param name="count">How many times EACH INDIVIDUAL REQUEST should be made.</param>
    /// <returns>Requested NSFW results.</returns>
    /// <exception cref="ArgumentException">When the count is zero.</exception>
    public async Task<IEnumerable<NekoImage>> RequestNsfwResultsAsync(NsfwEndpoint endpoints, uint count = 1)
    {
        if (count == 0)
            throw new ArgumentException("\"count\" must not be zero", nameof(count));

        List<NekoImage> responses = new();
        IEnumerable<NsfwEndpoint> availableFlags = Enum.GetValues<NsfwEndpoint>();

        foreach (var endpoint in availableFlags)
        {
            var isSet = (endpoint & endpoints) != 0;
            if (!isSet) continue;

            if (endpoint == NsfwEndpoint.All)
            {
                var images = await RequestAllNsfwAsync(count).ConfigureAwait(false);
                responses.AddRange(images);
                continue;
            }

            NsfwEndpoint tempEndpoint = endpoint;
            while (tempEndpoint == NsfwEndpoint.Random && Enum.GetNames(typeof(NsfwEndpoint)).Length > 3)
            {
                var r = new Random();
                // random is 0
                // so simply ignore it
                var indexPick = r.Next(1, Enum.GetNames(typeof(NsfwEndpoint)).Length - 1);
                tempEndpoint = availableFlags.ToArray()[indexPick];

                if (IsLoggingAllowed)
                    NekoLogger.LogWarning($"Replaced \"Random\" with \"{tempEndpoint.ToString().ToLower()}\"");
            }

            var dest = tempEndpoint.ToString().ToLower();

            for (var i = 0; i < count; ++i)
            {
                var response = await GetResponse<NekoImage>($"{HostUrl}{MediaRequestUrlSegment}/{dest}").ConfigureAwait(false);
                responses.Add(response);
            }
        }

        return responses;
    }

    /// <summary>
    ///     Request to all NSFW endpoints.
    /// </summary>
    /// <param name="count">How many times EACH INDIVIDUAL REQUEST should be made.</param>
    /// <returns>A list of responses to all NSFW endpoints.</returns>
    /// <exception cref="ArgumentException">When the count is zero.</exception>
    public async Task<IEnumerable<NekoImage>> RequestAllNsfwAsync(uint count = 1)
    {
        if (count == 0)
            throw new ArgumentException("\"count\" must not be zero", nameof(count));

        if (IsLoggingAllowed)
            NekoLogger.LogWarning("Requesting to all NSFW endpoints");

        List<NekoImage> responses = new();
        IEnumerable<NsfwEndpoint> availableFlags = Enum.GetValues<NsfwEndpoint>();

        foreach (var endpoint in availableFlags)
        {
            if (endpoint is NsfwEndpoint.All or NsfwEndpoint.Random) continue;

            var dest = endpoint.ToString().ToLower();

            for (var i = 0; i < count; ++i)
            {
                var response = await GetResponse<NekoImage>($"{HostUrl}{MediaRequestUrlSegment}/{dest}").ConfigureAwait(false);
                responses.Add(response);
            }
        }

        return responses;
    }

    /// <summary>
    ///     Request to all SFW endpoints.
    /// </summary>
    /// <param name="count">How many times EACH INDIVIDUAL REQUEST should be made.</param>
    /// <returns>A list of responses to all endpoints.</returns>
    /// <exception cref="ArgumentException">When the count is zero.</exception>
    public async Task<IEnumerable<NekoImage>> RequestAllSfwAsync(uint count = 1)
    {
        if (count == 0)
            throw new ArgumentException("\"count\" must not be zero", nameof(count));

        if (IsLoggingAllowed)
            NekoLogger.LogWarning("Requesting to all SFW endpoints");

        List<NekoImage> responses = new();
        IEnumerable<SfwEndpoint> availableFlags = Enum.GetValues<SfwEndpoint>();

        foreach (var endpoint in availableFlags)
        {
            if (endpoint is SfwEndpoint.All or SfwEndpoint.Random) continue;

            var dest = endpoint.ToString().ToLower();

            for (var i = 0; i < count; ++i)
            {
                var response = await GetResponse<NekoImage>($"{HostUrl}{MediaRequestUrlSegment}/{dest}").ConfigureAwait(false);
                responses.Add(response);
            }
        }

        return responses;
    }
}