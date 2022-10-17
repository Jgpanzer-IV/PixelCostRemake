﻿namespace PixelCost.Service.EventAPI.Models.DTOs
{
    public class CategoryDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public decimal? Expense { get; set; }
        public int? ExpenseCount { get; set; }
        public decimal? Balance { get; set; }
        public bool? IsAchived { get; set; }
        public long SubDurationId { get; set; } 

    }
}
