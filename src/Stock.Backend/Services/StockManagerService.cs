namespace Stock.Backend.Services;

using global::Stock.Backend.Stock.Domain;

using Grpc.Core;

public class StockManagerService : StockManager.StockManagerBase
{
    private readonly ILogger<StockManagerService> _logger;
    private readonly IStockRepository _stockRepository;
    
    public StockManagerService(ILogger<StockManagerService> logger, IStockRepository StockRepository)
    {
        this._logger = logger;
        this._stockRepository = StockRepository;
    }

    /// <inheritdoc />
    public override async Task<StockLevelReply> GetCurrentStockLevels(GetCurrentStockRequest request, ServerCallContext context)
    {
        var currentStockLevel = await this._stockRepository.GetStockLevels(request.ProductId);
        
        if (currentStockLevel == null)
        {
            throw new ArgumentException("Stock not found");
        }

        return new StockLevelReply()
        {
            FreeStock = currentStockLevel.FreeStock,
            ReservedStock = currentStockLevel.ReservedStock
        };
    }
}
