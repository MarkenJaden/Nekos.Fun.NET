using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nekos.Fun.NET.V1;
using Nekos.Fun.NET.V1.Endpoints;
using Nekos.Fun.NET.V1.Responses;

namespace NekoV1ClientTest;

[TestClass]
public class NekoV1ClientTest
{
    private readonly NekoV1Client _client = new();

    [TestMethod]
    public async Task RequestAllAsMethodCallTest()
    {
        List<NekoImage> results = new();
        results.AddRange(await _client.RequestAllNsfwAsync());
        results.AddRange(await _client.RequestAllSfwAsync());

        // -2: exclude Random and All
        var nsfwValidFlagCount = Enum.GetValues<NsfwEndpoint>().Length - 2;
        var sfwValidFlagCount = Enum.GetValues<SfwEndpoint>().Length - 2;

        Assert.IsTrue(results.Count == nsfwValidFlagCount + sfwValidFlagCount);
    }

    [TestMethod]
    public async Task RequestAllAsFlagTest()
    {
        List<NekoImage> results = new();
        results.AddRange(await _client.RequestSfwResultsAsync(SfwEndpoint.All));
        results.AddRange(await _client.RequestNsfwResultsAsync(NsfwEndpoint.All));

        // -2: exclude Random and All
        var nsfwValidFlagCount = Enum.GetValues<NsfwEndpoint>().Length - 2;
        var sfwValidFlagCount = Enum.GetValues<SfwEndpoint>().Length - 2;

        Assert.IsTrue(results.Count == nsfwValidFlagCount + sfwValidFlagCount);
    }

    [TestMethod]
    public async Task RequestSingleEndpointTest()
    {
        List<NekoImage> results = new();
        results.AddRange(await _client.RequestSfwResultsAsync(SfwEndpoint.Hug));
        results.AddRange(await _client.RequestNsfwResultsAsync(NsfwEndpoint.Boobs));
        Assert.IsTrue(results.Count == 2);
    }

    [TestMethod]
    public async Task RequestRandomEndpointTest()
    {
        List<NekoImage> results = new();
        results.AddRange(await _client.RequestSfwResultsAsync(SfwEndpoint.Random));
        results.AddRange(await _client.RequestNsfwResultsAsync(NsfwEndpoint.Random));
        Assert.IsTrue(results.Count == 2);
    }

    [TestMethod]
    public async Task RequestSfwMixedFlagsTest()
    {
        List<NekoImage> results = new();
        results.AddRange(await _client.RequestSfwResultsAsync(SfwEndpoint.Random | SfwEndpoint.All | SfwEndpoint.Kiss));
        var sfwValidFlagCount = Enum.GetValues<SfwEndpoint>().Length - 2;
        Assert.IsTrue(results.Count == sfwValidFlagCount + 1 + 1);
    }

    [TestMethod]
    public async Task RequestNsfwMixedFlagsTest()
    {
        List<NekoImage> results = new();
        results.AddRange(await _client.RequestNsfwResultsAsync(NsfwEndpoint.Random | NsfwEndpoint.All | NsfwEndpoint.spank));
        var nsfwValidFlagCount = Enum.GetValues<NsfwEndpoint>().Length - 2;
        Assert.IsTrue(results.Count == nsfwValidFlagCount + 1 + 1);
    }
}