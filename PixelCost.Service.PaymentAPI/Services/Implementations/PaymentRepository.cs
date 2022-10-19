using PixelCost.Service.PaymentAPI.Database;
using PixelCost.Service.PaymentAPI.Models.Entities;
using AutoMapper;
using PixelCost.Service.PaymentAPI.Models.DTOs;
using PixelCost.Service.PaymentAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PixelCost.Service.PaymentAPI.Services.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PaymentRepository(ApplicationDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<PaymentMethodDTO?> CreateAsync(PaymentMethodDTO paymentMethodDTO)
        {
            // Check for exists user, Because only exists user can create new entity.

            PaymentMethod paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodDTO);

            _dbContext.PaymentMethods?.Add(paymentMethod);
            int result = await _dbContext.SaveChangesAsync();

            if (result == 1)
                return paymentMethodDTO;

            return null;
        }

        public Task<List<PaymentMethodDTO>?> RetrieveByUserIdAsync(string userId)
        {
            return Task.Run(() => {

                if (_dbContext.PaymentMethods == null)
                    return null;

                List<PaymentMethod?> paymentMethods = new List<PaymentMethod?>();
                long[] listIDUserPayment = _dbContext.PaymentMethods.AsNoTracking().Where(e => e.UserId == userId).Select(e => e.Id).ToArray(); 
                
                for (long i = 0; i < listIDUserPayment.Length; i++) {
                    paymentMethods.Add(UpdatePaymentFinancial(listIDUserPayment[i]));
                }

                List<PaymentMethodDTO>? paymentMethodDTOs = _mapper.Map<List<PaymentMethodDTO>>(paymentMethods);

                if (paymentMethodDTOs.Count == 0)
                    return null;

                return paymentMethodDTOs;
            });
        }

        public Task<PaymentMethodDTO?> RetrieveByIdAsync(long id)
            => Task.Run(() => {

                PaymentMethod? paymentMethod = UpdatePaymentFinancial(id);
                
                if(paymentMethod == null)
                    return null;
                
                return _mapper.Map<PaymentMethodDTO>(paymentMethod);
            });

        public Task<List<ExpenseDTO>?> RetrievePaymentExpenseById(long id)
        {
            return Task.Run(() => { 
                
                List<Expense>? expenses = _dbContext.Expenses?.AsNoTracking().Where(e => e.PaymentId == id).ToList();

                if (expenses == null || expenses.Count == 0)
                    return null;

                List<ExpenseDTO> expenseDTOs = _mapper.Map<List<ExpenseDTO>>(expenses);
                return expenseDTOs;
            });
        }

        public Task<List<PrimaryExpenseDTO>?> RetrievePaymentPrimaryExpenseById(long id)
        {
            return Task.Run(() => { 
                
                List<PrimaryExpense>? primaryExpenses = _dbContext.PrimaryExpense?.AsNoTracking().Where(e=>e.PaymentId == id).ToList();
                
                if (primaryExpenses == null || primaryExpenses.Count == 0)
                    return null;

                List<PrimaryExpenseDTO> primaryExpenseDTOs = _mapper.Map<List<PrimaryExpenseDTO>>(primaryExpenses);
                return primaryExpenseDTOs;
            });
        }

        public Task<List<RevenueDTO>?> RetrievePaymentRevenueById(long id)
        {
            return Task.Run(() =>
            {
                List<Revenue>? revenues = _dbContext.Revenues?.AsNoTracking().Where(e => e.PaymentId == id).ToList();

                if (revenues == null || revenues.Count == 0)
                    return null;

                List<RevenueDTO> revenueDTOs = _mapper.Map<List<RevenueDTO>>(revenues);
                return revenueDTOs;
            });
        }

        


        public async Task<PaymentMethodDTO?> UpdateAsync(PaymentMethodDTO paymentMethodDTO)
        {
            if (_dbContext.PaymentMethods == null)
                return null;

            PaymentMethod newEntity = _mapper.Map<PaymentMethod>(paymentMethodDTO);
            
            _dbContext.PaymentMethods.Update(newEntity);
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

        public Task<bool> IsExists(long id) {

            return Task.Run(() =>
            {
                PaymentMethod? existsEntity = _dbContext.PaymentMethods?.AsNoTracking().FirstOrDefault(e => e.Id == id);
                return (existsEntity == null) ? false : true;
            });

        }


        
        private PaymentMethod? UpdatePaymentFinancial(long id) {

            if (_dbContext.PaymentMethods == null)
                return null;

            PaymentMethod? paymentMethod = _dbContext.PaymentMethods
                .Include(e => e.Revenues)
                .Include(e => e.Expenses)
                .Include(e => e.PrimaryExpenses)
                .FirstOrDefault(e => e.Id == id);

            if (paymentMethod == null)
                return null;

            paymentMethod.PaymentRevenue = paymentMethod.Revenues?.Sum(e => e.EarningAmount);
            paymentMethod.PaymentRevenueCount = paymentMethod.Revenues?.Count ?? 0;
            paymentMethod.PaymentExpense = paymentMethod.Expenses?.Sum(e => e.OrderingPrice);
            paymentMethod.PaymentExpenseCount = paymentMethod.Expenses?.Count ?? 0;
            paymentMethod.PaymentBalance = paymentMethod.PaymentRevenue - paymentMethod.PaymentExpense;

            _dbContext.SaveChanges();

            return paymentMethod;
        }

       
    }
}
