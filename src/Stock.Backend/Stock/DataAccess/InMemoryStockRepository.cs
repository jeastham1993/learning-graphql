namespace Stock.Backend.Stock.DataAccess;

using global::Stock.Backend.Stock.Domain;

public class InMemoryStockRepository : IStockRepository
{
    private List<StockLevel> _stockLevels;

    public InMemoryStockRepository()
    {
        this._stockLevels = new List<StockLevel>();
    }
    
    /// <inheritdoc />
    public async Task<StockLevel?> GetStockLevels(string productId)
    {
        var stockLevel = this._stockLevels.FirstOrDefault(
            p => p.Id.Equals(
                productId,
                StringComparison.OrdinalIgnoreCase));

        if (stockLevel == null)
        {
            stockLevel = new StockLevel()
            {
                FreeStock = 0,
                ReservedStock = 0
            };
        }

        return stockLevel;
    }
}