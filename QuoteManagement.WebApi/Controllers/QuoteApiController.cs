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
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static QuoteManagement.Common.EmailNotification;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/Quote")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuoteApiController : ControllerBase
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private IQuoteService _QuoteService;
        private IQuoteDetailService _QuoteDetailService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        private readonly DataConfig _dataConfig;
        private readonly ApplicationSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public QuoteApiController(
            ILoggerManager logger,
            IQuoteService QuoteService,
            IQuoteDetailService QuoteDetailService,
            IConfiguration config,
            IOptions<CommonMessages> commonMessages,
            IOptions<DataConfig> dataConfig,
            IOptions<ApplicationSettings> appSettings,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _QuoteService = QuoteService;
            _QuoteDetailService = QuoteDetailService;
            _config = config;
            _commonMessages = commonMessages.Value;
            _dataConfig = dataConfig.Value;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Get
        [HttpPost("GetQuoteList")]
        public async Task<ApiResponse<QuoteModel>> GetQuoteList(CommonPaginationModel model)
        {
            ApiResponse<QuoteModel> response = new ApiResponse<QuoteModel>() { Data = new List<QuoteModel>() };
            try
            {
                var data = await _QuoteService.GetQuoteList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("GetClosedQuoteList")]
        public async Task<ApiResponse<QuoteModel>> GetClosedQuoteList(CommonPaginationModel model)
        {
            ApiResponse<QuoteModel> response = new ApiResponse<QuoteModel>() { Data = new List<QuoteModel>() };
            try
            {
                var data = await _QuoteService.GetClosedQuoteList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("GetCustomerQuoteList")]
        public async Task<ApiResponse<QuoteModel>> GetCustomerQuoteList(CommonPaginationModel model)
        {
            ApiResponse<QuoteModel> response = new ApiResponse<QuoteModel>() { Data = new List<QuoteModel>() };
            try
            {
                var data = await _QuoteService.GetCustomerQuoteList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetCustomerQuoteList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpGet("GetQuoteById/{QuoteId}")]
        public async Task<ApiPostResponse<QuoteModel>> GetQuoteById(long QuoteId)
        {
            ApiPostResponse<QuoteModel> response = new ApiPostResponse<QuoteModel>() { Data = new QuoteModel() };

            try
            {
                var data = await _QuoteService.GetQuoteById(QuoteId);
                CommonPaginationModel model = new CommonPaginationModel();
                model.id = QuoteId;

                var dataQuoteVersion = await _QuoteDetailService.GetQuoteVersionList(model);
                int i = 0;
                foreach (var QuoteVersion in dataQuoteVersion)
                {
                    CommonPaginationModel modelQV = new CommonPaginationModel();
                    modelQV.id = QuoteVersion.QuoteVersionId;
                    var dataQuoteCollectionDetail = await _QuoteDetailService.GetQuoteCollectionDetailList(modelQV);
                    var dataQuoteItemDetail = await _QuoteDetailService.GetQuoteItemDetailList(modelQV);
                    var dataQuoteAdditionalInfo = await _QuoteDetailService.GetQuoteAdditionalInfoList(modelQV);
                  
                    dataQuoteVersion[i].itemCollection = dataQuoteCollectionDetail;
                    dataQuoteVersion[i].itemDetail = dataQuoteItemDetail;
                    dataQuoteVersion[i].additionalInfo = dataQuoteAdditionalInfo;

                    i++;
                }
                data.multiitemCollection = dataQuoteVersion;

                response.Data = data;
                
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpGet("GetQuotecustomerById/{QuoteId}")]
        public async Task<ApiPostResponse<QuoteModel>> GetQuotecustomerById(long QuoteId)
        {
            ApiPostResponse<QuoteModel> response = new ApiPostResponse<QuoteModel>() { Data = new QuoteModel() };

            try
            {
                var data = await _QuoteService.GetQuotecustomerById(QuoteId);
                //CommonPaginationModel model = new CommonPaginationModel();
                //model.id = QuoteId;

                //var dataQuoteVersion = await _QuoteDetailService.GetQuoteVersionList(model);
                //int i = 0;
                //foreach (var QuoteVersion in dataQuoteVersion)
                //{
                //    CommonPaginationModel modelQV = new CommonPaginationModel();
                //    modelQV.id = QuoteVersion.QuoteVersionId;
                //    var dataQuoteCollectionDetail = await _QuoteDetailService.GetQuoteCollectionDetailList(modelQV);
                //    var dataQuoteItemDetail = await _QuoteDetailService.GetQuoteItemDetailList(modelQV);
                //    var dataQuoteAdditionalInfo = await _QuoteDetailService.GetQuoteAdditionalInfoList(modelQV);

                //    dataQuoteVersion[i].itemCollection = dataQuoteCollectionDetail;
                //    dataQuoteVersion[i].itemDetail = dataQuoteItemDetail;
                //    dataQuoteVersion[i].additionalInfo = dataQuoteAdditionalInfo;

                //    i++;
                //}
                //data.multiitemCollection = dataQuoteVersion;

                response.Data = data;

                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpGet("CloneQuote/{QuoteId}")]
        public async Task<ApiPostResponse<QuoteModel>> CloneQuote(long QuoteId)
        {
            ApiPostResponse<QuoteModel> response = new ApiPostResponse<QuoteModel>() { Data = new QuoteModel() };

            try
            {
                var data = await _QuoteService.CloneQuote(QuoteId);
                CommonPaginationModel model = new CommonPaginationModel();
                model.id = QuoteId;

                var dataQuoteVersion = await _QuoteDetailService.GetCloneQuoteVersionList(model);
                int i = 0;
                foreach (var QuoteVersion in dataQuoteVersion)
                {
                    CommonPaginationModel modelQV = new CommonPaginationModel();
                    modelQV.id = QuoteVersion.QuoteVersionId;
                    var dataQuoteCollectionDetail = await _QuoteDetailService.GetCloneQuoteCollectionDetailList(modelQV);
                    var dataQuoteItemDetail = await _QuoteDetailService.GetCloneQuoteItemDetailList(modelQV);
                    var dataQuoteAdditionalInfo = await _QuoteDetailService.GetCloneQuoteAdditionalInfoList(modelQV);

                    dataQuoteVersion[i].itemCollection = dataQuoteCollectionDetail;
                    dataQuoteVersion[i].itemDetail = dataQuoteItemDetail;
                    dataQuoteVersion[i].additionalInfo = dataQuoteAdditionalInfo;
                    dataQuoteVersion[i].QuoteVersionId = 0;

                    i++;
                }
                data.multiitemCollection = dataQuoteVersion;
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("CloneQuote", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("GetCustomerList")]
        public async Task<ApiResponse<QuoteCustomerDetail>>  GetCustomerList()
        {
            ApiResponse<QuoteCustomerDetail> response = new ApiResponse<QuoteCustomerDetail>() { Data = new List<QuoteCustomerDetail>() };
            try
            {
                var data = await _QuoteService.GetCustomerList();
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

        [HttpGet("GetStatusList")]
        public async Task<ApiResponse<StatusDetail>> GetStatusList()
        {
            ApiResponse<StatusDetail> response = new ApiResponse<StatusDetail>() { Data = new List<StatusDetail>() };
            try
            {
                var data = await _QuoteService.GetStatusList();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetStatusList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpGet("getJoinersList")]
        public async Task<ApiResponse<JoinerDetail>> getJoinersList()
        {
            ApiResponse<JoinerDetail> response = new ApiResponse<JoinerDetail>() { Data = new List<JoinerDetail>() };
            try
            {
                var data = await _QuoteService.getJoinersList();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("getJoinersList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpPost("getQuoteVersionList")]
        public async Task<ApiResponse<MultiitemCollectionModel>> getQuoteVersionList(CommonPaginationModel model)
        {
            ApiResponse<MultiitemCollectionModel> response = new ApiResponse<MultiitemCollectionModel>() { Data = new List<MultiitemCollectionModel>() };
            try
            {
                var data = await _QuoteService.getQuoteVersionList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("getQuoteVersionList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpPost("GetItemCollectionList")]
        public async Task<ApiResponse<ItemCollectionMasterModel>> GetItemCollectionList()
        {
            ApiResponse<ItemCollectionMasterModel> response = new ApiResponse<ItemCollectionMasterModel>() { Data = new List<ItemCollectionMasterModel>() };
            try
            {
                var data = await _QuoteService.GetItemCollectionList();
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

        [HttpGet("GetCustomerDetail/{CustomerId}")]
        public async Task<ApiPostResponse<QuoteCustomerDetail>> GetCustomerDetailById(long CustomerId)
        {
            ApiPostResponse<QuoteCustomerDetail> response = new ApiPostResponse<QuoteCustomerDetail>() { Data = new QuoteCustomerDetail() };

            try
            {
                var data = await _QuoteService.GetCustomerDetailById(CustomerId);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetCustomerDetailById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("GetSetting")]
        public async Task<ApiPostResponse<SettingModel>> GetSetting(CommonPaginationModel model)
        {
           ApiPostResponse<SettingModel> response = new ApiPostResponse<SettingModel>() { Data = new SettingModel() };
            try
            {
                var data = await _QuoteService.GetSetting(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Post
        [HttpPost("SaveQuote")]
        public async Task<BaseApiResponse> SaveQuote(QuoteModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            var BasePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");
            string emailBody = "";
            try
            {
                var result = await _QuoteService.SaveQuoteData(model);

                string savesuccess = string.Empty;
                if (!string.IsNullOrEmpty(result))
                {
                    savesuccess = result.Split("|")[0];

                }
                if (string.IsNullOrEmpty(savesuccess))
                {
                    if (result.Split("|").Count() > 1)
                    {
                        int CollectionId = Convert.ToInt32(result.Split("|")[1]);
                        response.TAID = CollectionId.ToString();
                    }
                    response.Message = _commonMessages.Quote.SaveSuccess;
                    response.Success = true;
                    

                    if (model.QuoteStatusId!=null && model.OldQuoteStatusId!= model.QuoteStatusId && model.QuoteStatusId == "2")
                    {
                        var Quote= await _QuoteService.GetQuoteById(model.QuoteId);

                        if (!string.IsNullOrEmpty(Quote.JoinerEmailId))
                        {
                            if (!Directory.Exists(BasePath))
                            {
                                Directory.CreateDirectory(BasePath);
                            }
                            using (StreamReader reader = new StreamReader(Path.Combine(BasePath, "Joiner.html")))
                            {
                                //string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                                //string scheme = _httpContextAccessor.HttpContext.Request.Scheme;
                                //string hosturl = scheme + "://" + host;

                                emailBody = reader.ReadToEnd();
                                emailBody = emailBody.Replace("{{Joiner_Name}}", model.Joiners);
                                emailBody = emailBody.Replace("{{Quote_No}}", model.QuoteNo);
                                emailBody = emailBody.Replace("{{URL}}", _config["Data:APPURL"].ToString());
                                emailBody = emailBody.Replace("{{Customer_Name}}", model.CustomerName);
                            }

                            EmailSetting setting = new EmailSetting
                            {
                                EmailEnableSsl = Convert.ToBoolean(_appSettings.EmailEnableSsl),
                                EmailHostName = _appSettings.EmailHostName,
                                EmailPassword = _appSettings.EmailPassword,
                                EmailAppPassword = _appSettings.EmailAppPassword,
                                EmailPort = Convert.ToInt32(_appSettings.EmailPort),
                                EmailUsername = _appSettings.EmailUsername,
                                FromEmail = _appSettings.FromEmail,
                                FromName = _appSettings.FromName,
                            };
                            bool isSuccess = SendMailMessage(Quote.JoinerEmailId, null, null, "Joining Notification for the Quote : " + model.QuoteNo, emailBody, setting, null);
                        }
                    }
                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.Quote.AlreadyExists;
                    response.Success = false;
                }
                else if (Convert.ToInt32(result) == 2)
                {
                    response.Message = _commonMessages.Quote.StockNotAvailable;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.Quote.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("SaveQuote", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpPost("SaveCustomerQuote")]
        public async Task<BaseApiResponse> SaveCustomerQuote(QuoteModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            var BasePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");
            string emailBody = "";
            try
            {
               
                var result = await _QuoteService.SaveCustomerQuoteData(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.Quote.SaveSuccess;
                    response.Success = true;

                    
                        var Quote = await _QuoteService.GetQuoteById(model.QuoteId);
                        if (!Directory.Exists(BasePath))
                        {
                            Directory.CreateDirectory(BasePath);
                        }
                        using (StreamReader reader = new StreamReader(Path.Combine(BasePath, "JoinerUpdate.html")))
                        {
                            //string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            //string scheme = _httpContextAccessor.HttpContext.Request.Scheme;
                            //string hosturl = scheme + "://" + host;

                            emailBody = reader.ReadToEnd();
                            emailBody = emailBody.Replace("{{Joiner_Name}}", model.Joiners);
                            emailBody = emailBody.Replace("{{Quote_No}}", model.QuoteNo);
                            emailBody = emailBody.Replace("{{URL}}", _config["Data:APPURL"].ToString());
                            emailBody = emailBody.Replace("{{Customer_Name}}", Quote.CustomerName);
                        }


                        EmailSetting setting = new EmailSetting
                        {
                            EmailEnableSsl = Convert.ToBoolean(_appSettings.EmailEnableSsl),
                            EmailHostName = _appSettings.EmailHostName,
                            EmailPassword = _appSettings.EmailPassword,
                            EmailAppPassword = _appSettings.EmailAppPassword,
                            EmailPort = Convert.ToInt32(_appSettings.EmailPort),
                            EmailUsername = _appSettings.EmailUsername,
                            FromEmail = _appSettings.FromEmail,
                            FromName = _appSettings.FromName,
                        };
                        bool isSuccess = SendMailMessage(_config["Data:AdminEmailId"].ToString(), null, null, "Joiner Update Notification for the Quote : "+ model.QuoteNo, emailBody, setting, null);

                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.Quote.AlreadyExists;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.Quote.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("SaveQuote", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpPost("AddVersion")]
        public async Task<BaseApiResponse> AddVersion(QuoteModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _QuoteService.AddVersion(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.QuoteVersion.SaveSuccess;
                    response.Success = true;
                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.QuoteVersion.AlreadyExists;
                    response.Success = false;
                }
                else if (Convert.ToInt32(result) == 2)
                {
                    response.Message = _commonMessages.QuoteVersion.StockNotAvailable;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.QuoteVersion.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("AddVersion", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpPost("GetAdditionalInfoQuote")]
        public async Task<ApiResponse<AdditionalInfoModel>> GetAdditionalInfoQuote(CommonPaginationModel model)
        {
            ApiResponse<AdditionalInfoModel> response = new ApiResponse<AdditionalInfoModel>() { Data = new List<AdditionalInfoModel>() };
            try
            {
                var data = await _QuoteService.GetAdditionalInfoQuote(model);
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
        [HttpPost("SaveSetting")]
        
        public async Task<BaseApiResponse> SaveSetting( SettingModel model)
        {

            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _QuoteService.SaveSettingData(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.Quote.SaveSuccess;
                    response.Success = true;
                }
                else if (Convert.ToInt32(result) == 1)
                {
                    response.Message = _commonMessages.Quote.AlreadyExists;
                    response.Success = false;
                }
                else
                {
                    response.Message = _commonMessages.Quote.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("Save", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteQuote")]
        public async Task<BaseApiResponse> DeleteQuote(CommonIdModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _QuoteService.DeleteQuote(model);
                if (result)
                {
                    response.Message = _commonMessages.Quote.DeleteSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = _commonMessages.Quote.DeleteError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("DeleteQuote", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpGet("DownloadQuotePDF/{QuoteId}")]
        public async Task<ApiPostResponse<QuoteModel>> DownloadQuotePDF(long QuoteId)
        {
            ApiPostResponse<QuoteModel> response = new ApiPostResponse<QuoteModel>() { Data = new QuoteModel() };
            try
            {
                string baseUrl = "";
                var BasePathTemplate = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");
                string emailBody = "";
                string emailHeader = "";
                string ItemBody = "";
                string AdditionInfoBody = "";
                var data = await _QuoteService.GetQuoteById(QuoteId);
                CommonPaginationModel model = new CommonPaginationModel();
                model.id = QuoteId;

                var dataQuoteVersion = await _QuoteDetailService.GetQuoteVersionList(model);
                var QuoteVersion = dataQuoteVersion.FirstOrDefault();
                {
                    CommonPaginationModel modelQV = new CommonPaginationModel();
                    modelQV.id = QuoteVersion.QuoteVersionId;
                    var dataQuoteCollectionDetail = await _QuoteDetailService.GetQuoteCollectionDetailList(modelQV);
                    var dataQuoteItemDetail = await _QuoteDetailService.GetQuoteItemDetailList(modelQV);
                    var dataQuoteAdditionalInfo = await _QuoteDetailService.GetQuoteAdditionalInfoList(modelQV);

                    dataQuoteVersion[0].itemCollection = dataQuoteCollectionDetail;
                    dataQuoteVersion[0].itemDetail = dataQuoteItemDetail;
                    dataQuoteVersion[0].additionalInfo = dataQuoteAdditionalInfo;

                }
                data.multiitemCollection = dataQuoteVersion;

                foreach (ItemDetailModel idm in dataQuoteVersion[0].itemDetail)
                {
                    ItemBody += @"<tr>
                            <td>" + idm.itemName + @"</td>
                            <td style='text-align: right;'>" + idm.quotedQty + @"</td>
                            
                        </tr>";
                    //<td>£" + idm.quotedCost + @"</td>
                }
                foreach (AdditionalInfoModel aim in dataQuoteVersion[0].additionalInfo)
                {
                    AdditionInfoBody += @"<tr>
                            <td>" + aim.details + @"</td>
                            <td>" + aim.description + @"</td>
                            <td style='text-align: right;'>" + aim.quotedQty + @"</td>
                           
                        </tr>";
                     //<td>£" + aim.cost + @"</td>
                }

                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "QuotePrintV1.html")))
                {
                    emailBody = reader.ReadToEnd();
                    emailBody = emailBody.Replace("{{QuoteNo}}", data.QuoteNo);
                    emailBody = emailBody.Replace("{{CustomerName}}", data.CustomerName);
                    emailBody = emailBody.Replace("{{SiteAddress1}}", data.SiteAddress1);
                    emailBody = emailBody.Replace("{{SiteAddress1Display}}", (!string.IsNullOrEmpty(data.SiteAddress1) ? "block" : "none"));

                    emailBody = emailBody.Replace("{{SiteAddress2}}", data.SiteAddress2);
                    emailBody = emailBody.Replace("{{SiteAddress2Display}}", (!string.IsNullOrEmpty(data.SiteAddress2) ? "block" : "none"));

                    emailBody = emailBody.Replace("{{Description}}", data.Description);
                    emailBody = emailBody.Replace("{{DescDisplay}}", (!string.IsNullOrEmpty(data.Description) ? "block" : "none"));

                    emailBody = emailBody.Replace("{{City}}", data.City);
                    emailBody = emailBody.Replace("{{CityDisplay}}", (!string.IsNullOrEmpty(data.City) ? "block" : "none"));

                    emailBody = emailBody.Replace("{{PostCode}}", data.PostCode);
                    emailBody = emailBody.Replace("{{PostCodeDisplay}}", (!string.IsNullOrEmpty(data.PostCode) ? "block" : "none"));

                    emailBody = emailBody.Replace("{{ItemDetails}}", ItemBody);
                    emailBody = emailBody.Replace("{{ItemDetailDisplay}}", (!string.IsNullOrEmpty(ItemBody) ? "block" : "none"));

                    emailBody = emailBody.Replace("{{AdditionalInfoDetails}}", AdditionInfoBody);
                    emailBody = emailBody.Replace("{{AddInfoDisplay}}", (!string.IsNullOrEmpty(AdditionInfoBody) ? "block" : "none"));

                    emailBody = emailBody.Replace("{{TotalQuotedCost}}", QuoteVersion.totalAmt.ToString());
                    emailBody = emailBody.Replace("{{AdminFee}}", "0");
                    emailBody = emailBody.Replace("{{QuotedLabourDays}}", QuoteVersion.labourDays.ToString());
                    emailBody = emailBody.Replace("{{LabourRate}}", QuoteVersion.labourRate.ToString());
                    emailBody = emailBody.Replace("{{LabourCost}}", QuoteVersion.labourCost.ToString());

                    emailBody = emailBody.Replace("{{IsPlanSupplied}}", (data.IsPlanSupplied? "Yes":"No"));
                    emailBody = emailBody.Replace("{{JoineryInformation}}", !string.IsNullOrEmpty(data.JoineryInformation)? data.JoineryInformation.ToString():"");
                    emailBody = emailBody.Replace("{{DoorThickness}}", !string.IsNullOrEmpty(data.DoorThickness) ? data.DoorThickness.ToString() : "");
                    emailBody = emailBody.Replace("{{CabinetThickness}}", !string.IsNullOrEmpty(data.CabinetThickness) ? data.CabinetThickness.ToString() : "");
                    emailBody = emailBody.Replace("{{ShelfThickness}}", !string.IsNullOrEmpty(data.ShelfThickness) ? data.ShelfThickness.ToString() : "");
                    emailBody = emailBody.Replace("{{OtherInformation}}", !string.IsNullOrEmpty(data.OtherInformation) ? data.OtherInformation.ToString() : "");
                    emailBody = emailBody.Replace("{{Plinth}}", !string.IsNullOrEmpty(data.Plinth) ? data.Plinth.ToString() : "");
                    emailBody = emailBody.Replace("{{Fillers}}", !string.IsNullOrEmpty(data.Fillers) ? data.Fillers.ToString() : "");
                    emailBody = emailBody.Replace("{{Handles}}", !string.IsNullOrEmpty(data.Handles) ? data.Handles.ToString() : "");

                    emailBody = emailBody.Replace("{{KnobsOrHandles}}", !string.IsNullOrEmpty(data.KnobsOrHandles) ? data.KnobsOrHandles.ToString() : "");
                    emailBody = emailBody.Replace("{{WhatIsTheSpec}}", !string.IsNullOrEmpty(data.WhatIsTheSpec) ? data.WhatIsTheSpec.ToString() : "");
                    emailBody = emailBody.Replace("{{TypeOfHinges}}", !string.IsNullOrEmpty(data.TypeOfHinges) ? data.TypeOfHinges.ToString() : "");
                    emailBody = emailBody.Replace("{{ShadowGap}}", !string.IsNullOrEmpty(data.ShadowGap) ? data.ShadowGap.ToString() : "");
                    emailBody = emailBody.Replace("{{Paintfinish}}", !string.IsNullOrEmpty(data.Paintfinish) ? data.Paintfinish.ToString() : "");
                    emailBody = emailBody.Replace("{{Painter}}", !string.IsNullOrEmpty(data.Painter) ? data.Painter.ToString() : "");
                    emailBody = emailBody.Replace("{{Spray}}", !string.IsNullOrEmpty(data.Spray) ? data.Spray.ToString() : "");
                    emailBody = emailBody.Replace("{{ColourInside}}", !string.IsNullOrEmpty(data.ColourInside) ? data.ColourInside.ToString() : "");
                    emailBody = emailBody.Replace("{{ColourOutside}}", !string.IsNullOrEmpty(data.ColourOutside) ? data.ColourOutside.ToString() : ""); ;

                    emailBody = emailBody.Replace("{{Lighting}}", !string.IsNullOrEmpty(data.Lighting) ? data.Lighting.ToString() : "");
                    emailBody = emailBody.Replace("{{SpecNeedToOrder}}", !string.IsNullOrEmpty(data.SpecNeedToOrder) ? data.SpecNeedToOrder.ToString() : "");
                    emailBody = emailBody.Replace("{{IsNeedPrePainting}}", (data.IsNeedPrePainting ? "Yes" : "No"));
                    emailBody = emailBody.Replace("{{IsElectricianRequired}}", (data.IsElectricianRequired ? "Yes" : "No"));
                    emailBody = emailBody.Replace("{{DetailRequirements}}", !string.IsNullOrEmpty(data.DetailRequirements) ? data.DetailRequirements.ToString() : "");
                    emailBody = emailBody.Replace("{{IsMirrors}}", (data.IsMirrors ? "Yes" : "No"));
                    emailBody = emailBody.Replace("{{MirrorSize}}", !string.IsNullOrEmpty(data.MirrorSize) ? data.MirrorSize.ToString() : "");
                    emailBody = emailBody.Replace("{{IsGlassShelves}}", (data.IsGlassShelves ? "Yes" : "No"));
                    emailBody = emailBody.Replace("{{GlassShelveSize}}", !string.IsNullOrEmpty(data.GlassShelveSize) ? data.GlassShelveSize.ToString() : "");
                    emailBody = emailBody.Replace("{{OtherItems}}", !string.IsNullOrEmpty(data.OtherItems) ? data.OtherItems.ToString() : "");
                    emailBody = emailBody.Replace("{{DateOfFinalMeasurement}}", data.DateOfFinalMeasurement!=null ? Convert.ToDateTime(data.DateOfFinalMeasurement).ToString("MM/dd/yyyy") : "");




                    emailBody = emailBody.Replace("{{JoineryInformationDisplay}}", (!string.IsNullOrEmpty(data.JoineryInformation) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{DoorThicknessDisplay}}", (!string.IsNullOrEmpty(data.DoorThickness) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{CabinetThicknessDisplay}}", (!string.IsNullOrEmpty(data.CabinetThickness) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{ShelfThicknessDisplay}}", (!string.IsNullOrEmpty(data.ShelfThickness) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{OtherInformationDisplay}}", (!string.IsNullOrEmpty(data.OtherInformation) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{PlinthDisplay}}", (!string.IsNullOrEmpty(data.Plinth) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{FillersDisplay}}", (!string.IsNullOrEmpty(data.Fillers) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{HandlesDisplay}}", (!string.IsNullOrEmpty(data.Handles) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{KnobsOrHandlesDisplay}}", (!string.IsNullOrEmpty(data.KnobsOrHandles) ? "block" : "none"));

                    emailBody = emailBody.Replace("{{WhatIsTheSpecDisplay}}", (!string.IsNullOrEmpty(data.WhatIsTheSpec) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{TypeOfHingesDisplay}}", (!string.IsNullOrEmpty(data.TypeOfHinges) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{ShadowGapDisplay}}", (!string.IsNullOrEmpty(data.ShadowGap) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{PaintfinishDisplay}}", (!string.IsNullOrEmpty(data.Paintfinish) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{PainterDisplay}}", (!string.IsNullOrEmpty(data.Painter) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{SprayDisplay}}", (!string.IsNullOrEmpty(data.Spray) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{ColourInsideDisplay}}", (!string.IsNullOrEmpty(data.ColourInside) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{ColourOutsideDisplay}}", (!string.IsNullOrEmpty(data.ColourOutside) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{LightingDisplay}}", (!string.IsNullOrEmpty(data.Lighting) ? "block" : "none"));


                    emailBody = emailBody.Replace("{{SpecNeedToOrderDisplay}}", (!string.IsNullOrEmpty(data.SpecNeedToOrder) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{DetailRequirementsDisplay}}", (!string.IsNullOrEmpty(data.DetailRequirements) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{MirrorSizeDisplay}}", (!string.IsNullOrEmpty(data.MirrorSize) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{GlassShelveSizeDisplay}}", (!string.IsNullOrEmpty(data.GlassShelveSize) ? "block" : "none"));
                    emailBody = emailBody.Replace("{{OtherItemsDisplay}}", (!string.IsNullOrEmpty(data.OtherItems) ? "block" : "none"));

                    emailBody = emailBody.Replace("{{Total}}", QuoteVersion.total.ToString());
                    emailBody = emailBody.Replace("{{VAT}}", QuoteVersion.vat.ToString());
                    emailBody = emailBody.Replace("{{NetTotal}}", QuoteVersion.netTotal.ToString());
                }

                //using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "QuoteHeader.html")))
                //{
                //    emailHeader = reader.ReadToEnd();
                //    emailHeader = emailHeader.Replace("{{QuoteNo}}", data.QuoteNo);
                //}

                    int headerHeight = 68;

                int footerHeight = 68;
                baseUrl = "http://example.com?page-height=1130&page-width=980";
                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                // header settings
                //converter.Options.DisplayHeader = true;
                //converter.Header.DisplayOnFirstPage = true;
                //converter.Header.DisplayOnOddPages = true;
                //converter.Header.DisplayOnEvenPages = true;
                //converter.Header.Height = headerHeight;

                //PdfHtmlSection headerHtml = new PdfHtmlSection(emailHeader,baseUrl);
                //headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                //converter.Header.Add(headerHtml);

                // footer settings
                //converter.Options.DisplayFooter = true;
                //converter.Footer.DisplayOnFirstPage = true;
                //converter.Footer.DisplayOnOddPages = true;
                //converter.Footer.DisplayOnEvenPages = true;
                //converter.Footer.Height = footerHeight;
                //converter.Options.WebPageHeight = 0;
                //converter.Options.WebPageFixedSize = false;
                //converter.Options.CssMediaType = HtmlToPdfCssMediaType.Print;
                //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                //PdfHtmlSection footerHtml = new PdfHtmlSection(Path.Combine(BasePathTemplate, "QuoteFooter.html"));
                //footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;

                //converter.Footer.Add(footerHtml);


                // add page numbering element to the footer

                // create a new pdf document converting an url
                converter.Options.JavaScriptEnabled = true;
                //converter.Options.CssMediaType = HtmlToPdfCssMediaType.Screen;
                converter.Options.PdfPageSize = PdfPageSize.A4;
                converter.Options.RenderingEngine = RenderingEngine.Blink;
                converter.Options.BlinkEnginePath = _dataConfig.BlinkEnginePath;//"C:\\Ritesh\\Projects\\Chromium-88.0.4324.0";
                converter.Options.PostLoadingScript = "generateReport({'page-height':1626,'page-width':1150});";
                converter.Options.PostLoadingScriptDelayAfter = 500;
                
                PdfDocument doc = converter.ConvertHtmlString(emailBody, "");

                //converter.Options.WebPageHeight = 1215 * (doc.Pages.Count);
                //doc = null;
                //doc = converter.ConvertHtmlString(emailBody, baseUrl);

                // custom header on page 3
                //if (doc.Pages.Count >= 3)
                //{
                //    PdfPage page = doc.Pages[2];

                //    PdfTemplate customHeader = doc.AddTemplate(
                //        page.PageSize.Width, headerHeight);
                //    PdfHtmlElement customHtml = new PdfHtmlElement(
                //        "<div><b>This is the custom header that will " +
                //        "appear only on page 3!</b></div>",
                //        string.Empty);
                //    customHeader.Add(customHtml);

                //    page.CustomHeader = customHeader;
                //}

                // save pdf document
                var BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/QuotePDF");
                string FileName = data.QuoteNo + ".pdf";
                if (!Directory.Exists(BasePath))
                {
                    Directory.CreateDirectory(BasePath);
                }
                var path = Path.Combine(BasePath, FileName);
                // save pdf document
                if (System.IO.File.Exists(FileName))
                {
                    System.IO.File.Delete(FileName);
                }
                doc.Save(path);

                // close pdf document
                doc.Close();
                response.TAID = _dataConfig.FilePath + "QuotePDF/" + FileName;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetQuoteAdditionalInfoList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
                return response;
            }
        }


        #endregion
    }
}
