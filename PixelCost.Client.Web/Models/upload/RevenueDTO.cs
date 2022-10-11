using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelCost.Client.Web.Models.upload
{
    public class RevenueDTO
    {
        public string Name {get;set;} = string.Empty;
        public string Price {get;set;} = string.Empty;
        public DateTime OrderDate {get;set;}
        public string PaymentMethodID {get;set;} = string.Empty;
    }
}