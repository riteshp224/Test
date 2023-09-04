using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Common;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
using QuoteManagement.Service.Services.Item;
using QuoteManagement.WebApi.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/Item")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemApiController : ControllerBase
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private IItemService _itemService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        private readonly DataConfig _dataConfig;
        private readonly ApplicationSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public ItemApiController(
            ILoggerManager logger,
            IItemService itemService,
            IConfiguration config,
            IOptions<CommonMessages> commonMessages,
            IOptions<DataConfig> dataConfig,
            IOptions<ApplicationSettings> appSettings,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _itemService = itemService;
            _config = config;
            _commonMessages = commonMessages.Value;
            _dataConfig = dataConfig.Value;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Get
        [HttpGet("GetItemListCollection")]
        public async Task<ApiResponse<ItemMasterModel>> GetItemListCollection( )
        {
            ApiResponse<ItemMasterModel> response = new ApiResponse<ItemMasterModel>() { Data = new List<ItemMasterModel>() };
            try
            {
                var data = await _itemService.GetItemListCollection();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetItemListCollection", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpGet("GetItemList")]
        public async Task<ApiResponse<ItemMasterModel>> GetItemList()
        {
            ApiResponse<ItemMasterModel> response = new ApiResponse<ItemMasterModel>() { Data = new List<ItemMasterModel>() };
            try
            {
                var data = await _itemService.GetItemList();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetItemList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpGet("GetItemById/{ItemId}")]
        public async Task<ApiPostResponse<ItemMasterModel>> GetItemById(long ItemId)
        {
            ApiPostResponse<ItemMasterModel> response = new ApiPostResponse<ItemMasterModel>() { Data = new ItemMasterModel() };
            try
            {
                var data = await _itemService.GetItemById(ItemId);
                response.Data = data;
                if (response.Data != null)
                {
                    response.Data.imageURL = !string.IsNullOrEmpty(data.ItemPhoto) ? _dataConfig.FilePath + "Items/" + data.ItemPhoto : null;
                }
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetItemById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Post
        [HttpPost("SaveItem")]
     
        public async Task<BaseApiResponse> SaveItem([FromForm] ItemMasterModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            try
            {


                if (model.ItemPhotoFile != null)
                {
                    Guid guidFile = Guid.NewGuid();
                    var FileName = guidFile + Path.GetExtension(model.ItemPhotoFile.FileName);
                    var BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/Items");

                    if (!Directory.Exists(BasePath))
                    {
                        Directory.CreateDirectory(BasePath);
                    }
                    var path = Path.Combine(BasePath, FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ItemPhotoFile.CopyToAsync(stream);
                    }
                    model.ItemPhoto = FileName;
                }

                var result = await _itemService.SaveItemData(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.Item.SaveSuccess;
                    response.Success = true;
                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.Item.AlreadyExists;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.Item.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("SaveItem", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteItem")]
        public async Task<BaseApiResponse> DeleteItem(CommonIdModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _itemService.DeleteItem(model);
                if (result)
                {
                    response.Message = _commonMessages.Item.DeleteSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = _commonMessages.Item.DeleteError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("DeleteItem", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion
    }
}
