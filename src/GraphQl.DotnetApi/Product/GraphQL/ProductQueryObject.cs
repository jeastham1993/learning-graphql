namespace GraphQl.DotnetApi.Product.GraphQL;

using global::GraphQL;
using global::GraphQL.Types;

using GraphQl.DotnetApi.Product.DataTransfer;
using GraphQl.DotnetApi.Product.GraphQL.Types;
using GraphQl.DotnetApi.Shared;

using GrpcProductClient;

public class ProductQueryObject : BaseGraphType<object>
{
    private readonly ProductManager.ProductManagerClient _client;
    private readonly ILogger<ProductQueryObject> _logger;
    
    public ProductQueryObject(ProductManager.ProductManagerClient client, ILogger<ProductQueryObject> logger) : base(logger)
    {
        this._client = client;
        this._logger = logger;
        
        Name = "ProductQueries";
        Description = "The base mutation for all the entities in our object graph.";

        this.MapGetProductEndpoint();
        this.MapListProductsEndpoint();
        this.MapListReviewsEndpoint();
    }

    private void MapListProductsEndpoint()
    {
        this.Field<ProductListType, ProductListDTO>("listProducts")
            .Description("Lists all products")
            .ResolveAsync(
                async context => await this.Handle(
                    context,
                    async _ =>
                    {
                        var result = this._client.GetProducts(new GetProductsRequest());

                        var products = new List<ProductDTO>();

                        while (await result.ResponseStream.MoveNext(default))
                        {
                            var currentProduct = result.ResponseStream.Current;
                            products.Add(new ProductDTO(currentProduct.Id));
                        }

                        return new ProductListDTO(products);
                    }));
    }

    private void MapGetProductEndpoint()
    {
        this.Field<ProductType, ProductDTO>("getProduct")
            .Description("Gets a product by it's unique identifier")
            .Argument<StringGraphType>("id")
            .ResolveAsync(
                async context => await this.Handle(
                    context,
                    async _ =>
                    {
                        this._logger.LogInformation("Starting product query");

                        await Task.Delay(TimeSpan.FromSeconds(1));
                        
                        var result = await this._client.GetProductAsync(
                            new GetProductRequest()
                            {
                                Id = context.GetArgument(
                                    "id",
                                    string.Empty)
                            });

                        this._logger.LogInformation("Query complete");

                        await Task.Delay(TimeSpan.FromSeconds(1));

                        return new ProductDTO(result.Id);
                    }));
    }

    private void MapListReviewsEndpoint()
    {
        this.Field<ReviewListType, ReviewListDTO>("getReviewsForProduct")
            .Description("List reviews by the product")
            .Argument<StringGraphType>("productId")
            .ResolveAsync(
                async context => await this.Handle(
                    context,
                    async _ =>
                    {
                        return new ReviewListDTO();
                    }));
    }
}