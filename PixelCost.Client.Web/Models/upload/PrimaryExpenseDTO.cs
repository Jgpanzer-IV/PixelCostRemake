using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelCost.Client.Web.Models.upload
{
    public class PrimaryExpenseDTO
    {
        public string Name {get;set;} = string.Empty;
        public decimal Price {get;set;}
        public DateTime OrderDate {get;set;}
        public string PaymentMethodID {get;set;} = string.Empty;
    }
}