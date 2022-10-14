using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PixelCost.Client.Web.Models.upload;

namespace PixelCost.Client.Web.Models.content
{
    public class FullProgressBar
    {
        public string? Value01 {get;set;}
        public string? Value02 {get;set;}
        public string? Value03 {get;set;}
        public string? Label01 {get;set;}
        public string? Label02 {get;set;}
        public string? Label03 {get;set;}
        public string? Description {get;set;}
        public string? Information {get;set;}
        public byte Progress {get;set;}


        public FullProgressBar()
        {}

        public FullProgressBar(DurationDTO durationDTO)
        {
            Value01 = durationDTO.StartingDate.ToShortDateString();
            Value02 = DateTime.Now.ToShortDateString();
            Value03 = durationDTO.EndingDate.ToShortDateString();
            Label01 = "Starting Date";
            Label02 = "Current";
            Label03 = "Endign Date";
            Description = "Remain "+ durationDTO.RemainDate +" will reach the end.";
            Information = "Progress " + durationDTO.Progress + " %";
            Progress = durationDTO.Progress;
        }

        public FullProgressBar(SubDurationDTO subDurationDTO)
        {
            Value01 = subDurationDTO.StartingDate.ToShortDateString();
            Value02 = DateTime.Now.ToShortDateString();
            Value03 = subDurationDTO.EndingDate.ToShortDateString();
            Label01 = "Starting Date";
            Label02 = "Current";
            Label03 = "Ending Date";
            Description = "Remain "+ subDurationDTO.RemainDate +" will reach the end.";
            Information = "Progress "+ subDurationDTO.Progress +" %";
            Progress = subDurationDTO.Progress;
        
        }
    }
}