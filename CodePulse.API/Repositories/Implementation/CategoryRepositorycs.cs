using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepositorycs: ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepositorycs(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.ID ==category.ID);
            if (existingCategory == null) return null;
           
            _dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
            await _dbContext.SaveChangesAsync();          

            return category;
        }
    }
}
