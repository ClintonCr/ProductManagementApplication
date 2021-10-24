using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.Contract.Models;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductOptionService _productOptionService;

        public ProductsController(IProductService productService, IProductOptionService productOptionService)
        {
            _productService = productService;
            _productOptionService = productOptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] string name)
        {
            Products productEntities;

            if (string.IsNullOrWhiteSpace(name))
                productEntities = await _productService.GetProducts().ConfigureAwait(false);
            else
                productEntities = await _productService.GetProductByName(name).ConfigureAwait(false);

            return Ok(productEntities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByProductId(Guid id)
        {
            Product productEntity = await _productService.GetProductById(id).ConfigureAwait(false);

            return Ok(productEntity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product contract)
        {
            Guid id = await _productService.CreateProduct(contract).ConfigureAwait(false);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product contract)
        {
            await _productService.UpdateProduct(id, contract).ConfigureAwait(false);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProductById(id).ConfigureAwait(false);

            return NoContent();
        }

        [HttpGet("{id}/options")]
        public async Task<IActionResult> GetProductOptionsByProductId(Guid id)
        {
            ProductOptions productOptionEntities = 
                await _productOptionService.GetProductOptionByProductId(id).ConfigureAwait(false);

            return Ok(productOptionEntities);
        }

        [HttpGet("{id}/options/{optionId}")]
        public async Task<IActionResult> GetProductOptionById(Guid id, Guid optionId)
        {
            ProductOption productOptionEntity =
                await _productOptionService.GetProductOptionById(id, optionId).ConfigureAwait(false);

            return Ok(productOptionEntity);
        }

        [HttpPost("{id}/options")]
        public async Task<IActionResult> CreateProductOption(Guid id, [FromBody] ProductOption contract)
        {
            Guid productOptionId = await _productOptionService.CreateProductOption(id, contract).ConfigureAwait(false);

            return Ok(productOptionId);
        }

        [HttpPut("{id}/options/{optionId}")]
        public async Task<IActionResult> UpdateProductOption(Guid id, Guid optionId, [FromBody] ProductOption contract)
        {
            await _productOptionService.UpdateProductOption(id, optionId, contract).ConfigureAwait(false);

            return NoContent();
        }

        [HttpDelete("{id}/options/{optionId}")]
        public async Task<IActionResult> DeleteProductOption(Guid id, Guid optionId)
        {
            await _productOptionService.DeleteProductOption(id, optionId).ConfigureAwait(false);

            return NoContent();
        }
    }
}
