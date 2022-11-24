namespace GraphQl.DotnetApi;

using global::GraphQL;

using GraphQl.DotnetApi.Product.GraphQL;

public static class BuilderExtensions
{
    public static WebApplicationBuilder AddGraphQLServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddGraphQL(
            (options) =>
            {
                options.AddGraphTypes();
                options.AddDataLoader();
                options.AddSystemTextJson();
            });

        builder.Services.AddSingleton<ProductQueryObject>();
        builder.Services.AddSingleton<ProductMutationObject>();
        builder.Services.AddSingleton<ProductSchema>();

        return builder;
    }
}