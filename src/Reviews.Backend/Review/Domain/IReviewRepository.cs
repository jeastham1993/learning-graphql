namespace Reviews.Backend.Review.Domain;

public interface IReviewRepository
{
    Task<List<Review>> GetReviews(string productId);
}