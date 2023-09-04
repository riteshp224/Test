using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Common;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
using QuoteManagement.Service.Services.ItemCategory;
using QuoteManagement.WebApi.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/ItemCategory")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemCategoryApiController : ControllerBase
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private IItemCategoryService _itemCategoryService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        private readonly DataConfig _dataConfig;
        private readonly ApplicationSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public ItemCategoryApiController(
            ILoggerManager logger,
            IItemCategoryService itemCategoryService,
            IConfiguration config,
            IOptions<CommonMessages> commonMessages,
            IOptions<DataConfig> dataConfig,
            IOptions<ApplicationSettings> appSettings,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _itemCategoryService = itemCategoryService;
            _config = config;
            _commonMessages = commonMessages.Value;
            _dataConfig = dataConfig.Value;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Get
        [HttpGet("GetItemCategoryList")]
        public async Task<ApiResponse<ItemCategoryMasterModel>> GetItemCategoryList()
        {
            ApiResponse<ItemCategoryMasterModel> response = new ApiResponse<ItemCategoryMasterModel>() { Data = new List<ItemCategoryMasterModel>() };
            try
            {
                var data = await _itemCategoryService.GetItemCategoryList();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetItemCategoryList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpGet("GetItemCategoryById/{ItemCategoryId}")]
        public async Task<ApiPostResponse<ItemCategoryMasterModel>> GetItemCategoryById(long ItemCategoryId)
        {
            ApiPostResponse<ItemCategoryMasterModel> response = new ApiPostResponse<ItemCategoryMasterModel>() { Data = new ItemCategoryMasterModel() };
            try
            {
                var data = await _itemCategoryService.GetItemCategoryById(ItemCategoryId);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetItemCategoryById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Post
        [HttpPost("SaveItemCategory")]
        public async Task<BaseApiResponse> SaveItemCategory([FromForm] ItemCategoryMasterModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _itemCategoryService.SaveItemCategoryData(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.ItemCategory.SaveSuccess;
                    response.Success = true;
                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.ItemCategory.AlreadyExists;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.ItemCategory.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("SaveItemCategory", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteItemCategory")]
        public async Task<BaseApiResponse> DeleteItemCategory(CommonIdModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _itemCategoryService.DeleteItemCategory(model);
                if (result)
                {
                    response.Message = _commonMessages.ItemCategory.DeleteSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = _commonMessages.ItemCategory.DeleteError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("DeleteItemCategory", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion
    }
}
