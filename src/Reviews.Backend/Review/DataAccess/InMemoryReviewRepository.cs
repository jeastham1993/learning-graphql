namespace Reviews.Backend.Review.DataAccess;

using Reviews.Backend.Review.Domain;

public class InMemoryReviewRepository : IReviewRepository
{
    private List<Review> _reviews;

    public InMemoryReviewRepository()
    {
        this._reviews = new List<Review>();
    }
    
    /// <inheritdoc />
    public async Task<List<Review>> GetReviews(string id)
    {
        return this._reviews.Where(
            p => p.ProductId.Equals(
                id,
                StringComparison.OrdinalIgnoreCase)).ToList();
    }
}