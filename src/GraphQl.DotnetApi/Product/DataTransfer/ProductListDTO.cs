namespace GraphQl.DotnetApi.Product.DataTransfer;

public class ProductListDTO
{
    public ProductListDTO()
    {
        this.Products = new List<ProductDTO>();
    }
    
    public ProductListDTO(IEnumerable<ProductDTO> products)
    {
        this.Products = products;
    }
    
    public IEnumerable<ProductDTO> Products { get; set; }
}