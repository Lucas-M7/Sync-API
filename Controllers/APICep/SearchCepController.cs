using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api_Que_ConsomeOutrasApi.Controllers.APICep;

[ApiController]
[Route("api/")]
public class SearchCepController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public SearchCepController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://viacep.com.br/");
    }

    /// <summary>
    /// Retorna as informações Estado, Cidade e Bairro passado, no parâmetro bairro 
    /// você pode escrever (Avenida) que vai retornar todos endereços que tem essa informação contida
    /// </summary>
    /// <param name="UF"></param>
    /// <param name="Cidade"></param>
    /// <param name="Bairro"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /YourCep/{UF}/{Cidade}/{Bairro}
    ///     {
    ///     "UF": "PE",
    ///     "Cidade": "Recife",
    ///     "Bairro": "Boa Viagem"
    ///     }
    ///
    /// </remarks>
    [HttpGet("YourCep/{UF}/{Cidade}/{Bairro}")]
    public async Task<IActionResult> GetYoutCep(string UF, string Cidade, string Bairro)
    {
        try
        {
            string apiUrl = $"https://viacep.com.br/ws/{UF}/{Cidade}/{Bairro}/json/";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var myCep = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(myCep, Formatting.Indented);

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
