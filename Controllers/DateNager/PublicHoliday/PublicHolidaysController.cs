using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DateNager.HolidayPublic.Controllers;

[ApiController]
[Route("api/")]
public class PublicHolidaysController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public PublicHolidaysController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://date.nager.at");
    }


    /// <summary>
    /// Retorna os próximos feriados nacionais do país desejado
    /// </summary>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /NextPublicHolidays/{countryCode}
    ///     {
    ///        "countryCode": "BR"
    ///     }
    ///
    /// </remarks>
    [HttpGet("NextPublicHolidays/{countryCode}")]
    public async Task<IActionResult> GetDate(string countryCode)
    {
        try
        {
            string apiUrl = $"/api/v3/NextPublicHolidays/{countryCode}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var holidays = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(holidays, Formatting.Indented);
                return Ok(formattedJson);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
        catch
        {
            throw new ArgumentException("Falha ao fazer o consumo da API");
        }
    }


    /// <summary>
    /// Retorna todos os feriados nacionais do ano e país desejado
    /// </summary>
    /// <param name="year"></param>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /AllPublicHolidays/{year}/{countryCode}
    ///     {
    ///        "year": 2024,
    ///        "countryCode": "BR"
    ///     }
    ///
    /// </remarks>
    [HttpGet("AllPublicHolidays/{year}/{countryCode}")]
    public async Task<IActionResult> GetDateYear(string year, string countryCode)
    {
        try
        {
            string apiUrl = $"/api/v3/PublicHolidays/{year}/{countryCode}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var holidays = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(holidays, Formatting.Indented);
                return Ok(formattedJson);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
        catch
        {
            throw new ArgumentException("Falha ao fazer o consumo da API");
        }
    }
}
