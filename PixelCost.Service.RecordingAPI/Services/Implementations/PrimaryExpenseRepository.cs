using Microsoft.EntityFrameworkCore;
using PixelCost.Service.RecordingAPI.Database;
using PixelCost.Service.RecordingAPI.Models.DTOs;
using PixelCost.Service.RecordingAPI.Services.Interfaces;

namespace PixelCost.Service.RecordingAPI.Services.Implementations
{
    public class PrimaryExpenseRepository : IPrimaryExpenseRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public PrimaryExpenseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task<PrimaryExpenseDTO?> CreateAsync(PrimaryExpenseDTO primaryExpenseDTO) => Task.Run(() => {
            PrimaryExpenseDTO? createdEntity = _dbContext.PrimaryExpenses?.Add(primaryExpenseDTO).Entity;
            int result = _dbContext.SaveChanges();
            return (result == 1) ? createdEntity : null;
        });

        public Task<bool> DeleteAsync(long id) => Task.Run(() => {
            PrimaryExpenseDTO? deletedEntity = _dbContext.PrimaryExpenses?.First(e => e.Id == id);
            if (deletedEntity == null)
                return false;
            _dbContext.PrimaryExpenses?.Remove(deletedEntity);
            int result = _dbContext.SaveChanges();
            return (result == 1) ? true : false;
        });

        public Task<IList<PrimaryExpenseDTO>?> RetrieveByDurationIdAsync(long id) => Task.Run(() => {
            IList<PrimaryExpenseDTO>? result = _dbContext.PrimaryExpenses?.AsNoTracking().Where(e => e.DurationId == id).ToList();
            return (result == null || result.Count == 0) ? null : result;
        });
    
        public Task<IList<PrimaryExpenseDTO>?> RetrieveByUserIdAsync(string userId) => Task.Run(() => {
            IList<PrimaryExpenseDTO>? result = _dbContext.PrimaryExpenses?.AsNoTracking().Where(e => e.UserId == userId).ToList();
            return (result == null || result.Count == 0) ? null : result;
        });

        public Task<PrimaryExpenseDTO?> RetrieveByIdAsync(long id) => Task.Run(() => {
            return _dbContext.PrimaryExpenses?.AsNoTracking().FirstOrDefault(e => e.Id == id);
        });


        public Task<PrimaryExpenseDTO?> UpdateAsync(PrimaryExpenseDTO primaryExpenseDTO) => Task.Run(() => {
            PrimaryExpenseDTO? updatedEntity = _dbContext.PrimaryExpenses?.Update(primaryExpenseDTO).Entity;
            int result = _dbContext.SaveChanges();
            return (result == 1) ? updatedEntity : null;
        });
    }
}
