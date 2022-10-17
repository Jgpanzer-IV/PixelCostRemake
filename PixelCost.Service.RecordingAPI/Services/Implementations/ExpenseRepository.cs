using Microsoft.EntityFrameworkCore;
using PixelCost.Service.RecordingAPI.Database;
using PixelCost.Service.RecordingAPI.Models.DTOs;
using PixelCost.Service.RecordingAPI.Services.Interfaces;

namespace PixelCost.Service.RecordingAPI.Services.Implementations
{
    public class ExpenseRepository : IExpenseRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public ExpenseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task<ExpenseDTO?> CreateAsync(ExpenseDTO expenseDTO) => Task.Run(() =>
        {
            ExpenseDTO? createdEntity = _dbContext.Expenses?.Add(expenseDTO).Entity;
            int result = _dbContext.SaveChanges();
            return (result == 1) ? createdEntity : null;
        });


        public Task<bool> DeleteAsync(long id) => Task.Run(() => {
            ExpenseDTO? deletedEntity = _dbContext.Expenses?.First(e => e.Id == id);
            if (deletedEntity == null)
                return false;
            _dbContext.Expenses?.Remove(deletedEntity);
            int result = _dbContext.SaveChanges();
            return (result == 1) ? true : false;
        });

        public Task<IList<ExpenseDTO>?> RetrieveByCategoryIdAsync(long id) => Task.Run(() => {
            IList<ExpenseDTO>? result = _dbContext.Expenses?.AsNoTracking().Where(e => e.CategoryId == id).ToList();
            return (result == null || result.Count == 0) ? null : result;
        });

        public Task<IList<ExpenseDTO>?> RetrieveByDurationIdAsync(long id) => Task.Run(() => {
            IList<ExpenseDTO>? result = _dbContext.Expenses?.AsNoTracking().Where(e => e.DurationId == id).ToList();
            return (result == null || result.Count == 0) ? null : result;
        });

        public Task<IList<ExpenseDTO>?> RetrieveBySubDurationIdAsync(long id) => Task.Run(() => {
            IList<ExpenseDTO>? result = _dbContext.Expenses?.AsNoTracking().Where(e => e.SubDurationId == id).ToList();
            return (result == null || result.Count == 0) ? null : result;
        });

        public Task<IList<ExpenseDTO>?> RetrieveByUserIdAsync(string userId) => Task.Run(() => {
            IList<ExpenseDTO>? result = _dbContext.Expenses?.AsNoTracking().Where(e => e.UserId == userId).ToList();
            return (result == null || result.Count == 0) ? null : result;
        });

        public Task<ExpenseDTO?> RetrieveByIdAsync(long id) => Task.Run(() => {
            return _dbContext.Expenses?.AsNoTracking().FirstOrDefault(e => e.Id == id);
        });


        public Task<ExpenseDTO?> UpdateAsync(ExpenseDTO expenseDTO) => Task.Run(() => {
            ExpenseDTO? updatedEntity = _dbContext.Expenses?.Update(expenseDTO).Entity;
            int result = _dbContext.SaveChanges();
            return (result == 1) ? updatedEntity : null;
        });
    }
}
