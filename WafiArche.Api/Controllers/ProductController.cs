using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WafiArche.Application.Products;
using WafiArche.Domain.Products;

namespace WafiArche.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            var alpha = _productAppService.CreateProduct(product);

            return Ok(alpha);
        }
    }
}
