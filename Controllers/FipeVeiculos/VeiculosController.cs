using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api_Que_ConsomeOutrasApi.Controllers.FipeVeiculos;

[ApiController]
[Route("api/")]
public class VeiculosController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public VeiculosController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://parallelum.com.br/fipe/api/v1");
    }


    /// <summary>
    /// Retorna as marcas disponíveis para consulta
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [HttpGet("carros/marcas")]
    public async Task<IActionResult> GetCars()
    {
        try
        {
            string apiUrl = "https://parallelum.com.br/fipe/api/v1/carros/marcas";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var veiculos = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(veiculos, Formatting.Indented);
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
    /// Retorna os modelos da marca desejada de acordo com o código (id) passado
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /carros/marcas/{id}/modelos
    ///     {
    ///        "id": 58
    ///     }
    ///
    /// </remarks>
    [HttpGet("carros/marcas/{id}/modelos")]
    public async Task<IActionResult> GetCarsModel(int id)
    {
        try
        {
            string apiUrl = $"https://parallelum.com.br/fipe/api/v1/carros/marcas/{id}/modelos";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var veiculos = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(veiculos, Formatting.Indented);
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