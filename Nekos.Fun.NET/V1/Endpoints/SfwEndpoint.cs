namespace Nekos.Fun.NET.V1.Endpoints;

/// <summary>
///     List of SFW endpoints
/// </summary>
[Flags]
public enum SfwEndpoint : ulong
{
    /// <summary>
    ///     Everything except for Random.
    /// </summary>
    /// <seealso cref="Random"/>
    All = 1 << 0,

    /// <summary>
    ///     A random thing.
    ///     Use this if you don't know what to pick.
    /// </summary>
    /// <remarks>May cause infinite loop issue if your luck is terrible.</remarks>
    Random = 1 << 1,

    Kiss = 1 << 2,

    Lick = 1 << 3,
    
    Hug = 1 << 4,

    Baka = 1 << 5,

    cry = 1 << 6,

    Poke = 1 << 7,

    Smug = 1 << 8,
    
    Slap = 1 << 9,

    Tickle = 1 << 10,

    Pat = 1 << 11,
    
    Laugh = 1 << 12,
    
    Feed = 1 << 13,
    
    Cuddle = 1 << 14,
}