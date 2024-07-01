using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    [Table("Stock")]
    public class Stock
    {
        public int id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18.2)")]
        public decimal PurchasePrice { get; set; }
        [Column(TypeName = "decimal(18.2)")]
        public decimal Lastdiv { get; set; }

        public string industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }

        public List<Comments> comments { get; set; } = new List<Comments>();
         public List<Portfolio> portfolios = new List<Portfolio>();
    }
}