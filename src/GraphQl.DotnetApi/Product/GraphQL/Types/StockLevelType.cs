namespace GraphQl.DotnetApi.Product.GraphQL.Types;

using global::GraphQL;
using global::GraphQL.Execution;
using global::GraphQL.Types;

using GraphQl.DotnetApi.Product.DataTransfer;

using GrpcProductClient;

public class StockLevelType : ObjectGraphType<StockLevelDTO>
{
    public StockLevelType(ILogger<StockLevelType> logger)
    {
        this.Field(x => x.FreeStock);
        this.Field(x => x.ReservedStock);
    }
}