using PixelCost.Service.CategoryAPI.Models.DTOs;
using PixelCost.Service.CategoryAPI.Models.Entities;

namespace PixelCost.Service.CategoryAPI.Services.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryDTO?> RetrieveByIdAsync(long id);
        Task<CategoryDTO?> CreateAsync(CategoryDTO categoryDTO);
        Task<CategoryDTO?> UpdateAsync(CategoryDTO categoryDTO);
        Task<bool> DeleteAsync(long id);
    }
}
