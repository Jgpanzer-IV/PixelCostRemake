using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelCost.Client.Web.Models.upload
{
    public class CategoryDTO
    {
        public string Name {get;set;} = string.Empty;
        public decimal Cost {get;set;}
        public decimal Balance {get;set;}
        public decimal Expense {get;set;}
        public decimal AverageExpense {get;set;}

        public List<ExpenseDTO>? Expenses {get;set;}
    }
}