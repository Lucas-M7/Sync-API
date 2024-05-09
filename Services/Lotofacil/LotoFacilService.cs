namespace Api_Que_ConsomeOutrasApi.Services.LotofacilAPI;
public class LotoFacilService
{
    private readonly Random _random;

    public LotoFacilService()
    {
        _random = new Random();
    }

    public async Task<List<int[]>> GerarNumerosDaLotofacil(int numerosDeLinhas)
    {
        var lotofacilNumeros = new List<int[]>();

        for (int i = 0; i < numerosDeLinhas; i++)
        {
            var numeros = await GerarLinhaAsync();
            lotofacilNumeros.Add(numeros);
        }

        return lotofacilNumeros;
    }

    private Task<int[]> GerarLinhaAsync()
    {
        return Task.FromResult(GerarLinha());
    }

    private int[] GerarLinha()
    {
        var linha = new List<int>();
        while (linha.Count < 15)
        {
            var numero = _random.Next(1, 26);
            if (!linha.Contains(numero))
            {
                linha.Add(numero);
            }
        }

        linha.Sort();
        return [.. linha];
    }
}