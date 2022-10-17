using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PixelCost.Service.CategoryAPI.Database;
using PixelCost.Service.CategoryAPI.Models.DTOs;
using PixelCost.Service.CategoryAPI.Models.Entities;
using PixelCost.Service.CategoryAPI.Services.Interfaces;

namespace PixelCost.Service.CategoryAPI.Services.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CategoryRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<CategoryDTO?> CreateAsync(CategoryDTO categoryDTO)
        {
            return Task.Run(() => { 
                Category category = _mapper.Map<Category>(categoryDTO);
                Category createdEntity = _dbContext.Categories.Add(category).Entity;
                int result = _dbContext.SaveChanges();
                return (result == 1)? _mapper.Map<CategoryDTO>(createdEntity):null;
            });
        }
        public Task<CategoryDTO?> RetrieveByIdAsync(long id)
        {
            return Task.Run(() => {
                Category? category = _dbContext.Categories?.AsNoTracking().FirstOrDefault(e => e.Id == id);
                return (category != null) ? _mapper.Map<CategoryDTO>(category) : null;
            });


        }
        public Task<CategoryDTO?> UpdateAsync(CategoryDTO categoryDTO)
        {
            return Task.Run(() => { 
                Category entity = _mapper.Map<Category>(categoryDTO);
                Category? updatedEntity = _dbContext.Categories?.Update(entity).Entity;
                int result = _dbContext.SaveChanges();
                return (result == 1 && updatedEntity != null) ? _mapper.Map<CategoryDTO>(updatedEntity) : null; 
            });
        }
        public Task<bool> DeleteAsync(long id)
        {
            return Task.Run(() => { 
                Category? entity = _dbContext.Categories?.First(e => e.Id == id);
                if (entity == null)
                    return false;
                _dbContext.Categories?.Remove(entity);
                int result = _dbContext.SaveChanges();
                return (result >= 1) ? true : false;
            });
        }

    }
}
