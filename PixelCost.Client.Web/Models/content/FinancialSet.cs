using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PixelCost.Client.Web.Models.upload;

namespace PixelCost.Client.Web.Models.content
{
    public class FinancialSet
    {
        public FinancialStatus? Cost {get;set;}
        public FinancialStatus? Balance {get;set;}
        public FinancialStatus? Expense {get;set;}
        public FinancialStatus? AverageUsed {get;set;}

        public FinancialSet()
        {}

        public FinancialSet(DurationDTO durationDTO)
        {
            Cost = new FinancialStatus("Cost",durationDTO.InitialCost.ToString(),0);
            Balance = new FinancialStatus("Balance",durationDTO.Balance.ToString(),1);
            Expense = new FinancialStatus("Expense",durationDTO.Expense.ToString(),2);
            AverageUsed = new FinancialStatus("Average used per day",(durationDTO.Expense / durationDTO.ExpenseCount).ToString(),3);
        }

        public FinancialSet(SubDurationDTO subDurationDTO){
            Cost = new FinancialStatus("Cost",subDurationDTO.Cost.ToString(),0);
            Balance = new FinancialStatus("Balance",subDurationDTO.Balance.ToString(),1);
            Expense = new FinancialStatus("Expense",subDurationDTO.Expense.ToString(),2);
            AverageUsed = new FinancialStatus("Average used per day",subDurationDTO.AverageExpense.ToString(),3);
        }

        public FinancialSet(CategoryDTO categoryDTO){
            Cost = new FinancialStatus("Cost",categoryDTO.Cost.ToString(),0);
            Balance = new FinancialStatus("Balance",categoryDTO.Balance.ToString(),1);
            Expense = new FinancialStatus("Expense",categoryDTO.Expense.ToString(),2);
            AverageUsed = new FinancialStatus("Average used per day",categoryDTO.AverageExpense.ToString(),3);
        }

    }
}