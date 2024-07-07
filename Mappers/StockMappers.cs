using finapp_backend.Dtos.Stock;
using finapp_backend.Models;

namespace finapp_backend.Mappers;
public static class StockMappers
{
    public static StockDtos ToStockDto(this Stock stock)
    {
        return new StockDtos
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap,
            Comments = stock.Comments.Select(x => x.ToCommentDto()).ToList()
        };
    }
    public static Stock ToStockFromCreateDto(this CreateStockDto createStockDto)
    {
        return new Stock
        {
            Symbol = createStockDto.Symbol,
            CompanyName = createStockDto.CompanyName,
            Purchase = createStockDto.Purchase,
            LastDiv = createStockDto.LastDiv,
            Industry = createStockDto.Industry,
            MarketCap = createStockDto.MarketCap
        };
    }
}