using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PixelCost.Client.Web.Models.upload;

namespace PixelCost.Client.Web.Models.content
{
    public class ShortProgressBar
    {
        public string Label {get;set;} = string.Empty;
        public string Description {get;set;} = string.Empty;
        public byte ValueProgress {get;set;}

        public ShortProgressBar()
        {}

        public ShortProgressBar(CategoryDTO categoryDTO){
            byte percentage = (byte) ((categoryDTO.Balance *100)/ categoryDTO.Cost);
            Label = "Balance "+ percentage;
            Description = "Balance "+ (categoryDTO.Balance)+ " from "+ categoryDTO.Cost;
            ValueProgress = percentage;
        }
    }
}