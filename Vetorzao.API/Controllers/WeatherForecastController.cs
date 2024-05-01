using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pgvector.EntityFrameworkCore;
using Vetorzao.Application.Infra;
using Vetorzao.UI.Infra.Context;
using Vetorzao.UI.Models;

namespace Vetorzao.API.Controllers;

public class CreateProduct
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public ProductCategory Category { get; set; }

    public Product ToProduct(float[] embeddings)
    => new()
    {
        Category = Category,
        Name = Name,
        Description = Description,
        Price = Price,
        Embeddings = new(embeddings),
    };
}

public class SearchProduct
{
    public string SearchTerm { get; set; } = default!;
}

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(
    ApplicationDbContext _context,
    EmbeddingProvider embedder
    ) : ControllerBase
{
    [HttpPost(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Create(CreateProduct request)
    {
        var embeddings = await embedder.GenerateEmbeddings($"{request.Name} - {request.Description}");

        var saveProduct = _context.Products.Add(request.ToProduct(embeddings));

        await _context.SaveChangesAsync();

        var e = saveProduct.Entity;

        return Ok(e);
    }

    [HttpPost("Search")]
    public async Task<IActionResult> Get([FromBody] SearchProduct request)
    {
        var embeddings = await embedder.GenerateEmbeddings(request.SearchTerm);

        var items = await _context.Products
                            .AsNoTracking()
                            .Select(x => new
                            {
                                Entity = x,
                                Distance = x.Embeddings!.L2Distance(new(embeddings))
                            })
                            .OrderBy(x => x.Distance)
                            .Take(3)
                            .ToListAsync()
                            ;


        return Ok(items.Select(x => new
        {
            x.Distance,
            Entity = new
            {
                x.Entity.Id,
                x.Entity.Name,
                x.Entity.Description,
                x.Entity.Price,
            }
        }));
    }
}
