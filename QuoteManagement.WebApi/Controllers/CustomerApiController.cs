using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Common;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
using QuoteManagement.Service.Services.Customer;
using QuoteManagement.WebApi.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerApiController : ControllerBase
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private ICustomerService _CustomerService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        private readonly DataConfig _dataConfig;
        private readonly ApplicationSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public CustomerApiController(
            ILoggerManager logger,
            ICustomerService CustomerService,
            IConfiguration config,
            IOptions<CommonMessages> commonMessages,
            IOptions<DataConfig> dataConfig,
            IOptions<ApplicationSettings> appSettings,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _CustomerService = CustomerService;
            _config = config;
            _commonMessages = commonMessages.Value;
            _dataConfig = dataConfig.Value;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Get
        [HttpPost("GetCustomerList")]
        public async Task<ApiResponse<CustomerMasterModel>> GetCustomerList(CommonPaginationModel model)
        {
            ApiResponse<CustomerMasterModel> response = new ApiResponse<CustomerMasterModel>() { Data = new List<CustomerMasterModel>() };
            try
            {
                var data = await _CustomerService.GetCustomerList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetCustomerList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpGet("GetCustomerById/{CustomerId}")]
        public async Task<ApiPostResponse<CustomerMasterModel>> GetCustomerById(long CustomerId)
        {
            ApiPostResponse<CustomerMasterModel> response = new ApiPostResponse<CustomerMasterModel>() { Data = new CustomerMasterModel() };
            try
            {
                var data = await _CustomerService.GetCustomerById(CustomerId);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetCustomerById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Post
        [HttpPost("SaveCustomer")]
        public async Task<BaseApiResponse> SaveCustomer([FromForm] CustomerMasterModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _CustomerService.SaveCustomerData(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.Customer.SaveSuccess;
                    response.Success = true;
                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.Customer.AlreadyExists;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.Customer.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("SaveCustomer", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteCustomer")]
        public async Task<BaseApiResponse> DeleteCustomer(CommonIdModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _CustomerService.DeleteCustomer(model);
                if (result)
                {
                    response.Message = _commonMessages.Customer.DeleteSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = _commonMessages.Customer.DeleteError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("DeleteCustomer", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion
    }
}
