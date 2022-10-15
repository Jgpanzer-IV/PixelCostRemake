
using PixelCost.Service.WalletAPI.Model.DTOs;

namespace PixelCost.Service.WalletAPI.Services.Interfaces
{
    public interface IPaymentMethodRepository
    {
        Task<List<PaymentMethodDTO>?> RetrieveByWalletIdAsync(string walletId);
        Task<PaymentMethodDTO?> CreateAsync(PaymentMethodDTO paymentMethodDTO);
        Task<PaymentMethodDTO?> UpdateAsync(PaymentMethodDTO paymentMethodDTO);
        Task<bool> DeleteAsync(long Id);
    }
}
