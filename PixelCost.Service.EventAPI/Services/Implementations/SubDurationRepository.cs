using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PixelCost.Service.EventAPI.Database;
using PixelCost.Service.EventAPI.Models.DTOs;
using PixelCost.Service.EventAPI.Models.Entities;
using PixelCost.Service.EventAPI.Services.Interfaces;

namespace PixelCost.Service.EventAPI.Services.Implementations
{
    public class SubDurationRepository : ISubDurationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SubDurationRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public Task<SubDurationDTO?> CreateAsync(SubDurationDTO subDurationDTO)
        {
            return Task.Run(() => {
                if (_dbContext.SubDurations == null)
                    return null;

                SubDuration subDuration = _mapper.Map<SubDuration>(subDurationDTO);
                SubDuration created = _dbContext.SubDurations.Add(subDuration).Entity;
                int result = _dbContext.SaveChanges();
                return (result >= 1 && created != null) ? _mapper.Map<SubDurationDTO>(created) : null;
            });
        }

        public Task<bool> DeleteAsync(long id)
        {
            return Task.Run(() => {

                if (_dbContext.SubDurations == null)
                    return false;
                
                SubDuration deletedEntity = _dbContext.SubDurations.First(e=>e.Id == id);
                _dbContext.SubDurations.Remove(deletedEntity);
                int result = _dbContext.SaveChanges();
                return (result == 1) ? true : false;
            });
        }

        public Task<SubDurationDTO?> RetrieveByIdAsync(long id)
        {
            return Task.Run(() => {
                SubDuration? subDuration = _dbContext.SubDurations.AsNoTracking().FirstOrDefault(e => e.Id == id);
                return (subDuration != null) ? _mapper.Map<SubDurationDTO>(subDuration) : null;
            });

        }

        public Task<IList<SubDurationDTO>?> RetrieveByUserIdAsync(string userId)
        {
            return Task.Run(() => {
                IList<SubDuration> subDurations = _dbContext.SubDurations.AsNoTracking().Where(e => e.userId == userId).ToList();
                return (subDurations != null && subDurations.Count != 0) ? _mapper.Map<IList<SubDurationDTO>>(subDurations) : null;
            });
        }

        public Task<SubDurationDTO?> UpdateAsync(SubDurationDTO subDurationDTO)
        {
            return Task.Run(() => {
                SubDuration entity = _mapper.Map<SubDuration>(subDurationDTO);
                SubDuration updatedEnttiy = _dbContext.SubDurations.Update(entity).Entity;
                int result = _dbContext.SaveChanges();
                return (result == 1) ? _mapper.Map<SubDurationDTO>(updatedEnttiy) : null;
            });
        }
    }
}
