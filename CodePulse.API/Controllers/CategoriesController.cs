using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {

            this._categoryRepository = categoryRepository;
        }

        // 
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
          
            await _categoryRepository.CreateCategoryAsync(category);

            var response = new CategoryDto
            {
                ID = category.ID,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            // Map domain models to DTOs
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    ID = category.ID,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });

            }

            return Ok(response);
        }

        // GET: api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                ID = category.ID,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
        }

        //PUT: api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id,UpdateCategoryRequestDto request)
        {
            var category = new Category
            {
                ID = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            category = await _categoryRepository.UpdateAsync(category);

            if (category == null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                ID = category.ID,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);    
        }

    }
}
