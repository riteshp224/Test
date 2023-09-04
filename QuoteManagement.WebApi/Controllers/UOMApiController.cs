using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Common;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
using QuoteManagement.Service.Services.UOM;
using QuoteManagement.WebApi.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/UOM")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UOMApiController : ControllerBase
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private IUOMService _UOMService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        private readonly DataConfig _dataConfig;
        private readonly ApplicationSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public UOMApiController(
            ILoggerManager logger,
            IUOMService UOMService,
            IConfiguration config,
            IOptions<CommonMessages> commonMessages,
            IOptions<DataConfig> dataConfig,
            IOptions<ApplicationSettings> appSettings,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _UOMService = UOMService;
            _config = config;
            _commonMessages = commonMessages.Value;
            _dataConfig = dataConfig.Value;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Get
        [HttpGet("GetUOMList")]
        public async Task<ApiResponse<UOMMasterModel>> GetUOMList()
        {
            ApiResponse<UOMMasterModel> response = new ApiResponse<UOMMasterModel>() { Data = new List<UOMMasterModel>() };
            try
            {
                var data = await _UOMService.GetUOMList();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetUOMList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpGet("GetUOMById/{UOMId}")]
        public async Task<ApiPostResponse<UOMMasterModel>> GetUOMById(long UOMId)
        {
            ApiPostResponse<UOMMasterModel> response = new ApiPostResponse<UOMMasterModel>() { Data = new UOMMasterModel() };
            try
            {
                var data = await _UOMService.GetUOMById(UOMId);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetUOMById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Post
        [HttpPost("SaveUOM")]
        public async Task<BaseApiResponse> SaveUOM([FromForm] UOMMasterModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _UOMService.SaveUOMData(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.UOM.SaveSuccess;
                    response.Success = true;
                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.UOM.AlreadyExists;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.UOM.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("SaveUOM", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteUOM")]
        public async Task<BaseApiResponse> DeleteUOM(CommonIdModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _UOMService.DeleteUOM(model);
                if (result)
                {
                    response.Message = _commonMessages.UOM.DeleteSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = _commonMessages.UOM.DeleteError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("DeleteUOM", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion
    }
}
