using BusinessLogic.Features.Products.Commands.CreateProduct;
using BusinessLogic.Features.Products.Commands.UpdateProduct;
using BusinessLogic.Features.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IMediator mediator,
            ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id)
        {
            _logger.LogInformation("{information}", $"Start Time: {DateTime.UtcNow}, Method: {nameof(GetProductByIdAsync)}");
            var response = await _mediator.Send(new GetProductByIdQuery(id));
            _logger.LogInformation("{information}", $"End Time: {DateTime.UtcNow}, Method: {nameof(GetProductByIdAsync)}");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync([FromBody] CreateProductCommand command)
        {
            _logger.LogInformation("{information}", $"Start Time: {DateTime.UtcNow}, Method: {nameof(CreateAsync)}");
            var response = await _mediator.Send(command);
            _logger.LogInformation("{information}", $"End Time: {DateTime.UtcNow}, Method: {nameof(CreateAsync)}");
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateAsync(int id, UpdateProductCommand command)
        {
            _logger.LogInformation("{information}", $"Start Time: {DateTime.UtcNow}, Method: {nameof(UpdateAsync)}");
            command.ProductId = id;
            var response = await _mediator.Send(command);
            _logger.LogInformation("{information}", $"End Time: {DateTime.UtcNow}, Method: {nameof(UpdateAsync)}");
            return Ok(response);
        }
    }
}
