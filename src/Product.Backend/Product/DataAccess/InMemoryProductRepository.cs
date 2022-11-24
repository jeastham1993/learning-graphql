namespace GraphQl.DotnetApi.Product.DataAccess;

using GraphQl.DotnetApi.Product.Domain;

public class InMemoryProductRepository : IProductRepository
{
    private List<Product> _products;

    public InMemoryProductRepository()
    {
        this._products = new List<Product>();
    }
    
    /// <inheritdoc />
    public async Task<Product?> GetProduct(string id)
    {
        return this._products.FirstOrDefault(
            p => p.Id.Equals(
                id,
                StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> GetProducts() => this._products;

    /// <inheritdoc />
    public async Task AddProduct(Product product) => this._products.Add(product);
}