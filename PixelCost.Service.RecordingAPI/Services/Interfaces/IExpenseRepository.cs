using PixelCost.Service.RecordingAPI.Models.DTOs;

namespace PixelCost.Service.RecordingAPI.Services.Interfaces
{
    public interface IExpenseRepository
    {
        Task<IList<ExpenseDTO>?> RetrieveByUserIdAsync(string userId);
        Task<IList<ExpenseDTO>?> RetrieveByDurationIdAsync(long id);
        Task<IList<ExpenseDTO>?> RetrieveBySubDurationIdAsync(long id);
        Task<IList<ExpenseDTO>?> RetrieveByCategoryIdAsync(long id);
        Task<ExpenseDTO?> RetrieveByIdAsync(long id);
        Task<ExpenseDTO?> CreateAsync(ExpenseDTO expenseDTO);
        Task<ExpenseDTO?> UpdateAsync(ExpenseDTO expenseDTO);
        Task<bool> DeleteAsync(long id);
    }
}
