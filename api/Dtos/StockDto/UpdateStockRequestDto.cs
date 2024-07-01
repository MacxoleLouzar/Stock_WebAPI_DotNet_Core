using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.StockDto
{
    public class UpdateStockRequestDto
    { [Required]
        [MaxLength(100)]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [MaxLength(10)]
        [Range(1, 1000000000)]
        public decimal PurchasePrice { get; set; }
        [Required]
        [MaxLength(6)]
        [Range(0.001, 100)]
        public decimal Lastdiv { get; set; }
        [Required]
        [MaxLength(100)]
        public string industry { get; set; } = string.Empty;
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}