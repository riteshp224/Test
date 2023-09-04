using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Common;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
using QuoteManagement.Service.Services.DashBoard;
using QuoteManagement.WebApi.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/DashBoard")]
    [ApiController]
  //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DashBoardApiController : Controller
    {
        private readonly ILoggerManager _logger;
        private IDashBoardService _DashBoardService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        private readonly DataConfig _dataConfig;
        private readonly ApplicationSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #region Constructor
        public DashBoardApiController(
         ILoggerManager logger,
         IDashBoardService dashBoardService,
         IConfiguration config,
         IOptions<CommonMessages> commonMessages,
         IOptions<DataConfig> dataConfig,
         IOptions<ApplicationSettings> appSettings,
         IHttpContextAccessor httpContextAccessor
         )
        {
            _logger = logger;
            _DashBoardService =dashBoardService;
            _config = config;
            _commonMessages = commonMessages.Value;
            _dataConfig = dataConfig.Value;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion


        #region Get
        [HttpGet("GetTotalCount")]
        public async Task<ApiPostResponse<DashBoardModel>> GetTotalCount()
        {
            ApiPostResponse<DashBoardModel> response = new ApiPostResponse<DashBoardModel>() { Data = new DashBoardModel() };
            try
            {
                var data = await _DashBoardService.GetTotalCount();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
              
                response.Message = "Something went wrong on dashbord GetTotalCount";
            }
            return response;
        }

        [HttpGet("GetComparsionChartData")]
        public async Task<ApiResponse<Comparsionchart>> GetComparsionChartData()
        {
            ApiResponse<Comparsionchart> response = new ApiResponse<Comparsionchart>() { Data = new List<Comparsionchart>() };
            try
            {
                var data = await _DashBoardService.GetComparsionChartData();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.Message = "Something went wrong on dashbord GetComparsionChartaData";
            }
            return response;
        }
        [HttpGet("GetPopularList")]
        public async Task<ApiResponse<ItemMasterModel>> GetPopularList()
        {
            ApiResponse<ItemMasterModel> response = new ApiResponse<ItemMasterModel>() { Data = new List<ItemMasterModel>() };
            try
            {
                var data = await _DashBoardService.GetPopularItemData();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.Message = "Something went wrong on dashbord GetPopularItemData";
            }
            return response;
        }

        [HttpGet("GetOngoingProject")]
        public async Task<ApiResponse<QuoteModel>> GetOngoingProject()
        {
            ApiResponse<QuoteModel> response = new ApiResponse<QuoteModel>() { Data = new List<QuoteModel>() };
            try
            {
                var data = await _DashBoardService.GetOngoingProject();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.Message = "Something went wrong on dashbord GetPopularItemData";
            }
            return response;
        }
        [HttpGet("GetOngoingQuotes")]
        public async Task<ApiResponse<QuoteModel>> GetOngoingQuotes()
        {
            ApiResponse<QuoteModel> response = new ApiResponse<QuoteModel>() { Data = new List<QuoteModel>() };
            try
            {
                var data = await _DashBoardService.GetOngoingQuotes();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.Message = "Something went wrong on dashbord GetPopularItemData";
            }
            return response;
        }

        [HttpPost("GetQuoteData")]
        public async Task<ApiResponse<QuoteDataModel>> GetQuoteData(QuoteDataModel quoteDataModel)
        {
            ApiResponse<QuoteDataModel> response = new ApiResponse<QuoteDataModel>() { Data = new List<QuoteDataModel>() };
            try
            {
                var data = await _DashBoardService.GetQuoteData(quoteDataModel);
                int i = 0;
                foreach (var quote in data)
                {
                    MonthwiseQuoteDataModel monthwise = new MonthwiseQuoteDataModel();
                    var monthwisequotedetail = await _DashBoardService.GetMonthWiseQuoteData(quote.QuoteId,quoteDataModel.FromDate, quoteDataModel.ToDate);
                    data[i].monthwiseQuoteDataList = monthwisequotedetail;
                    i++;
                }
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.Message = "Something went wrong on dashbord GetPopularItemData";
            }
            return response;
        }
        #endregion

    }
}
