using AutoMapper;
using PixelCost.Service.DurationAPI.Database;
using PixelCost.Service.DurationAPI.Models.DTOs;
using PixelCost.Service.DurationAPI.Models.Entities;
using PixelCost.Service.DurationAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PixelCost.Service.DurationAPI.Services.Implementations
{
    public class DurationRepository : IDurationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DurationRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<DurationDTO?> CreateAsync(DurationDTO durationDTO)
        {
            return Task.Run(() => { 
                Duration entity = _mapper.Map<Duration>(durationDTO);
                Duration? createdEntity = _dbContext.Durations?.Add(entity).Entity;
                int result = _dbContext.SaveChanges();
                return (createdEntity != null && result == 1)? _mapper.Map<DurationDTO>(createdEntity) : null;
            });
        }
        public Task<DurationDTO?> RetrieveByIdAsync(long id)
        {
            return Task.Run(() => { 
                Duration? duration = _dbContext.Durations?.AsNoTracking().FirstOrDefault(e => e.Id == id);
                return (duration != null)? _mapper.Map<DurationDTO>(duration):null;
            });
        }

        public Task<IList<DurationDTO>?> RetrieveByUserIdAsync(string userId)
        {
            return Task.Run(() => {
                IList<Duration>? durations = _dbContext.Durations?.AsNoTracking().Where(e => e.UserId == userId).ToList();
                return (durations != null && durations?.Count != 0) ? _mapper.Map<IList<DurationDTO>?>(durations) : null;
            });
        }
       
        public Task<DurationDTO?> UpdateAsync(DurationDTO durationDTO)
        {
            return Task.Run(() => { 
                Duration entity = _mapper.Map<Duration>(durationDTO);
                Duration? updatedEntity = _dbContext.Durations?.Update(entity).Entity;
                int result = _dbContext.SaveChanges();
                return (result == 1 && updatedEntity != null)? _mapper.Map<DurationDTO>(updatedEntity) : null;
            });
        }

        public Task<bool> DeleteAsync(long id)
        {
            return Task.Run(() => { 
            
                if (_dbContext.Durations == null)
                    return false;

                Duration deletedEntity = _dbContext.Durations.First(e => e.Id == id);
                _dbContext.Durations?.Remove(deletedEntity);
                int result = _dbContext.SaveChanges(); 
                
                return result == 1;
            });

        }

        public Task UpdateEntity(long id) {
            return Task.Run(() => { });
        }
    }
}
