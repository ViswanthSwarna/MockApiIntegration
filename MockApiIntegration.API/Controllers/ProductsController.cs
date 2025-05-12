using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MockApiIntegration.Application.DTOs;
using MockApiIntegration.Application.Services.Interfaces;

namespace MockApiIntegration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IValidator<ProductRequestDto> _productRequestDtoValidator;

        public ProductsController(IProductService service, IValidator<ProductRequestDto> productRequestDtoValidator)
        {
            _service = service;
            _productRequestDtoValidator = productRequestDtoValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto dto)
        {
            var validationResult = await _productRequestDtoValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }    
            var id = await _service.AddProductAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _service.GetProductsAsync(name, page, pageSize);
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
