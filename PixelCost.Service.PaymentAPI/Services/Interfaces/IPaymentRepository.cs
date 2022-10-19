using PixelCost.Service.PaymentAPI.Models.DTOs;

namespace PixelCost.Service.PaymentAPI.Services.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<PaymentMethodDTO>?> RetrieveByUserIdAsync(string userId);
        Task<PaymentMethodDTO?> RetrieveByIdAsync(long id);
        Task<List<ExpenseDTO>?> RetrievePaymentExpenseById(long id);
        Task<List<PrimaryExpenseDTO>?> RetrievePaymentPrimaryExpenseById(long id);
        Task<List<RevenueDTO>?> RetrievePaymentRevenueById(long id);
        Task<PaymentMethodDTO?> CreateAsync(PaymentMethodDTO paymentMethodDTO);
        Task<PaymentMethodDTO?> UpdateAsync(PaymentMethodDTO paymentMethodDTO);
        Task<bool> DeleteAsync(long Id);
        Task<bool> IsExists(long id); 
    }
}
