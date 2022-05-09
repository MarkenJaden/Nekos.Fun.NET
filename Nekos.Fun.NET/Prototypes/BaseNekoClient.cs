﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace Nekos.Fun.NET.Prototypes;

/// <summary>
///     A boilerplate client.
///     Normally you should not use this.
///     Use <see cref="NekosV1Client"/> class instead.
/// </summary>
public class BaseNekoClient
{
    /// <summary>
    ///     Base URL to send request.
    ///     Should be initialized through a constructor.
    /// </summary>
    private protected static string HostUrl;

    /// <summary>
    ///     Logger to log the execution of program.
    ///     An instance of Microsoft.Extensions.Logging.ILogger.
    /// </summary>
    protected ILogger NekoLogger;

    /// <summary>
    ///     Whether logging is allowed or not.
    /// </summary>
    protected bool IsLoggingAllowed;

    /// <summary>
    ///     Construct a client with provided logger and logging option.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="isLoggingAllowed"></param>
    protected BaseNekoClient(ILogger logger, bool isLoggingAllowed = true)
    {
        NekoLogger = logger;
        IsLoggingAllowed = isLoggingAllowed;
    }

    /// <summary>
    ///     Construct a client with no logger and no logging allowed.
    /// </summary>
    protected BaseNekoClient()
    {
        NekoLogger = NullLogger<BaseNekoClient>.Instance;
        IsLoggingAllowed = false;
    }

    /// <summary>
    ///     Construct a client from the other client.
    /// </summary>
    /// <param name="other">Other client.</param>
    protected BaseNekoClient(BaseNekoClient other)
    {
        IsLoggingAllowed = other.IsLoggingAllowed;
        NekoLogger = other.NekoLogger;
    }

    /// <summary>
    ///     Override currently used logging allowance.
    /// </summary>
    /// <param name="isLoggingAllowed">true if logging is allowed, false otherwise.</param>
    /// <returns>Post-reconfigured client. Most of the time you don't need this.</returns>
    public BaseNekoClient OverrideLoggingAllowance(bool isLoggingAllowed)
    {
        IsLoggingAllowed = isLoggingAllowed;
        return this;
    }

    /// <summary>
    ///     Override currently used logger. Implicitly set <see cref="IsLoggingAllowed"/> to true.
    /// </summary>
    /// <param name="logger">New logger. An instance of Microsoft.Extensions.Logging.ILogger.</param>
    /// <returns>Post-reconfigured client. Most of the time you don't need this.</returns>
    public BaseNekoClient OverrideLogger(ILogger logger)
    {
        NekoLogger = logger;
        IsLoggingAllowed = true;
        return this;
    }

    /// <summary>
    ///     Override currently used client.
    /// </summary>
    /// <param name="other">Other client.</param>
    /// <returns>Post-reconfigured client. Most of the time you don't need this.</returns>
    public BaseNekoClient OverrideClient(BaseNekoClient other)
    {
        NekoLogger = other.NekoLogger;
        IsLoggingAllowed = other.IsLoggingAllowed;
        return this;
    }

    /// <summary>
    ///     Send the request asynchronously.
    /// </summary>
    /// <param name="destination">Full destination URL.</param>
    /// <typeparam name="T">Class to deserialize from JSON.</typeparam>
    /// <returns>JSON-deserialized class.</returns>
    protected async Task<T> GetResponse<T>(string destination)
    {
        using var httpClient = new HttpClient();
        var req = new HttpRequestMessage(HttpMethod.Get, destination);
        var res = await httpClient.SendAsync(req);

        if (!res.IsSuccessStatusCode)
            if (IsLoggingAllowed)
                NekoLogger.LogError($"{destination} returned status code: {res.StatusCode}");

        var response = await res.Content.ReadAsStringAsync();

        if (IsLoggingAllowed)
            NekoLogger.LogDebug($"{destination} returned: {response}".Replace('\n', '\0'));

        return JsonConvert.DeserializeObject<T>(response);
    }
}