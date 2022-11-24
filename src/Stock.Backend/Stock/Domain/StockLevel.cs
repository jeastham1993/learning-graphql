namespace Stock.Backend.Stock.Domain;

public class StockLevel
{
    public StockLevel(){}
    
    public string Id { get; set; }
    
    public string ProductId { get; set; }
    
    public double FreeStock { get; set; }
    
    public double ReservedStock { get; set; }
}