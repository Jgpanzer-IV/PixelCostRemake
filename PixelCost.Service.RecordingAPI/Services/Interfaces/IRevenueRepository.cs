using PixelCost.Service.RecordingAPI.Models.DTOs;

namespace PixelCost.Service.RecordingAPI.Services.Interfaces
{
    public interface IRevenueRepository
    {
        Task<IList<RevenueDTO>?> RetrieveByUserIdAsync(string userId);
        Task<IList<RevenueDTO>?> RetrieveByDurationIdAsync(long id);
        Task<IList<RevenueDTO>?> RetrieveBySubDurationIdAsync(long id);
        Task<RevenueDTO?> RetrieveByIdAsync(long id);
        Task<RevenueDTO?> CreateAsync(RevenueDTO revenueDTO);
        Task<RevenueDTO?> UpdateAsync(RevenueDTO revenueDTO);
        Task<bool> DeleteAsync(long id);
    }
}
