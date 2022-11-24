using Grpc.Core;
using Product.Backend;

namespace Product.Backend.Services;

using GraphQl.DotnetApi.Product.Domain;

public class ProductManagerService : ProductManager.ProductManagerBase
{
    private readonly ILogger<ProductManagerService> _logger;
    private readonly IProductRepository _productRepository;
    
    public ProductManagerService(ILogger<ProductManagerService> logger, IProductRepository productRepository)
    {
        _logger = logger;
        this._productRepository = productRepository;
    }

    /// <inheritdoc />
    public override async Task<ProductReply> GetProduct(GetProductRequest request, ServerCallContext context)
    {
        var product = await this._productRepository.GetProduct(request.Id);
        
        if (product == null)
        {
            throw new ArgumentException("Product not found");
        }

        return new ProductReply()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }

    /// <inheritdoc />
    public override async Task GetProducts(
        GetProductsRequest request,
        IServerStreamWriter<ProductReply> responseStream,
        ServerCallContext context)
    {
        var products = await this._productRepository.GetProducts();

        foreach (var product in products)
        {
            await responseStream.WriteAsync(new ProductReply()
            {
                Name = product.Name,
                Id = product.Id,
                Price = product.Price
            });
        }
    }

    /// <inheritdoc />
    public override async Task<ProductReply> AddProduct(AddProductRequest request, ServerCallContext context)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Price = request.Price
        };

        await this._productRepository.AddProduct(product);

        return new ProductReply()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
}
