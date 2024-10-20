using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WafiArche.Application.Products;
using WafiArche.Application.Products.Dtos;
using WafiArche.Domain.Products;

namespace WafiArche.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ProductController : ControllerBase
    //{
    //    private readonly IProductAppService _productAppService;
    //    private readonly IMapper _mapper;
    //    public ProductController(IProductAppService productAppService, IMapper mapper)
    //    {
    //        _productAppService = productAppService;
    //        _mapper = mapper;
    //    }

    //    [HttpPost]
    //    public ActionResult<ProductDto> CreateProduct([FromBody] ProductCreateDto productCreateDto)
    //    {
    //        Product product = _mapper.Map<Product>(productCreateDto);
    //        var alpha = _productAppService.CreateProduct(product);
    //        return Ok(alpha);
    //    }

    //    [HttpGet]
    //    public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
    //    {
    //        IEnumerable<Product> productList = _productAppService.GetAll();

    //        return Ok(_mapper.Map<List<ProductDto>>(productList));
    //    }
    //}
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
    }

}
