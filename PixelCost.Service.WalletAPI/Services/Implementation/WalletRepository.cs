using PixelCost.Service.WalletAPI.Database;
using PixelCost.Service.WalletAPI.Model.Entities;
using PixelCost.Service.WalletAPI.Services.Interfaces;
using AutoMapper;
using PixelCost.Service.WalletAPI.Model.DTOs;
using Microsoft.EntityFrameworkCore;

namespace PixelCost.Service.WalletAPI.Services.Implementations
{
    public class WalletRepository : IWalletRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public WalletRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        /// <summary>
        /// Retrieve an entity based on the specify entity's id.
        /// </summary>
        /// <param name="id">id used to retrieve the entity</param>
        /// <returns>An entity if it dose exists, Null if it dosn't exists.</returns>
        public Task<WalletDTO?> RetrieveByIdAsync(string userId)
        {
            return Task.Run(() => { 
                
                Wallet? wallet = _dbContext.Wallets?.AsNoTracking().FirstOrDefault(e => e.UserID == userId);
                if (wallet == null)
                    return null;
                return _mapper.Map<WalletDTO>(wallet);

            });
        }

        public Task<WalletDTO?> RetrieveUpdatedByIdAsync(string userId)
        {
            return Task.Run(() => {

                Wallet? wallet = UpdateFinancialInfo(userId);
                if (wallet == null)
                    return null;
                return _mapper.Map<WalletDTO>(wallet);

            });
        }

        /// <summary>
        /// Create new walletDto entity from the given entity
        /// </summary>
        /// <param name="walletDto">The entity to be created</param>
        /// <returns>WalletDto entity that have been created if it success, null if the entity cannot be created</returns>
        public async Task<WalletDTO?> CreateAsync(WalletDTO walletDto)
        {
            if (_dbContext.Wallets == null)
                return null;

            Wallet wallet = _mapper.Map<Wallet>(walletDto);

            await _dbContext.Wallets.AddAsync(wallet);
            if (await _dbContext.SaveChangesAsync() == 1) 
                return walletDto;

            return null;
        }


        /// <summary>
        /// Update an entity from the given entity
        /// </summary>
        /// <param name="walletDto">The entity to be updated</param>
        /// <returns>WalletDTO entity that have been updated if it success, null if it cannot be updated</returns>
        public async Task<WalletDTO?> UpdateAsync(WalletDTO walletDto)
        {
            if (_dbContext.Wallets == null)
                return null;

            Wallet? wallet = await _dbContext.Wallets.AsNoTracking().FirstOrDefaultAsync(e => e.UserID == walletDto.UserID);
            if (wallet == null)
                return null;

            walletDto.DateCreate = wallet.DateCreate;

            Wallet updated = _mapper.Map<Wallet>(walletDto);

            _dbContext.Wallets.Update(updated);
            if (await _dbContext.SaveChangesAsync() == 1)
                return walletDto;

            return null;

        }


        /// <summary>
        /// Delete the entity using id and claimId and return the result indicate it is success or not.
        /// </summary>
        /// <param name="id">The id used to delete the entity.</param>
        /// <param name="userId">The userId used to scope searching the entity.</param>
        /// <returns>True if it success, False if it not success.</returns>
        public async Task<bool> DeleteAsync(string userId)
        {
            if (_dbContext.Wallets == null)
                return false;

            Wallet? wallet = await _dbContext.Wallets.FirstOrDefaultAsync(e => e.UserID == userId);
            
            if (wallet == null)
                return false;
            
            _dbContext.Wallets.Remove(wallet);

            if(await _dbContext.SaveChangesAsync() == 1)
                return true;
            return false;


        }


        private Wallet? UpdateFinancialInfo(string userId) {

            Wallet? wallet = _dbContext.Wallets?.Include(e => e.PaymentMethods).FirstOrDefault(e => e.UserID == userId);

            if (wallet == null)
                return null;

            wallet.TotalExpense = wallet.PaymentMethods?.Sum(e => e.PaymentExpense);
            wallet.TotalNumberExpense = wallet.PaymentMethods?.Sum(e => e.PaymentExpenseCount) ?? 0;
            wallet.TotalRevenue = wallet.PaymentMethods?.Sum(e => e.PaymentRevenue);
            wallet.TotalNumberRevenue = wallet.PaymentMethods?.Sum(e => e.PaymentRevenueCount) ?? 0;
            wallet.Balance = wallet.TotalRevenue - wallet.TotalExpense;

            _dbContext.Update(wallet);
            _dbContext.SaveChanges();
            

            return wallet;
        }


    }
}
