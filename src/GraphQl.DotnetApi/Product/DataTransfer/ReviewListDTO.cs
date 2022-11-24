namespace GraphQl.DotnetApi.Product.DataTransfer;

public class ReviewListDTO
{
    public ReviewListDTO()
    {
        this.Reviews = new List<ReviewDTO>();
    }
    
    public List<ReviewDTO> Reviews { get; set; }
}