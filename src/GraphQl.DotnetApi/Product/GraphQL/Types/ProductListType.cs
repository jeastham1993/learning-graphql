namespace GraphQl.DotnetApi.Product.GraphQL.Types;

using global::GraphQL.Types;

using GraphQl.DotnetApi.Product.DataTransfer;

public class ProductListType : ObjectGraphType<ProductListDTO>
{
    public ProductListType()
    {
        this.Field<ListGraphType<ProductType>>("products");
    }
}