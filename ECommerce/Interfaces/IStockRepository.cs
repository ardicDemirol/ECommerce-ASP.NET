using ECommerce.Dtos.Stock;
using ECommerce.Helpers;
using ECommerce.Models;

namespace ECommerce.Interfaces;
public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock?> GetBySymbolAsync(string symbol);
    Task<Stock?> CreateAsync(Stock stock);
    Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);
    Task<Stock> DeleteAsync(int id);
    Task<bool> StockExists(int id);
}
