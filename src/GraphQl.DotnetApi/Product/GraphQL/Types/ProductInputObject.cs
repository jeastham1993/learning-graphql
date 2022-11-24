namespace GraphQl.DotnetApi.Product.GraphQL.Types;

using global::GraphQL.Types;

using GraphQl.DotnetApi.Product.DataTransfer;

public class ProductInputObject : InputObjectGraphType<ProductDetailDTO>
{
    public ProductInputObject()
    {
        this.Name = "AddProductInput";
        this.Description = "A new product";

        this.Field(r => r.Name).Description("Name of the product");
        this.Field(r => r.Price).Description("Price of the product");
    }
}