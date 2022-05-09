using Newtonsoft.Json;

namespace Nekos.Fun.NET.V1.Responses;

/// <summary>
///     Response object for image requests.
/// </summary>
public class NekoImage
{
    /// <summary>
    ///     Image/GIF URL.
    /// </summary>
    [JsonProperty("image")] public string Url { get; set; }
}