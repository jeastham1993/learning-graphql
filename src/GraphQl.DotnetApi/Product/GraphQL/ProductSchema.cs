namespace GraphQl.DotnetApi.Product.GraphQL;

using global::GraphQL.Types;

public class ProductSchema : Schema
{
    public ProductSchema(
        ProductQueryObject query,
        ProductMutationObject mutation,
        IServiceProvider provider) : base(provider)
    {
        Query = query;
        Mutation = mutation;
    }
}