using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WafiArche.Application.Products;
using WafiArche.Application.Products.Dtos;
using WafiArche.Domain.Products;

namespace WafiArche.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductAppService productAppService, IMapper mapper, ILogger<ProductController> logger)
        {
            _productAppService = productAppService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<ProductDto> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            _logger.LogInformation("Received request to create a product");

            Product product = _mapper.Map<Product>(productCreateDto);
            var alpha = _productAppService.CreateProduct(product);

            _logger.LogInformation("Product created successfully with ID: {ProductId}", alpha.Id);

            return Ok(alpha);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            _logger.LogInformation("Received request to fetch all products");

            IEnumerable<Product> productList = _productAppService.GetAll();

            _logger.LogInformation("Fetched {ProductCount} products", productList.Count());

            return Ok(_mapper.Map<List<ProductDto>>(productList));
        }

        [HttpGet("Id : int", Name = "GetPoductById")]
        public ActionResult<ProductDto> GetPoductById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = _productAppService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPut("Id : int", Name = "UpdateProduct")]

        public IActionResult UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            if (id != productUpdateDto.Id || productUpdateDto == null)
            {
                return BadRequest();
            }
            Product model = _mapper.Map<Product>(productUpdateDto);


            bool result = _productAppService.UpdateProduct(model);

            if (result)
            {
                _logger.LogInformation("Product updated successfully with ID: {ProductId}", model.Id);
            }
            else
            {
                _logger.LogError("Error updating product with ID: {ProductId}", model.Id);
            }

            return NoContent();
        }

        [HttpDelete("Id : int", Name = "DeleteProduct")]

        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = _productAppService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            var result = _productAppService.DeleteProduct(id);
            if (result)
            {
                _logger.LogInformation("Product deleted successfully with ID: {ProductId}", id);
            }
            else
            {
                _logger.LogError("Error deleting product with ID: {ProductId}", id);
            }

            return NoContent();
        }
    }

}
