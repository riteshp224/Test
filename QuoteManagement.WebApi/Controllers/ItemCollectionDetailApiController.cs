using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Common;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
using QuoteManagement.Service.Services.ItemCollection;
using QuoteManagement.WebApi.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/ItemCollectionDetail")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemCollectionDetailApiController : ControllerBase
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private IItemCollectionDetailService _ItemCollectionDetailService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        private readonly DataConfig _dataConfig;
        private readonly ApplicationSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public ItemCollectionDetailApiController(
            ILoggerManager logger,
            IItemCollectionDetailService ItemCollectionDetailService,
            IConfiguration config,
            IOptions<CommonMessages> commonMessages,
            IOptions<DataConfig> dataConfig,
            IOptions<ApplicationSettings> appSettings,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _ItemCollectionDetailService = ItemCollectionDetailService;
            _config = config;
            _commonMessages = commonMessages.Value;
            _dataConfig = dataConfig.Value;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Get
        [HttpGet("GetItemCollectionDetailList/{ItemCollectionId}")]
        public async Task<ApiResponse<ItemCollectionDetailModel>> GetItemCollectionDetailList(long ItemCollectionId)
        {
            ApiResponse<ItemCollectionDetailModel> response = new ApiResponse<ItemCollectionDetailModel>() { Data = new List<ItemCollectionDetailModel>() };
            try
            {
                var data = await _ItemCollectionDetailService.GetItemCollectionDetailList(ItemCollectionId);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetItemCollectionDetailList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpGet("GetItemCollectionDetailById/{ItemCollectionDetailId}")]
        public async Task<ApiPostResponse<ItemCollectionDetailModel>> GetItemCollectionDetailById(long ItemCollectionDetailId)
        {
            ApiPostResponse<ItemCollectionDetailModel> response = new ApiPostResponse<ItemCollectionDetailModel>() { Data = new ItemCollectionDetailModel() };
            try
            {
                var data = await _ItemCollectionDetailService.GetItemCollectionDetailById(ItemCollectionDetailId);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetItemCollectionDetailById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Post
        [HttpPost("SaveItemCollectionDetail")]
        async Task<BaseApiResponse> SaveItemCollectionDetail([FromBody] ItemCollectionDetailModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _ItemCollectionDetailService.SaveItemCollectionDetailData(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.ItemCollectionDetail.SaveSuccess;
                    response.Success = true;
                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.ItemCollectionDetail.AlreadyExists;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.ItemCollectionDetail.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("SaveItemCollectionDetail", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteItemCollectionDetail")]
        public async Task<BaseApiResponse> DeleteItemCollectionDetail(CommonIdModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _ItemCollectionDetailService.DeleteItemCollectionDetail(model);
                if (result)
                {
                    response.Message = _commonMessages.ItemCollectionDetail.DeleteSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = _commonMessages.ItemCollectionDetail.DeleteError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("DeleteItemCollectionDetail", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion
    }
}
