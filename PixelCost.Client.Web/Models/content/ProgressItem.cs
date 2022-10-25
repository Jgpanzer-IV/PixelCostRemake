using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PixelCost.Client.Web.Models.upload;

namespace PixelCost.Client.Web.Models.content
{
    public class ProgressItem
    {
        public long? Id { get; set; }
        public string MainText {get;set;} = string.Empty;
        public string Description {get;set;} = string.Empty;
        public string InformationText {get;set;} = string.Empty;
        public string ItemName {get;set;} = string.Empty;
        public string Value {get;set;} = string.Empty;
        public float? Progress {get;set;}

        public ProgressItem()
        { }

        public ProgressItem(DurationDTO durationDTO){
            Id = durationDTO.Id;
            ItemName = durationDTO.Name;
            MainText = "Progress : " + durationDTO.Progress.ToString() + " %";
            Value = "Balance " + durationDTO.Balance?.ToString("C");
            Progress = durationDTO.Progress;
            InformationText = (durationDTO.RemainingDays > durationDTO.TotalDays)? "Still not reache the active duration.":"Remain "+ durationDTO.RemainingDays+ " Day will reach the end.";
        }   

        public ProgressItem(SubDurationDTO subDurationDTO){
            ItemName = subDurationDTO.Name;
            Value = "Cost : " + subDurationDTO.Cost;
            MainText = "Progress : " + subDurationDTO.Progress;
            InformationText = (subDurationDTO.RemainDate == 0)? "Passed":"Left "+subDurationDTO.RemainDate+" will reach the end";
            Progress = subDurationDTO.Progress;
        }

        public ProgressItem(CategoryDTO categoryDTO){
            ItemName = categoryDTO.Name;
            MainText = "Balance "+((categoryDTO.Balance * 100) / categoryDTO.Cost).ToString() +" %";
            Progress = (byte) ((categoryDTO.Balance * 100) / categoryDTO.Cost);
            InformationText = "Expense : " + categoryDTO.Expense;
            Value = "Cost : " + categoryDTO.Cost;
        }

    }
}