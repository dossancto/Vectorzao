using OllamaSharp;
using OllamaSharp.Models;

namespace Vetorzao.Application.Infra;

public class EmbeddingProvider
{
    private readonly OllamaApiClient _ollama;

    public EmbeddingProvider()
    {
        var uri = new Uri("http://localhost:11434");
        var ollama = new OllamaApiClient(uri);

        _ollama = ollama;
    }

    public async Task<float[]> GenerateEmbeddings(string text)
    {
        var request = new GenerateEmbeddingRequest()
        {
            Model = "snowflake-arctic-embed:latest",
            Prompt = text
        };

        var response = await _ollama.GenerateEmbeddings(request);

        return response.Embedding.Select(x => (float)x).ToArray();

    }
}
