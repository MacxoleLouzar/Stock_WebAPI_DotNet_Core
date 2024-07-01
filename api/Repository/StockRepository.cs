using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        async Task<List<Stock>> IStockRepository.GetAllStocksAsync(QueryObject query)
        {
            var stock = _context.Stocks.Include(x => x.comments).ThenInclude(z => z.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stock = stock.Where(x => x.CompanyName.ToLower().Contains(query.CompanyName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stock = stock.Where(x => x.Symbol.ToLower().Contains(query.Symbol.ToLower()));
            }

            //sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy == "Symbol")
                {
                    if (query.IsSorAscending)
                    {
                        stock = stock.OrderBy(x => x.Symbol);
                    }
                    else
                    {
                        stock = stock.OrderByDescending(x => x.Symbol);
                    }
                }
                else if (query.SortBy == "CompanyName")
                {
                   if(query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase)){
                     stock = query.IsSortDescending ? stock.OrderByDescending(x => x.CompanyName) : stock.OrderBy(x => x.CompanyName);
                   }
                }             
            }

            var offset = (query.PageNumber - 1) * query.PageSize;
            stock = stock.Skip(offset).Take(query.PageSize);

            return await stock.ToListAsync();
        }
        async Task<Stock> IStockRepository.GetStockByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(x => x.comments).FirstOrDefaultAsync(x => x.id == id);
            if (stock == null)
            {
                return null;
            }
            return stock;
        }

        async Task<Stock> IStockRepository.CreateStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        async Task<Stock> IStockRepository.UpdateStockAsync(int id, Stock stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.id == id);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.PurchasePrice = stockDto.PurchasePrice;
            existingStock.Lastdiv = stockDto.Lastdiv;
            existingStock.industry = stockDto.industry;
            existingStock.MarketCap = stockDto.MarketCap;
            await _context.SaveChangesAsync();
            return existingStock;
        }

        async Task<Stock> IStockRepository.DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.id == id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        async Task<bool> IStockRepository.StockExist(int id)
        {
            return await _context.Stocks.AnyAsync(x => x.id == id);
        }

        public Task<Stock?> GetStockBySymbol(string symbol)
        {
            return _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        }
    }
}