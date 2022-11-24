namespace GraphQl.DotnetApi.Shared;

using GraphQL;
using GraphQL.Execution;
using GraphQL.Types;

public class BaseGraphType<TGraphType> : ObjectGraphType<TGraphType>
{
    private readonly ILogger<TGraphType> logger;

    public BaseGraphType(ILogger<TGraphType> logger)
    {
        this.logger = logger;
    }
    
    public async Task<TResponseType?> Handle<TResponseType>(IResolveFieldContext<object> context, Func<IResolveFieldContext<object>, Task<TResponseType>> request) where TResponseType : class
    {
        try
        {
            return await request.Invoke(context);
        }
        catch (Exception ex)
        {
            this.logger.LogError(
                ex,
                "Failure");

            context.Errors.Add(new RequestError("Failure processing request"));

            return null;
        }
    }
}