using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using movie.Dto;
using movie.Interfaces;
using movie.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;
        
        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
         
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var categoriesMapper = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoriesMapper);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)
        {
            //if (!_categoryRepository.CategoryExists(categoryId))
            //    return NotFound();

            var category = _categoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody]CategoryDto category)
        {
            var categoryMap = _mapper.Map<Category>(category);
            _categoryRepository.CreateCategory(categoryMap);
            return Ok(category);

        }

        [HttpPut]
        public IActionResult UpdateCategory([FromBody] CategoryDto category)
        {
            var categoryMap = _mapper.Map<Category>(category);
            _categoryRepository.UpdateCategory(categoryMap);
            return Ok(category);

        }

        [HttpDelete]
        public IActionResult DeleteCategory(int categoryId)
        {
            var category = _categoryRepository.GetCategory(categoryId);
            _categoryRepository.DeleteCategory(category);
            return Ok(category);
        }



    }
}
