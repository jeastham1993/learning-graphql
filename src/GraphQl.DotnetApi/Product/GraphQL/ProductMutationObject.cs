namespace GraphQl.DotnetApi.Product.GraphQL;

using global::GraphQL;
using global::GraphQL.Types;

using GraphQl.DotnetApi.Product.DataTransfer;
using GraphQl.DotnetApi.Product.GraphQL.Types;
using GraphQl.DotnetApi.Shared;

using GrpcProductClient;

public class ProductMutationObject : BaseGraphType<object>
{
    private readonly ProductManager.ProductManagerClient _client;
    private readonly ILogger<ProductMutationObject> _logger;
    
    public ProductMutationObject(ProductManager.ProductManagerClient client, ILogger<ProductMutationObject> logger) : base(logger)
    {
        this._client = client;
        this._logger = logger;
        
        Name = "ProductMutations";
        Description = "The base mutation for all the entities in our object graph.";

        this.MapAddProductCommand();
    }

    private void MapAddProductCommand()
    {
        Field<ProductType, ProductDTO>("addProduct")
            .Description("Add a new product")
            .Argument<NonNullGraphType<ProductInputObject>>("addProductInput")
            .ResolveAsync(
                async context => await this.Handle(
                    context,
                    async _ =>
                    {
                        this._logger.LogInformation("Adding new product");
                        
                        var inputObject = context.GetArgument<ProductDetailDTO>("addProductInput");

                        var result = await this._client.AddProductAsync(
                            new AddProductRequest()
                            {
                                Name = inputObject.Name,
                                Price = inputObject.Price
                            });
                        
                        this._logger.LogInformation("Added product");

                        return new ProductDTO(result.Id)
                        {
                            Details = new ProductDetailDTO()
                            {
                                Name = result.Name,
                                Price = result.Price
                            }
                        };
                    }));
    }
}