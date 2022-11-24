namespace Reviews.Backend.Review.Domain;

public class Review
{
    public Review(){}
    
    public string ProductId { get; set; }
    
    public int Stars { get; set; }
    
    public string Contents { get; set; }
}