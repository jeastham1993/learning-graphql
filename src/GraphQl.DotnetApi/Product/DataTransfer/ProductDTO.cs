namespace GraphQl.DotnetApi.Product.DataTransfer;

public class ProductDTO
{
    public ProductDTO()
    {
    }

    public ProductDTO(
        string id)
    {
        this.Id = id;
    }
    
    public string Id { get; set; }
    
    public ProductDetailDTO Details { get; set; }
    
    public List<ReviewDTO> Reviews { get; set; }
    
    public StockLevelDTO StockLevel { get; set; }
}