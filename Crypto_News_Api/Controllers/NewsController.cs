using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Crypto_News_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public NewsController(HttpClient client, IConfiguration configuration)
    {

        _client = client;
        this._configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> GetNews()
    {
        var apikey = _configuration["api_key"];
        var apihost = _configuration["api_host"];
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://investing-cryptocurrency-markets.p.rapidapi.com/coins/get-news?pair_ID=1057391&page=1&time_utc_offset=28800&lang_ID=1"),
            Headers =
    {
        { "X-RapidAPI-Key", $"{apikey}" },
        { "X-RapidAPI-Host", $"{apihost}" },
    },
        };
        using (var response = await _client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return Ok(body);
        }
    }
}
