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
            
            if (await _productService.IsThereAnyProduct())
            {
                var products = await _productService.GetProducts();
                if(products != null)
                {
                    return Ok(products);
                }
                return BadRequest("Продукты не дошли");
            }
            return BadRequest("Продуктов нет");
        }

        [Route("api/search")]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string search)
        {

            if (search == null)
            {
                return BadRequest("Введите что-нибудь");

            }
            var products = await _productService.SearchProducts(search);
            var masters = await _productService.SearchMasters(search);
            if ((products == null || !products.Any()) && (masters == null || !masters.Any()))
            {
                return NotFound("По вашему запросу ничего не найдено");
            }
            return Ok(new
            {
                products,
                masters
            });


        }

        [Route("api/product")]
        [HttpGet]
        public async Task<IActionResult> ShowProduct([FromQuery] int id)
        {
            var ex = await _productService.DoesProductExsist(id);
            if (!ex)
            {
                return BadRequest("Товар не cуществует");
            }

            var product = await _productService.GetProduct(id);
            if (product == null)
            {
                return BadRequest("Товар не найден");
            }
            return Ok(product);
        }

    }
}
