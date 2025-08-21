using marketplaceE.DTOs;
using Microsoft.AspNetCore.Mvc;
using marketplaceE.Services;
namespace marketplaceE.Controlllers
{
    [ApiController]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("api/products")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowProducts>>> ShowProducts()
        {
            
            if (await _productService.IsThereAny())
            {
                var products = _productService.GetProducts();
                if(products != null)
                {
                    return Ok(products);
                }
                return BadRequest("Продукты не дошли");
            }
            return BadRequest("Продуктов нет");
        }

    }
}
