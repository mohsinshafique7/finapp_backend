using finapp_backend.Dtos.Stock;
using finapp_backend.Helpers;
using finapp_backend.Models;

namespace finapp_backend.Interfaces;
public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock?> GetBySymbolAsync(string symbol);

    Task<Stock> CreateAsync(Stock stock);
    Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> stockExists(int id);
}