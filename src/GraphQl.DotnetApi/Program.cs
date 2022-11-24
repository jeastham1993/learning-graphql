using System.Reflection;

using GraphQl.DotnetApi;
using GraphQl.DotnetApi.Product.GraphQL;

using Grpc.Net.Client;

using GrpcProductClient;

using GrpcReviewClient;

using GrpcStockClient;

using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();

builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

var productChannel = GrpcChannel.ForAddress(builder.Configuration["ProductEndpoint"]);
var stockLevelChannel = GrpcChannel.ForAddress(builder.Configuration["StockEndpoint"]);
var reviewChannel = GrpcChannel.ForAddress(builder.Configuration["ReviewEndpoint"]);

builder.Services.AddSingleton(new ProductManager.ProductManagerClient(productChannel));
builder.Services.AddSingleton(new StockManager.StockManagerClient(stockLevelChannel));
builder.Services.AddSingleton(new ReviewManager.ReviewManagerClient(reviewChannel));

builder.AddGraphQLServices();

var app = builder.Build();

app.UseGraphQL<ProductSchema>();

app.UseRouting();

app.UseEndpoints(
    endpoints =>
    {
        endpoints.MapGraphQL();
        endpoints.MapGraphQLPlayground();
    });

app.Run();
