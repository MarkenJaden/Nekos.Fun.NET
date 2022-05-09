using System.ComponentModel;

namespace Nekos.Fun.NET.V1.Endpoints;

/// <summary>
///     List of NSFW endpoints
/// </summary>
[Flags]
public enum NsfwEndpoint : ulong
{
    /// <summary>
    ///     Everything except for Random.
    /// </summary>
    /// <seealso cref="Random"/>
    All = 1 << 0,

    /// <summary>
    ///     A random endpoint.
    ///     Use this if you don't know what to pick.
    /// </summary>
    /// <remarks>May cause infinite loop issue if your luck is terrible.</remarks>
    Random = 1 << 1,

    Ass = 1 << 2,
    
    Blowjob = 1 << 3,

    Boobs = 1 << 4,
    
    Cum = 1 << 5,
    
    Feet = 1 << 6,
    
    hentai = 1 << 7,
    
    wallpapers = 1 << 8,
    
    spank = 1 << 9,
    
    gasm = 1 << 10,
    
    lesbian = 1 << 11,
    
    lewd = 1 << 12,
    
    pussy = 1 << 13,

    //4K = 1 << 14,
}