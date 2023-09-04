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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/ItemCollection")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemCollectionApiController : ControllerBase
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private IItemCollectionService _ItemCollectionService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        private readonly DataConfig _dataConfig;
        private readonly ApplicationSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public ItemCollectionApiController(
            ILoggerManager logger,
            IItemCollectionService ItemCollectionService,
            IConfiguration config,
            IOptions<CommonMessages> commonMessages,
            IOptions<DataConfig> dataConfig,
            IOptions<ApplicationSettings> appSettings,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _ItemCollectionService = ItemCollectionService;
            _config = config;
            _commonMessages = commonMessages.Value;
            _dataConfig = dataConfig.Value;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Get
        [HttpGet("GetItemCollectionList")]
        public async Task<ApiResponse<ItemCollectionMasterModel>> GetItemCollectionList()
        {
            ApiResponse<ItemCollectionMasterModel> response = new ApiResponse<ItemCollectionMasterModel>() { Data = new List<ItemCollectionMasterModel>() };
            try
            {
                var data = await _ItemCollectionService.GetItemCollectionList();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetItemCollectionList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpGet("GetItemCollectionById/{ItemCollectionId}")]
        public async Task<ApiPostResponse<ItemCollectionMasterModel>> GetItemCollectionById(long ItemCollectionId)
        {
            ApiPostResponse<ItemCollectionMasterModel> response = new ApiPostResponse<ItemCollectionMasterModel>() { Data = new ItemCollectionMasterModel() };
            try
            {
               
                var data = await _ItemCollectionService.GetItemCollectionById(ItemCollectionId);
                response.Data = data;
                if (response.Data != null)
                {
                    response.Data.imageURL = !string.IsNullOrEmpty(data.ItemPhoto) ? _dataConfig.FilePath + "ItemCollection/" + data.ItemPhoto : null;

                }
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetItemCollectionById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Post
        [HttpPost("SaveItemCollection")]
        public async Task<BaseApiResponse> SaveItemCollection([FromForm] ItemCollectionMasterModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            try
            {
                if (model.ItemPhotoFile != null)
                {
                    Guid guidFile = Guid.NewGuid();
                    var FileName = guidFile + Path.GetExtension(model.ItemPhotoFile.FileName);
                    var BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/ItemCollection");

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
                var result = await _ItemCollectionService.SaveItemCollectionData(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.ItemCollection.SaveSuccess;
                    response.Success = true;
                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.ItemCollection.AlreadyExists;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.ItemCollection.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("SaveItemCollection", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteItemCollection")]
        public async Task<BaseApiResponse> DeleteItemCollection(CommonIdModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _ItemCollectionService.DeleteItemCollection(model);
                if (result)
                {
                    response.Message = _commonMessages.ItemCollection.DeleteSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = _commonMessages.ItemCollection.DeleteError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("DeleteItemCollection", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion
    }
}
