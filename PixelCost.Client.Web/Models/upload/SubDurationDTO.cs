using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelCost.Client.Web.Models.upload
{
    public class SubDurationDTO
    {
        public string Name {get;set;} = string.Empty;
        public DateTime StartingDate {get;set;}
        public DateTime EndingDate {get;set;}
        public int RemainDate {get;set;}
        public byte Progress {get;set;}
        public decimal Cost {get;set;}
        public decimal CostCategory {get;set;}
        public decimal Expense {get;set;}
        public decimal AverageExpense {get;set;}
        public decimal Balance {get;set;}

        public List<CategoryDTO>? Categories {get;set;}


    }
}