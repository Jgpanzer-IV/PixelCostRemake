using PixelCost.Service.EventAPI.Models.DTOs;

namespace PixelCost.Service.EventAPI.Services.Interfaces
{
    public interface ISubDurationRepository
    {
        Task<IList<SubDurationDTO>?> RetrieveByUserIdAsync(string userId);
        Task<SubDurationDTO?> RetrieveByIdAsync(long id);
        Task<SubDurationDTO?> CreateAsync(SubDurationDTO subDurationDTO);
        Task<SubDurationDTO?> UpdateAsync(SubDurationDTO subDurationDTO);
        Task<bool> DeleteAsync(long id);

    }
}
