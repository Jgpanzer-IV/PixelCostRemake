using PixelCost.Service.DurationAPI.Models.DTOs;

namespace PixelCost.Service.DurationAPI.Services.Interfaces
{
    public interface IDurationRepository
    {
        Task<IList<DurationDTO>?> RetrieveByUserIdAsync(string userId);
        Task<DurationDTO?> RetrieveByIdAsync(long id);
        Task<DurationDTO?> CreateAsync(DurationDTO durationDTO);
        Task<DurationDTO?> UpdateAsync(DurationDTO durationDTO);
        Task<bool> DeleteAsync(long id);
        Task<bool> IsExists(long id);
    }
}
