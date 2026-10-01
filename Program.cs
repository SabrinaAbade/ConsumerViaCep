using System.Text.Json;

Console.Write("Digite o CEP: ");
string? cep = Console.ReadLine();

if (string.IsNullOrWhiteSpace(cep))
{
    Console.WriteLine("CEP inválido.");
    return;
}

using HttpClient client = new HttpClient();

string url = $"https://viacep.com.br/ws/{cep}/json/";

try
{
    HttpResponseMessage response = await client.GetAsync(url);

    if (response.IsSuccessStatusCode)
    {
        string json = await response.Content.ReadAsStringAsync();

        Endereco? endereco = JsonSerializer.Deserialize<Endereco>(json);

        if (endereco != null)
        {
            Console.WriteLine("\nEndereço encontrado:");
            Console.WriteLine($"CEP: {endereco.Cep}");
            Console.WriteLine($"Logradouro: {endereco.Logradouro}");
            Console.WriteLine($"Bairro: {endereco.Bairro}");
            Console.WriteLine($"Cidade: {endereco.Localidade}");
            Console.WriteLine($"Estado: {endereco.Uf}");
        }
    }
    else
    {
        Console.WriteLine($"Erro na consulta. Status: {response.StatusCode}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}

public class Endereco
{
    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Bairro { get; set; }
    public string? Localidade { get; set; }
    public string? Uf { get; set; }
}
