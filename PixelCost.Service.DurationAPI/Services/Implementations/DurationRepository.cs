using AutoMapper;
using PixelCost.Service.DurationAPI.Database;
using PixelCost.Service.DurationAPI.Models.DTOs;
using PixelCost.Service.DurationAPI.Models.Entities;
using PixelCost.Service.DurationAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PixelCost.Service.DurationAPI.Services.Implementations
{
    public class DurationRepository : IDurationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DurationRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<DurationDTO?> CreateAsync(DurationDTO durationDTO)
        {
            return Task.Run(() => { 
                Duration entity = _mapper.Map<Duration>(durationDTO);
                Duration? createdEntity = _dbContext.Durations?.Add(entity).Entity;
                int result = _dbContext.SaveChanges();
                return (createdEntity != null && result == 1)? _mapper.Map<DurationDTO>(createdEntity) : null;
            });
        }
        public Task<DurationDTO?> RetrieveByIdAsync(long id)
        {
            return Task.Run(() => {
                UpdateEntity(id);
                Duration? duration = _dbContext.Durations?.AsNoTracking().FirstOrDefault(e => e.Id == id);
                return (duration != null) ? _mapper.Map<DurationDTO>(duration) : null;
            });
        }

        public Task<IList<DurationDTO>?> RetrieveByUserIdAsync(string userId)
        {
            return Task.Run(() => {
                long[]? listId = _dbContext.Durations?.AsNoTracking()?.Where(e => e.UserId == userId).Select(e => e.Id ).ToArray();
                for (int i = 0; i < listId?.Length; i++) {
                    UpdateEntity(listId[i]);
                }
                IList<Duration>? durations = _dbContext.Durations?.AsNoTracking().Where(e => e.UserId == userId).ToList();
                return (durations != null && durations?.Count != 0) ? _mapper.Map<IList<DurationDTO>?>(durations) : null;
            });
        }
       
        public Task<DurationDTO?> UpdateAsync(DurationDTO durationDTO)
        {
            return Task.Run(() => { 
                Duration entity = _mapper.Map<Duration>(durationDTO);
                Duration? updatedEntity = _dbContext.Durations?.Update(entity).Entity;
                int result = _dbContext.SaveChanges();
                return (result == 1 && updatedEntity != null)? _mapper.Map<DurationDTO>(updatedEntity) : null;
            });
        }

        public Task<bool> DeleteAsync(long id)
        {
            return Task.Run(() => { 
            
                if (_dbContext.Durations == null)
                    return false;

                Duration deletedEntity = _dbContext.Durations.First(e => e.Id == id);
                _dbContext.Durations?.Remove(deletedEntity);
                int result = _dbContext.SaveChanges(); 
                
                return result == 1;
            });

        }

        private void UpdateEntity(long id) {
         
            Duration? duration = _dbContext.Durations?
                .Include(e => e.Categories)
                .Include(e => e.SubDurations)
                .Include(e => e.Revenues)
                .Include(e => e.PrimaryExpenses)
                .FirstOrDefault(e => e.Id == id);

            if (duration != null)
            {
                // Update entity information
                duration.TotalDays = duration.EndingDate.Subtract(duration.StartingDate).Days;
                
                duration.RemainingDays = duration.EndingDate.Subtract(DateTime.Now).Days;
                if (duration.RemainingDays < 0) 
                    duration.RemainingDays = 0;

                duration.Progress = ((duration.TotalDays - duration.RemainingDays) * 100) / duration.TotalDays ;
                if (duration.Progress < 0) 
                    duration.Progress = 0;
                else if (duration.Progress >= 100) {
                    duration.Progress = 100;
                    duration.IsActive = false;
                }
                duration.TotalCost = (duration.SubDurations?.Sum(e => e.Revenue)) + duration.InitialCost;

                duration.SumCategoryCost = duration.Categories?.Sum(e => e.Cost);
                duration.SumSubDurationCost = duration.SubDurations?.Sum(e => e.InitialCost);

                duration.UsableMoney = duration.InitialCost - (duration.SumCategoryCost + duration.SumSubDurationCost);
                duration.UsableMoney += duration.Categories?.Where(e => e.IsAchived == true).Sum(e => e.Balance);
                duration.UsableMoney += duration.SubDurations?.Where(e => e.IsAchived == true).Sum(e => e.Balance);

                duration.Revenue = duration.Revenues?.Sum(e => e.EarningAmount);
                duration.RevenueCount = duration.Revenues?.Count;
                duration.Expense = duration.Categories?.Sum(e => e.Expense) + duration.SubDurations?.Sum(e => e.Expense) + duration.PrimaryExpenses?.Sum(e => e.OrderingPrice);
                duration.ExpenseCount = duration.Categories?.Sum(e => e.ExpenseCount);

                duration.SumSubDurationBalance = duration.SubDurations?.Sum(e => e.Balance);
                duration.SumCategoryBalance = duration.Categories?.Sum(e => e.Balance);
                duration.Balance = duration.UsableMoney + duration.SumCategoryBalance + duration.SumSubDurationBalance;

                int result = _dbContext.SaveChanges();
            }
        }


        public Task<bool> IsExists(long id) {
            return Task.Run(() => {
                if (_dbContext.Durations?.AsNoTracking().FirstOrDefault(e => e.Id == id) == null)
                    return false;
                return true;
            });
        }
    }
}
