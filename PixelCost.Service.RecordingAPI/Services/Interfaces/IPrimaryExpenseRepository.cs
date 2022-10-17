using PixelCost.Service.RecordingAPI.Models.DTOs;

namespace PixelCost.Service.RecordingAPI.Services.Interfaces
{
    public interface IPrimaryExpenseRepository
    {
        Task<IList<PrimaryExpenseDTO>?> RetrieveByUserIdAsync(string userId);
        Task<IList<PrimaryExpenseDTO>?> RetrieveByDurationIdAsync(long id);
        Task<IList<PrimaryExpenseDTO>?> RetrieveBySubDurationIdAsync(long id);
        Task<IList<PrimaryExpenseDTO>?> RetrieveByCategoryIdAsync(long id);
        Task<PrimaryExpenseDTO?> RetrieveByIdAsync(long id);
        Task<PrimaryExpenseDTO?> CreateAsync(PrimaryExpenseDTO expenseDTO);
        Task<PrimaryExpenseDTO?> UpdateAsync(PrimaryExpenseDTO expenseDTO);
        Task<bool> DeleteAsync(long id);
    }
}
