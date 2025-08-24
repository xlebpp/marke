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
                var products = _productService.GetProducts();
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
        public async Task<IActionResult> Search([FromBody] string search)
        {

            if (search == null)
            {
                return BadRequest("Введите что-нибудь");

            }
            var products =await  _productService.SearchProducts(search);
            if ( products is not null)
            {
                return Ok(products);
            }
            var masters = await _productService.SearchMasters(search);
            if (masters is not null)
            {
                return Ok(masters);
            }

            return BadRequest("По вашему запросу ничего не найдено");
        }
    }
}
