using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelCost.Client.Web.Models.upload
{
    public class WalletDTO
    {
        public string UserID { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public decimal? Balance { get; set; }
        public decimal? TotalExpense { get; set; }
        public int TotalNumberExpense { get; set; }
        public decimal? TotalRevenue { get; set; }
        public int TotalNumberRevenue { get; set; }
        public DateTime? DateCreate { get; set; }

    }
}