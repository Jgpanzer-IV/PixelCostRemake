using PixelCost.Service.WalletAPI.Database;
using PixelCost.Service.WalletAPI.Model.Entities;
using PixelCost.Service.WalletAPI.Services.Interfaces;
using AutoMapper;
using PixelCost.Service.WalletAPI.Model.DTOs;

namespace PixelCost.Service.WalletAPI.Services.Implementations
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PaymentMethodRepository(ApplicationDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<PaymentMethodDTO?> CreateAsync(PaymentMethodDTO paymentMethodDTO)
        {
            PaymentMethod paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodDTO);

            _dbContext.PaymentMethods?.Add(paymentMethod);
            int result = await _dbContext.SaveChangesAsync();

            if (result == 1)
                return paymentMethodDTO;

            return null;
        }

        public Task<List<PaymentMethodDTO>?> RetrieveByWalletIdAsync(string walletId)
        {
            return Task.Run(() => { 
            
                if (_dbContext.PaymentMethods == null)
                    return null;

                List<PaymentMethod> paymentMethods = _dbContext.PaymentMethods.Where(e => e.WalletID == walletId).ToList();
                List<PaymentMethodDTO>? paymentMethodDTOs = _mapper.Map<List<PaymentMethodDTO>>(paymentMethods);

                if (paymentMethodDTOs.Count == 0)
                    return null;

                return paymentMethodDTOs;
            });
        }
        
        public async Task<PaymentMethodDTO?> UpdateAsync(PaymentMethodDTO paymentMethodDTO)
        {
            if (_dbContext.PaymentMethods == null)
                return null;


            if (_dbContext.PaymentMethods.FirstOrDefault(e => e.Id == paymentMethodDTO.ID) == null)
                return null;
            
            PaymentMethod updated = _mapper.Map<PaymentMethod>(paymentMethodDTO);
            _dbContext.PaymentMethods.Update(updated);
            int result = await _dbContext.SaveChangesAsync();
            
            if (result == 1)
                return paymentMethodDTO;
            
            return null;
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            PaymentMethod? selected = _dbContext.PaymentMethods?.FirstOrDefault(e => e.Id == Id);

            if (selected == null)
                return false;

            _dbContext.PaymentMethods?.Remove(selected);
            int result = await _dbContext.SaveChangesAsync();

            if(result == 1)
                return true;

            return false;
        }


    }
}
