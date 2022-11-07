using System;
using System.Collections.Generic;
using PixelCost.Client.Web.Models.upload;

namespace PixelCost.Client.Web.Models.content
{
    public class ListItem
    {
        public string? Name {get;set;}
        public string? ValueElement {get;set;}
        public string? Info {get;set;}
        public string? Description {get;set;}        

        public ListItem()
        {}

        /// <summary>
        /// Initiate a new instance using a single 'PrimaryExpenseDTO' object
        /// </summary>
        /// <param name="primaryExpenseDTO">An object that be used to set all values</param>
        public ListItem(PrimaryExpenseDTO? primaryExpenseDTO)
        {
            Name = primaryExpenseDTO?.OrderingName;
            ValueElement = primaryExpenseDTO?.OrderingPrice.ToString();
            Info = primaryExpenseDTO?.OrderingDate.ToLongDateString();
            Description = primaryExpenseDTO?.PaymentMethodId.ToString();
        }
        
        /// <summary>
        /// Initiate a new instance using a single 'ExpenseDTO' object
        /// </summary>
        /// <param name="expenseDTO">An object that be used to set all values</param>
        public ListItem(ExpenseDTO expenseDTO){
            Name = expenseDTO.Name;
            ValueElement = expenseDTO.OrderPrice.ToString();
            Info = expenseDTO.OrderDate.ToLongDateString();
            Description = expenseDTO.PaymentID;
        }

    }
}