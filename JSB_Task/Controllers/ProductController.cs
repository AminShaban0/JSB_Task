using AutoMapper;
using JSB_Task.Dtos;
using JSB_Task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JSB_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenaricRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenaricRepository<Product> productRepo , IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> PostProduct([FromQuery]ProductDto Dto)
        {
            var productMap = _mapper.Map<Product>(Dto);
            await _productRepo.AddAsync(productMap);
            return Ok("Product_Successfly");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepo.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
                return NotFound("Product not found.");

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductDto dto)
        {
            if (dto == null || id <= 0)
                return BadRequest("Invalid data.");

            var productEntity = await _productRepo.GetByIdAsync(id);
            if (productEntity == null)
                return NotFound("Product not found.");

            _mapper.Map(dto, productEntity);
            _productRepo.Update(productEntity);
            return Ok("Product successfully updated.");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productEntity = await _productRepo.GetByIdAsync(id);
            if (productEntity == null)
                return NotFound("Product not found.");

            _productRepo.Delete(productEntity);
            return Ok("Product successfully deleted.");
        }

    }
}
