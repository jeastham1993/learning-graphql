namespace Reviews.Backend.Services;

using Grpc.Core;

using Reviews.Backend.Review.Domain;

public class ReviewManagerService : ReviewManager.ReviewManagerBase
{
    private readonly ILogger<ReviewManagerService> _logger;
    private readonly IReviewRepository reviewRepository;
    
    public ReviewManagerService(ILogger<ReviewManagerService> logger, IReviewRepository reviewRepository)
    {
        this._logger = logger;
        this.reviewRepository = reviewRepository;
    }

    /// <inheritdoc />
    public override async Task GetReviews(
        GetReviewRequest request,
        IServerStreamWriter<ReviewReply> responseStream,
        ServerCallContext context)
    {
        var reviews = await this.reviewRepository.GetReviews(request.ProductId);

        foreach (var review in reviews)
        {
            await responseStream.WriteAsync(new ReviewReply()
            {
                Contents = review.Contents,
                Stars = review.Stars
            });
        }
    }
}
