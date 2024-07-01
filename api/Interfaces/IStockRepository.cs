using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.StockDto;
using api.Helpers;
using api.Model;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(QueryObject query);
        Task<Stock> GetStockByIdAsync(int id);
        Task<Stock?> GetStockBySymbol(string symbol);
        Task<Stock> CreateStockAsync(Stock stock);
        Task<Stock> UpdateStockAsync(int id, Stock stock);
        Task<Stock> DeleteStockAsync(int id);
        Task<bool> StockExist(int id);
    }
}