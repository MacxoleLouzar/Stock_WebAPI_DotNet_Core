using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.CommentDto;
using api.Model;

namespace api.Dtos.StockDto
{
    public class StockDto
    {
        public int id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal PurchasePrice { get; set; }
        public decimal Lastdiv { get; set; }
        public string industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }

        public List<CommentsDto> Comments { get; set; }
    }
}