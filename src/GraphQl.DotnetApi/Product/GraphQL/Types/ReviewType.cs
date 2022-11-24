namespace GraphQl.DotnetApi.Product.GraphQL.Types;

using global::GraphQL;
using global::GraphQL.Execution;
using global::GraphQL.Types;

using GraphQl.DotnetApi.Product.DataTransfer;

using GrpcProductClient;

public class ReviewType : ObjectGraphType<ReviewDTO>
{
    public ReviewType(ProductManager.ProductManagerClient client, ILogger<ReviewType> logger)
    {
        this.Field(x => x.Stars);
        this.Field(x => x.Contents);
    }
}