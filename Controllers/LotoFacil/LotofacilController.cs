using Api_Que_ConsomeOutrasApi.Services.LotofacilAPI;
using Microsoft.AspNetCore.Mvc;

namespace Api_Que_ConsomeOutrasApi.Controllers.LotoFacil;

[ApiController]
[Route("api/")]
public class LotofacilController : ControllerBase
{
    private readonly LotoFacilService _lotofacilService;

    public LotofacilController(LotoFacilService lotoFacilService)
    {
        _lotofacilService = lotoFacilService;
    }


    /// <summary>
    /// Gera por padrão 3 jogos da Lotofácil, mas pode ser a quantidade de jogos que o usuário quiser
    /// </summary>
    /// <param name="numeroDeJogos"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /GerarNumerosDaLotofácil/{numeroDeJogos}
    ///     {
    ///        "numeroDeJogos": 3
    ///     }
    ///
    /// </remarks>
    [HttpGet("GerarNumerosDaLotofácil/{numeroDeJogos}")]
    public async Task<ActionResult<IEnumerable<object>>> GerarNumerosLotoFacil(int numeroDeJogos = 3)
    {

        try
        {
            if (numeroDeJogos <= 0)
            {
                return BadRequest("Número de linhas inválido");
            }
            else
            {
                var numeros = await _lotofacilService.GerarNumerosDaLotofacil(numeroDeJogos);
                var jsonResult = new List<object>();

                for (int i = 0; i < numeros.Count; i++)
                {
                    var gameTitle = $"JOGO N° {i + 1}";
                    var gameNumbers = string.Join(" ", numeros[i]);
                    jsonResult.Add(new { título = gameTitle, números = gameNumbers });
                }

                return Ok(jsonResult);
            }
        }
        catch
        {

            throw new ArgumentException("Falha ao gerar jogos da lotofácil.");
        }
    }
}