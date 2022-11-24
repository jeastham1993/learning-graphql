namespace GraphQl.DotnetApi.Product.Domain;

public interface IProductRepository
{
    Task<Product?> GetProduct(string id);
    
    Task<IEnumerable<Product>> GetProducts();

    Task AddProduct(Product product);
}