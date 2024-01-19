using AutoMapper;
using BusinessLogic.Commons.Interfaces;
using BusinessLogic.Commons.Models;
using Infrastructure.ServicesClients.Models;
using Infrastructure.ServicesClients.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.ServicesClients
{
    public class ApiMochaServiceClient : IApiMochaServiceClient
    {
        private const string ProductDiscountApi = "product/{0}/discount";
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<ApiMochaServiceClient> _logger;

        public ApiMochaServiceClient(
            IHttpClientFactory clientFactory,
            IMapper mapper,
            IOptions<ServicesClientsSettings> clientSettings,
            ILogger<ApiMochaServiceClient> logger)
        {
            _client = clientFactory.CreateClient();
            _mapper = mapper;
            _client.BaseAddress = new Uri(clientSettings.Value.ApiMocha);
            _mapper = ConfigureMapping();
            _logger = logger;
        }

        protected static Mapper ConfigureMapping()
        {
            return new(new MapperConfiguration(conf =>
            {
                conf.CreateMap<ResponseProductDiscount, ProductDiscount>();
            }));
        }

        public virtual async Task<ProductDiscount> GetProductDiscount(int productId)
        {
            try
            {
                var endPoint = string.Format(ProductDiscountApi, productId);
                var response = await _client.GetAsync(endPoint);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("{message}", "An error occurred with the ApiMocha service");
                    return null;
                }

                var productDiscount = await response.Content.ReadAsAsync<ResponseProductDiscount>();
                return _mapper.Map<ResponseProductDiscount, ProductDiscount>(productDiscount);
            }
            catch (Exception ex)
            {
                _logger.LogError("{message}", $"An error occurred with the ApiMocha service: {ex.Message}");
                return null;
            }
        }
    }
}
