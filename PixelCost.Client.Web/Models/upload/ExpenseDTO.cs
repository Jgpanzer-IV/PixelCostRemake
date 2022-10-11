using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelCost.Client.Web.Models.upload
{
    public class ExpenseDTO
    {
        public string Name {get;set;} = string.Empty;
        public DateTime OrderDate {get;set;}
        public decimal OrderPrice {get;set;}
        public string PaymentID {get;set;} = string.Empty; 
    }
}