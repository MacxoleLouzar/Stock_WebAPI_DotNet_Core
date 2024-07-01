using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.StockDto;
using api.Model;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel){
          return new StockDto{
            id = stockModel.id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            PurchasePrice = stockModel.PurchasePrice,
            Lastdiv = stockModel.Lastdiv,
            industry = stockModel.industry,
            MarketCap = stockModel.MarketCap,
            Comments = stockModel.comments.Select(x => x.ToCommentsDto()).ToList()
          };
        } 

        public static Stock ToCreateStockRequestDto(this CreateStockRequestDto stockDto){
            return new Stock{
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                PurchasePrice = stockDto.PurchasePrice,
                Lastdiv = stockDto.Lastdiv,
                industry = stockDto.industry,
                MarketCap = stockDto.MarketCap
            };  
        }
        public static Stock ToUpdateStockRequestDto(this UpdateStockRequestDto stockDto){
            return new Stock{
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                PurchasePrice = stockDto.PurchasePrice,
                Lastdiv = stockDto.Lastdiv,
                industry = stockDto.industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}