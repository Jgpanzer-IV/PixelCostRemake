using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelCost.Client.Web.Models.content
{
    public class FinancialStatus
    {

        public string? Label{get;set;}
        public string? Value {get;set;}
        public string? Status {get; init;}

        /// <summary>
        /// Instanciate a new object with the values given via parameter
        /// </summary>
        /// <param name="label">Label for the partial</param>
        /// <param name="value">Value that be used by the partial</param>
        /// <param name="status">Status indicate color of the partial, 0 : Cost, 1 : Balance, 2 : Expense, 3 : Average used, Order will be gray color.</param>
        public FinancialStatus(string? label, string? value, byte status = 4)
        {
            Label = label;
            Value = value;

            switch(status){
                case 0:
                    Status = "00b7ff";    
                break;

                case 1:
                    Status = "15d600";    
                break;
                
                case 2:
                    Status = "c40000";    
                break;

                case 3:
                    Status = "c40000";    
                break;

                default:
                    Status = "a9a9a9";
                break;
            }
        }
    }
}