using Microsoft.EntityFrameworkCore;
using PixelCost.Service.RecordingAPI.Database;
using PixelCost.Service.RecordingAPI.Models.DTOs;
using PixelCost.Service.RecordingAPI.Services.Interfaces;

namespace PixelCost.Service.RecordingAPI.Services.Implementations
{
    public class RevenueRepository : IRevenueRepository
    {

        private readonly ApplicationDbContext _dbContext;
    

        public RevenueRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task<RevenueDTO?> CreateAsync(RevenueDTO revenueDTO) => Task.Run(() => {
            RevenueDTO? createdEntity = _dbContext.Revenues?.Add(revenueDTO).Entity;
            int result = _dbContext.SaveChanges();
            return (result == 1) ? createdEntity : null;
        });

        public Task<bool> DeleteAsync(long id) => Task.Run(() => {
            RevenueDTO? deletedEntity = _dbContext.Revenues?.First(e => e.Id == id);
            if (deletedEntity == null)
                return false;
            _dbContext.Revenues?.Remove(deletedEntity);
            int result = _dbContext.SaveChanges();
            return (result == 1) ? true : false;
        });

        public Task<IList<RevenueDTO>?> RetrieveByDurationIdAsync(long id) => Task.Run(() => {
            IList<RevenueDTO>? result = _dbContext.Revenues?.AsNoTracking().Where(e => e.DurationId == id).ToList();
            return (result == null || result.Count == 0) ? null : result;
        });

        public Task<IList<RevenueDTO>?> RetrieveBySubDurationIdAsync(long id) => Task.Run(() => {
            IList<RevenueDTO>? result = _dbContext.Revenues?.AsNoTracking().Where(e => e.SubDurationId == id).ToList();
            return (result == null || result.Count == 0) ? null : result;
        });

        public Task<IList<RevenueDTO>?> RetrieveByUserIdAsync(string userId) => Task.Run(() => {
            IList<RevenueDTO>? result = _dbContext.Revenues?.AsNoTracking().Where(e => e.UserId == userId).ToList();
            return (result == null || result.Count == 0) ? null : result;
        });


        public Task<RevenueDTO?> RetrieveByIdAsync(long id) => Task.Run(() => {
            return _dbContext.Revenues?.AsNoTracking().FirstOrDefault(e => e.Id == id);
        });


        public Task<RevenueDTO?> UpdateAsync(RevenueDTO revenueDTO) => Task.Run(() => {
            RevenueDTO? updatedEntity = _dbContext.Revenues?.Update(revenueDTO).Entity;
            int result = _dbContext.SaveChanges();
            return (result == 1) ? updatedEntity : null;
        });
            
    }
}
