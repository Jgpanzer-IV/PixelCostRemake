using PixelCost.Service.WalletAPI.Model.DTOs;

namespace PixelCost.Service.WalletAPI.Services.Interfaces
{
    public interface IWalletRepository
    {
        Task<WalletDTO?> RetrieveByIdAsync(string userId);
        Task<WalletDTO?> RetrieveUpdatedByIdAsync(string userId);
        Task<WalletDTO?> CreateAsync(WalletDTO wallet);
        Task<WalletDTO?> UpdateAsync(WalletDTO wallet);
        Task<bool> DeleteAsync(string userId); 
    }
}
