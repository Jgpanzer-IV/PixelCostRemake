using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelCost.Client.Web.Models.upload
{
    public class PrimaryExpenseDTO
    {
        public long Id { get; set; }
        public string OrderingName { get; set; } = string.Empty;
        public decimal OrderingPrice { get; set; }
        public DateTime OrderingDate { get; set; }

        public string UserId { get; set; } = string.Empty;
        public long DurationId { get; set; }
        public long PaymentMethodId { get; set; }
    }
}