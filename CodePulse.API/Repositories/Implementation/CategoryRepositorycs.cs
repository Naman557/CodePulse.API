using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;

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
    }
}
