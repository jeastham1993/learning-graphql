namespace GraphQl.DotnetApi.Product.GraphQL.Types;

using global::GraphQL.Execution;
using global::GraphQL.Types;

using GraphQl.DotnetApi.Product.DataTransfer;

public class ProductDetailType : ObjectGraphType<ProductDetailDTO>
{
    public ProductDetailType(ILogger<ProductDetailType> logger)
    {
        this.Field(x => x.Name);
        this.Field(x => x.Price);
    }
}