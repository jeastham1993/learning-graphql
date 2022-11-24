namespace Stock.Backend.Stock.Domain;

public interface IStockRepository
{
    Task<StockLevel?> GetStockLevels(string productId);
}