using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Common;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
using QuoteManagement.Service.Services.Quote;
using QuoteManagement.WebApi.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/QuoteDetail")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuoteDetailApiController : ControllerBase
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private IQuoteDetailService _QuoteDetailService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        private readonly DataConfig _dataConfig;
        private readonly ApplicationSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public QuoteDetailApiController(
            ILoggerManager logger,
            IQuoteDetailService QuoteDetailService,
            IConfiguration config,
            IOptions<CommonMessages> commonMessages,
            IOptions<DataConfig> dataConfig,
            IOptions<ApplicationSettings> appSettings,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _QuoteDetailService = QuoteDetailService;
            _config = config;
            _commonMessages = commonMessages.Value;
            _dataConfig = dataConfig.Value;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Get
        [HttpPost("GetQuoteDetailList")]
        public async Task<ApiResponse<QuoteDetailModel>> GetQuoteDetailList(CommonPaginationModel model)
        {
            ApiResponse<QuoteDetailModel> response = new ApiResponse<QuoteDetailModel>() { Data = new List<QuoteDetailModel>() };
            try
            {
                var data = await _QuoteDetailService.GetQuoteDetailList(model);
              
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteDetailList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpPost("GetQuotecustomerDetailList")]
        public async Task<ApiResponse<QuoteDetailModel>> GetQuotecustomerDetailList(CommonPaginationModel model)
        {
            ApiResponse<QuoteDetailModel> response = new ApiResponse<QuoteDetailModel>() { Data = new List<QuoteDetailModel>() };
            try
            {
                var data = await _QuoteDetailService.GetQuotecustomerDetailList(model);

                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuotecustomerDetailList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpPost("CloneQuoteDetailList")]
        public async Task<ApiResponse<QuoteDetailModel>> CloneQuoteDetailList(CommonPaginationModel model)
        {
            ApiResponse<QuoteDetailModel> response = new ApiResponse<QuoteDetailModel>() { Data = new List<QuoteDetailModel>() };
            try
            {
                var data = await _QuoteDetailService.CloneQuoteDetailList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("CloneQuoteDetailList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("GetItemCollectionDetailList")]
        public async Task<ApiResponse<QuoteDetailModel>> GetItemCollectionDetailList(CommonPaginationModel model)
        {
            ApiResponse<QuoteDetailModel> response = new ApiResponse<QuoteDetailModel>() { Data = new List<QuoteDetailModel>() };
            try
            {
                var data = await _QuoteDetailService.GetItemCollectionDetailList(model);
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

        [HttpGet("GetQuoteDetailById/{QuoteDetailId}")]
        public async Task<ApiPostResponse<QuoteDetailModel>> GetQuoteDetailById(long QuoteDetailId)
        {
            ApiPostResponse<QuoteDetailModel> response = new ApiPostResponse<QuoteDetailModel>() { Data = new QuoteDetailModel() };
            try
            {
                var data = await _QuoteDetailService.GetQuoteDetailById(QuoteDetailId);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteDetailById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        
        #endregion

        #region Post
        [HttpPost("SaveQuoteDetail")]
         async Task<BaseApiResponse> SaveQuoteDetail([FromBody] QuoteDetailModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _QuoteDetailService.SaveQuoteDetailData(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.QuoteDetail.SaveSuccess;
                    response.Success = true;
                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.QuoteDetail.AlreadyExists;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.QuoteDetail.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("SaveQuoteDetail", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteQuoteDetail")]
        public async Task<BaseApiResponse> DeleteQuoteDetail(CommonIdModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _QuoteDetailService.DeleteQuoteDetail(model);
                if (result)
                {
                    response.Message = _commonMessages.QuoteDetail.DeleteSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = _commonMessages.QuoteDetail.DeleteError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("DeleteQuoteDetail", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region QuoteVersioning Page
        [HttpPost("GetQuoteVersionList")]
        public async Task<ApiResponse<MultiitemCollectionModel>> GetQuoteVersionList(CommonPaginationModel model)
        {
            ApiResponse<MultiitemCollectionModel> response = new ApiResponse<MultiitemCollectionModel>() { Data = new List<MultiitemCollectionModel>() };
            try
            {
                var data = await _QuoteDetailService.GetQuoteVersionList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteVersionList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("GetQuoteItemDetailList")]
        public async Task<ApiResponse<ItemDetailModel>> GetQuoteItemDetailList(CommonPaginationModel model)
        {
            ApiResponse<ItemDetailModel> response = new ApiResponse<ItemDetailModel>() { Data = new List<ItemDetailModel>() };
            try
            {
                var data = await _QuoteDetailService.GetQuoteItemDetailList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteItemDetailList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        //Task<List<ItemCollectionModel>> GetQuoteCollectionDetailList(CommonPaginationModel model);
        [HttpPost("GetQuoteCollectionDetailList")]
        public async Task<ApiResponse<ItemCollectionModel>> GetQuoteCollectionDetailList(CommonPaginationModel model)
        {
            ApiResponse<ItemCollectionModel> response = new ApiResponse<ItemCollectionModel>() { Data = new List<ItemCollectionModel>() };
            try
            {
                var data = await _QuoteDetailService.GetQuoteCollectionDetailList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteCollectionDetailList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpPost("GetQuoteAdditionalInfoList")]
        public async Task<ApiResponse<AdditionalInfoModel>> GetQuoteAdditionalInfoList(CommonPaginationModel model)
        {
            ApiResponse<AdditionalInfoModel> response = new ApiResponse<AdditionalInfoModel>() { Data = new List<AdditionalInfoModel>() };
            try
            {
                var data = await _QuoteDetailService.GetQuoteAdditionalInfoList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteAdditionalInfoList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region QuoteCloneVersioning Page
        [HttpPost("GetCloneQuoteVersionList")]
        public async Task<ApiResponse<MultiitemCollectionModel>> GetCloneQuoteVersionList(CommonPaginationModel model)
        {
            ApiResponse<MultiitemCollectionModel> response = new ApiResponse<MultiitemCollectionModel>() { Data = new List<MultiitemCollectionModel>() };
            try
            {
                var data = await _QuoteDetailService.GetCloneQuoteVersionList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetCloneQuoteVersionList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("GetCloneQuoteItemDetailList")]
        public async Task<ApiResponse<ItemDetailModel>> GetCloneQuoteItemDetailList(CommonPaginationModel model)
        {
            ApiResponse<ItemDetailModel> response = new ApiResponse<ItemDetailModel>() { Data = new List<ItemDetailModel>() };
            try
            {
                var data = await _QuoteDetailService.GetCloneQuoteItemDetailList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetCloneQuoteItemDetailList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        //Task<List<ItemCollectionModel>> GetQuoteCollectionDetailList(CommonPaginationModel model);
        [HttpPost("GetCloneQuoteCollectionDetailList")]
        public async Task<ApiResponse<ItemCollectionModel>> GetCloneQuoteCollectionDetailList(CommonPaginationModel model)
        {
            ApiResponse<ItemCollectionModel> response = new ApiResponse<ItemCollectionModel>() { Data = new List<ItemCollectionModel>() };
            try
            {
                var data = await _QuoteDetailService.GetCloneQuoteCollectionDetailList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetCloneQuoteCollectionDetailList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpPost("GetCloneQuoteAdditionalInfoList")]
        public async Task<ApiResponse<AdditionalInfoModel>> GetCloneQuoteAdditionalInfoList(CommonPaginationModel model)
        {
            ApiResponse<AdditionalInfoModel> response = new ApiResponse<AdditionalInfoModel>() { Data = new List<AdditionalInfoModel>() };
            try
            {
                var data = await _QuoteDetailService.GetCloneQuoteAdditionalInfoList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetCloneQuoteAdditionalInfoList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion
    }
}
