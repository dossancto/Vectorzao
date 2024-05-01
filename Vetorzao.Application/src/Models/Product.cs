using Pgvector;

namespace Vetorzao.UI.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public ProductCategory Category { get; set; }

    public Vector? Embeddings { get; set; }

    public double Distance { get; set; }
}

public enum ProductCategory
{
    ITALIAN_FOOD,
    JAPONESE_FOOD,
    TECNOLOGY,
    PROGRAMMING,
}
