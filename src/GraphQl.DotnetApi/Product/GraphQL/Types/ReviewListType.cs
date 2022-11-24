namespace GraphQl.DotnetApi.Product.GraphQL.Types;

using global::GraphQL.Types;

using GraphQl.DotnetApi.Product.DataTransfer;

public class ReviewListType : ObjectGraphType<ReviewListDTO>
{
    public ReviewListType()
    {
        this.Field<ListGraphType<ReviewType>>("reviews");
    }
}