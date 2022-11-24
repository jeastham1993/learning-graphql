namespace GraphQl.DotnetApi.Product.GraphQL.Types;

using global::GraphQL.Execution;
using global::GraphQL.Types;

using GraphQl.DotnetApi.Product.DataTransfer;

using GrpcProductClient;

using GrpcReviewClient;

using GrpcStockClient;

public class ProductType : ObjectGraphType<ProductDTO>
{
    public ProductType(ProductManager.ProductManagerClient client,
        ReviewManager.ReviewManagerClient reviewClient,
        StockManager.StockManagerClient stockClient,
        ILogger<ProductType> logger)
    {
        this.Field(x => x.Id);
        
        this.Field<ProductDetailType, ProductDetailDTO>("details")
            .ResolveAsync(
                async context =>
                {
                    try
                    {
                        logger.LogInformation("Starting detail query");

                        var result = await client.GetProductAsync(
                            new GetProductRequest()
                            {
                                Id = context.Source.Id
                            });

                        logger.LogInformation("Detail query complete");

                        return new ProductDetailDTO()
                        {
                            Name = result.Name,
                            Price = result.Price
                        };
                    }
                    catch (Exception e)
                    {
                        logger.LogError(
                            e,
                            "Failure querying");
                        
                        context.Errors.Add(new RequestError(e.Message));

                        return null;
                    }
                });

        this.Field<ListGraphType<ReviewType>, List<ReviewDTO>>("reviews")
            .ResolveAsync(
                async context =>
                {
                    try
                    {
                        logger.LogInformation("Starting review query");

                        var reviewResponse = reviewClient.GetReviews(new GetReviewRequest()
                        {
                            ProductId = context.Source.Id
                        });

                        var reviews = new List<ReviewDTO>(); 
                        
                        while (await reviewResponse.ResponseStream.MoveNext(default))
                        {
                            var currentReview = reviewResponse.ResponseStream.Current;
                            reviews.Add(new ReviewDTO()
                            {
                                Contents = currentReview.Contents,
                                Stars = Convert.ToInt32(currentReview.Stars)
                            });
                        }
                        
                        logger.LogInformation("Finished review query");

                        return reviews;
                    }
                    catch (Exception e)
                    {
                        logger.LogError(
                            e,
                            "Failure querying");
                        
                        context.Errors.Add(new RequestError(e.Message));

                        return null;
                    }
                });
        
        this.Field<StockLevelType, StockLevelDTO>("stockLevel")
            .ResolveAsync(
                async context =>
                {
                    try
                    {
                        logger.LogInformation("Starting stock query");

                        var stockLevel = await stockClient.GetCurrentStockLevelsAsync(
                            new GetCurrentStockRequest()
                            {
                                ProductId = context.Source.Id
                            });

                        logger.LogInformation("Completing stock query");

                        return new StockLevelDTO()
                        {
                            FreeStock = stockLevel.FreeStock,
                            ReservedStock = stockLevel.ReservedStock
                        };
                    }
                    catch (Exception e)
                    {
                        logger.LogError(
                            e,
                            "Failure querying");
                        
                        context.Errors.Add(new RequestError(e.Message));

                        return null;
                    }
                });
    }
}