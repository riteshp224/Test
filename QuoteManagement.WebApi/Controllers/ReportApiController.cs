using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Common;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
using QuoteManagement.Service.Services.Report;
using QuoteManagement.WebApi.Logger;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/Report")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReportApiController : ControllerBase
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private IReportService _ReportService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        private readonly DataConfig _dataConfig;
        private readonly ApplicationSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public ReportApiController(
            IReportService ReportService,
            IConfiguration config,
            IOptions<CommonMessages> commonMessages,
            IOptions<DataConfig> dataConfig,
            IOptions<ApplicationSettings> appSettings,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _ReportService = ReportService;
            _config = config;
            _commonMessages = commonMessages.Value;
            _dataConfig = dataConfig.Value;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Get
        [HttpPost("GetJoinerPendingUpdateList")]
        public async Task<ApiResponse<ReportModel>> GetJoinerPendingUpdateList(CommonPaginationModel model)
        {
            ApiResponse<ReportModel> response = new ApiResponse<ReportModel>() { Data = new List<ReportModel>() };
            try
            {
                var data = await _ReportService.GetJoinerPendingUpdateList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("GetJoinerPendingUpdateList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpGet("JoinerPendingPDF")]
        public async Task<ApiPostResponse<QuoteModel>> JoinerPendingPDF()
        {
            ApiPostResponse<QuoteModel> response = new ApiPostResponse<QuoteModel>() { Data = new QuoteModel() };
            try
            {
                string baseUrl = "";
                var BasePathTemplate = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");
                string emailBody = "";
                string emailHeader = "";
                string Body = "";
                CommonPaginationModel model = new CommonPaginationModel();

                var dataJoiner = await _ReportService.GetJoinerPendingUpdateList(model);
                foreach (ReportModel rm in dataJoiner)
                {
                    Body += @"<tr>
                            <td>" + rm.QuoteNo + @"</td>
                            <td>" + rm.QuoteName + @"</td>
                            <td>" + rm.CustomerName + @"</td>
                            <td>" + rm.Joiners + @"</td>
                        </tr>";
                }

                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "JoinerPendingUpdatePrint.html")))
                {
                    emailBody = reader.ReadToEnd();
                    emailBody = emailBody.Replace("{{ItemDetails}}", Body);
                }
                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "ReportHeader.html")))
                {
                    emailHeader = reader.ReadToEnd();
                    emailHeader = emailHeader.Replace("{{ReportTitle}}", "Joiner Pending Update List").Replace("{{-}}", "").Replace("{{Filter1}}", "").Replace("{{To}}", "").Replace("{{Filter2}}", "");
                    //emailHeader = emailHeader.Replace("{{-}}", "");
                    //emailHeader = emailHeader.Replace("{{Filter1}}", "");
                    //emailHeader = emailHeader.Replace("{{To}}", "");
                    //emailHeader = emailHeader.Replace("{{Filter2}}", "");

                }

                int headerHeight = 68;

                int footerHeight = 68;

                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                // header settings
                converter.Options.DisplayHeader = true;
                converter.Header.DisplayOnFirstPage = true;
                converter.Header.DisplayOnOddPages = true;
                converter.Header.DisplayOnEvenPages = true;
                converter.Header.Height = headerHeight;

                PdfHtmlSection headerHtml = new PdfHtmlSection(emailHeader, baseUrl);
                headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Header.Add(headerHtml);

                // footer settings
                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = footerHeight;
                converter.Options.WebPageHeight = 0;
                converter.Options.WebPageFixedSize = false;
                converter.Options.CssMediaType = HtmlToPdfCssMediaType.Print;
                //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                PdfHtmlSection footerHtml = new PdfHtmlSection(Path.Combine(BasePathTemplate, "ReportFooter.html"));
                footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;

                converter.Footer.Add(footerHtml);


                // add page numbering element to the footer

                converter.Options.CustomCSS = "background-color:black";
                // create a new pdf document converting an url
                converter.Options.CssMediaType = HtmlToPdfCssMediaType.Screen;
                PdfDocument doc = converter.ConvertHtmlString(emailBody, baseUrl);

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
                var BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/ReportPDF");
                string FileName = "joinerPendingUpdate.pdf";
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
                response.TAID = _dataConfig.FilePath + "ReportPDF/" + FileName;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("JoinerPendingPDF", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
                return response;
            }
        }

        [HttpPost("getStatusWiseQuoteList")]
        public async Task<ApiResponse<StatusWiseQuoteDetailModel>> getStatusWiseQuoteList(StatusWiseQuoteDetailModel model)
        {
            ApiResponse<StatusWiseQuoteDetailModel> response = new ApiResponse<StatusWiseQuoteDetailModel>() { Data = new List<StatusWiseQuoteDetailModel>() };
            try
            {
                var data = await _ReportService.getStatusWiseQuoteList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("getStatusWiseQuoteList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("StatusWiseQuotePDF")]
        public async Task<ApiPostResponse<StatusWiseQuoteDetailModel>> StatusWiseQuotePDF(StatusWiseQuoteDetailModel model)
        {
            ApiPostResponse<StatusWiseQuoteDetailModel> response = new ApiPostResponse<StatusWiseQuoteDetailModel>() { Data = new StatusWiseQuoteDetailModel() };
            try
            {
                string baseUrl = "";
                var BasePathTemplate = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");
                string emailBody = "";
                string emailHeader = "";
                string Body = "";
                // StatusWiseQuoteDetailModel model = new StatusWiseQuoteDetailModel();

                var dataJoiner = await _ReportService.getStatusWiseQuoteList(model);
                foreach (StatusWiseQuoteDetailModel rm in dataJoiner)
                {
                    Body += @"<tr>
                            <td>" + rm.Status + @"</td>
                            <td>" + rm.NoOfQuotes + @"</td>
                            <td>£" + rm.ToalValueOfQuotes + @"</td>
                        </tr>";
                }

                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "StatusWiseQuoateDetailPrint.html")))
                {
                    emailBody = reader.ReadToEnd();
                    emailBody = emailBody.Replace("{{ItemDetails}}", Body);
                }
                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "ReportHeader.html")))
                {
                    emailHeader = reader.ReadToEnd();
                    emailHeader = emailHeader.Replace("{{ReportTitle}}", "Status Wise Quote");
                    emailHeader = emailHeader.Replace("{{-}}","-");
                    emailHeader = emailHeader.Replace("{{Filter1}}", model.FromDate.ToString("MM/dd/yyyy"));
                    emailHeader = emailHeader.Replace("{{To}}", "To");
                    emailHeader = emailHeader.Replace("{{Filter2}}", model.ToDate.ToString("MM/dd/yyyy"));
                }

                int headerHeight = 68;

                int footerHeight = 68;

                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                // header settings
                converter.Options.DisplayHeader = true;
                converter.Header.DisplayOnFirstPage = true;
                converter.Header.DisplayOnOddPages = true;
                converter.Header.DisplayOnEvenPages = true;
                converter.Header.Height = headerHeight;

                PdfHtmlSection headerHtml = new PdfHtmlSection(emailHeader, baseUrl);
                headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Header.Add(headerHtml);

                // footer settings
                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = footerHeight;
                converter.Options.WebPageHeight = 0;
                converter.Options.WebPageFixedSize = false;
                converter.Options.CssMediaType = HtmlToPdfCssMediaType.Print;
                //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                PdfHtmlSection footerHtml = new PdfHtmlSection(Path.Combine(BasePathTemplate, "ReportFooter.html"));
                footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;

                converter.Footer.Add(footerHtml);


                // add page numbering element to the footer

                converter.Options.CustomCSS = "background-color:black";
                // create a new pdf document converting an url
                converter.Options.CssMediaType = HtmlToPdfCssMediaType.Screen;
                PdfDocument doc = converter.ConvertHtmlString(emailBody, baseUrl);

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
                var BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/ReportPDF");
                string FileName = "StatusWiseQuote.pdf";
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
                response.TAID = _dataConfig.FilePath + "ReportPDF/" + FileName;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("StatusWiseQuotePDF", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
                return response;
            }
        }

        [HttpPost("getCompletedQuoteList")]
        public async Task<ApiResponse<CompletedQuoteDetailModel>> getCompletedQuoteList(CompletedQuoteDetailModel model)
        {
            ApiResponse<CompletedQuoteDetailModel> response = new ApiResponse<CompletedQuoteDetailModel>() { Data = new List<CompletedQuoteDetailModel>() };
            try
            {
                var data = await _ReportService.getCompletedQuoteList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("getCompletedQuoteList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("CompletedQuotePDF")]
        public async Task<ApiPostResponse<CompletedQuoteDetailModel>> CompletedQuotePDF(CompletedQuoteDetailModel model)
        {
            ApiPostResponse<CompletedQuoteDetailModel> response = new ApiPostResponse<CompletedQuoteDetailModel>() { Data = new CompletedQuoteDetailModel() };
            try
            {
                string baseUrl = "";
                var BasePathTemplate = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");
                string emailBody = "";
                string emailHeader = "";
                string Body = "";
                // StatusWiseQuoteDetailModel model = new StatusWiseQuoteDetailModel();

                var dataJoiner = await _ReportService.getCompletedQuoteList(model);
                foreach (CompletedQuoteDetailModel rm in dataJoiner)
                {
                    Body += @"<tr>
                            <td>" + rm.CompletedQuote + @"</td>
                            <td>£" + rm.ToalQuotedAmount + @"</td>
                            <td>£" + rm.ToalActualAmount + @"</td>
                            <td>£" + rm.ProfitAmount + @"</td>
                        </tr>";
                }

                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "CompletedQuoateDetailPrint.html")))
                {
                    emailBody = reader.ReadToEnd();
                    emailBody = emailBody.Replace("{{ItemDetails}}", Body);
                }
                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "ReportHeader.html")))
                {
                    emailHeader = reader.ReadToEnd();
                    emailHeader = emailHeader.Replace("{{ReportTitle}}", "Completed Quote");
                    emailHeader = emailHeader.Replace("{{-}}", "-");
                    emailHeader = emailHeader.Replace("{{Filter1}}", model.FromDate.ToString("MM/dd/yyyy"));
                    emailHeader = emailHeader.Replace("{{To}}", "To");
                    emailHeader = emailHeader.Replace("{{Filter2}}", model.ToDate.ToString("MM/dd/yyyy"));
                }

                int headerHeight = 68;

                int footerHeight = 68;

                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                // header settings
                converter.Options.DisplayHeader = true;
                converter.Header.DisplayOnFirstPage = true;
                converter.Header.DisplayOnOddPages = true;
                converter.Header.DisplayOnEvenPages = true;
                converter.Header.Height = headerHeight;

                PdfHtmlSection headerHtml = new PdfHtmlSection(emailHeader, baseUrl);
                headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Header.Add(headerHtml);

                // footer settings
                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = footerHeight;
                converter.Options.WebPageHeight = 0;
                converter.Options.WebPageFixedSize = false;
                converter.Options.CssMediaType = HtmlToPdfCssMediaType.Print;
                //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                PdfHtmlSection footerHtml = new PdfHtmlSection(Path.Combine(BasePathTemplate, "ReportFooter.html"));
                footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;

                converter.Footer.Add(footerHtml);


                // add page numbering element to the footer

                converter.Options.CustomCSS = "background-color:black";
                // create a new pdf document converting an url
                converter.Options.CssMediaType = HtmlToPdfCssMediaType.Screen;
                PdfDocument doc = converter.ConvertHtmlString(emailBody, baseUrl);

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
                var BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/ReportPDF");
                string FileName = "CompletedQuoateDetail.pdf";
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
                response.TAID = _dataConfig.FilePath + "ReportPDF/" + FileName;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("CompletedQuotePDF", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
                return response;
            }
        }

        [HttpPost("getCustomerListReport")]
        public async Task<ApiResponse<CustomerDetailModel>> getCustomerListReport(CustomerDetailModel model)
        {
            ApiResponse<CustomerDetailModel> response = new ApiResponse<CustomerDetailModel>() { Data = new List<CustomerDetailModel>() };
            try
            {
                var data = await _ReportService.GetCustomerListReport(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("getCustomerListReport", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("CustomerDetailPDF")]
        public async Task<ApiPostResponse<CustomerDetailModel>> CustomerDetailPDF(CustomerDetailModel model)
        {
            ApiPostResponse<CustomerDetailModel> response = new ApiPostResponse<CustomerDetailModel>() { Data = new CustomerDetailModel() };
            try
            {
                string baseUrl = "";
                var BasePathTemplate = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");
                string emailBody = "";
                string emailHeader = "";
                string Body = "";
                // StatusWiseQuoteDetailModel model = new StatusWiseQuoteDetailModel();

                var dataJoiner = await _ReportService.GetCustomerListReport(model);
                foreach (CustomerDetailModel rm in dataJoiner)
                {
                    Body += @"<tr>
                            <td>" + rm.FullName + @"</td>
                            <td>" + rm.Email + @"</td>
                            <td>" + rm.Phone+ @"</td>
                            <td>" + rm.LeadSource+ @"</td>
                            <td>" + rm.address+ @"</td>
                        </tr>";
                }

                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "CustomerDetailPrint.html")))
                {
                    emailBody = reader.ReadToEnd();
                    emailBody = emailBody.Replace("{{ItemDetails}}", Body);
                }
                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "ReportHeader.html")))
                {
                    emailHeader = reader.ReadToEnd();
                    emailHeader = emailHeader.Replace("{{ReportTitle}}", "Customer Details");
                    emailHeader = emailHeader.Replace("{{-}}", (!string.IsNullOrEmpty(model.LeadSource)?"- Lead Source : ":""));
                    emailHeader = emailHeader.Replace("{{Filter1}}", model.LeadSource);
                    emailHeader = emailHeader.Replace("{{To}}", "");
                    emailHeader = emailHeader.Replace("{{Filter2}}", "");
                }

                int headerHeight = 68;

                int footerHeight = 68;

                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                // header settings
                converter.Options.DisplayHeader = true;
                converter.Header.DisplayOnFirstPage = true;
                converter.Header.DisplayOnOddPages = true;
                converter.Header.DisplayOnEvenPages = true;
                converter.Header.Height = headerHeight;

                PdfHtmlSection headerHtml = new PdfHtmlSection(emailHeader, baseUrl);
                headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Header.Add(headerHtml);

                // footer settings
                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = footerHeight;
                converter.Options.WebPageHeight = 0;
                converter.Options.WebPageFixedSize = false;
                converter.Options.CssMediaType = HtmlToPdfCssMediaType.Print;
                //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                PdfHtmlSection footerHtml = new PdfHtmlSection(Path.Combine(BasePathTemplate, "ReportFooter.html"));
                footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;

                converter.Footer.Add(footerHtml);


                // add page numbering element to the footer

                converter.Options.CustomCSS = "background-color:black";
                // create a new pdf document converting an url
                converter.Options.CssMediaType = HtmlToPdfCssMediaType.Screen;
                PdfDocument doc = converter.ConvertHtmlString(emailBody, baseUrl);

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
                var BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/ReportPDF");
                string FileName = "CustomerDetail.pdf";
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
                response.TAID = _dataConfig.FilePath + "ReportPDF/" + FileName;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("CompletedQuotePDF", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
                return response;
            }
        }

        [HttpPost("getLowStockItemList")]
        public async Task<ApiResponse<LowItemStockDetailModel>> getLowStockItemList(LowItemStockDetailModel model)
        {
            ApiResponse<LowItemStockDetailModel> response = new ApiResponse<LowItemStockDetailModel>() { Data = new List<LowItemStockDetailModel>() };
            try
            {
                var data = await _ReportService.getLowStockItemList(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("getLowStockItemList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        [HttpPost("LowStockItemPDF")]
        public async Task<ApiPostResponse<LowItemStockDetailModel>> LowStockItemPDF(LowItemStockDetailModel model)
        {
            ApiPostResponse<LowItemStockDetailModel> response = new ApiPostResponse<LowItemStockDetailModel>() { Data = new LowItemStockDetailModel() };
            try
            {
                string baseUrl = "";
                var BasePathTemplate = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");
                string emailBody = "";
                string emailHeader = "";
                string Body = "";
                // StatusWiseQuoteDetailModel model = new StatusWiseQuoteDetailModel();

                var dataJoiner = await _ReportService.getLowStockItemList(model);
                foreach (LowItemStockDetailModel rm in dataJoiner)
                {
                    string Bodysub = "";
                    if(rm.IsStock==true && rm.AvailableStock>=5)
                    {
                        Bodysub = "<td style='color:#10B990'>In Stock [" + rm.AvailableStock+"]</td>";
                    }
                    else if (rm.IsStock == true && rm.AvailableStock < 5)
                    {
                        Bodysub = "<td style='color:rgba(249, 177, 21, 1)'>Low Stock [" + rm.AvailableStock + "]</td>";
                    }
                    else if (rm.IsStock == false)
                    {
                        Bodysub = "<td style='color:#F81A34'>Out Of Stock [" + rm.AvailableStock + "]</td>";
                    }

                    Body += @"<tr>
                            <td>" + rm.ItemName + @"</td>
                            <td>£" + rm.Cost + @"</td>
                            " + Bodysub + @"
                        </tr>";
                }

                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "LowStockItemDetailPrint.html")))
                {
                    emailBody = reader.ReadToEnd();
                    emailBody = emailBody.Replace("{{ItemDetails}}", Body);
                }
                using (StreamReader reader = new StreamReader(Path.Combine(BasePathTemplate, "ReportHeader.html")))
                {
                    emailHeader = reader.ReadToEnd();
                    emailHeader = emailHeader.Replace("{{ReportTitle}}", "Low Stock Item");
                    emailHeader = emailHeader.Replace("{{-}}", "");
                    emailHeader = emailHeader.Replace("{{Filter1}}", "");
                    emailHeader = emailHeader.Replace("{{To}}", "");
                    emailHeader = emailHeader.Replace("{{Filter2}}", "");
                }

                int headerHeight = 68;

                int footerHeight = 68;

                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                // header settings
                converter.Options.DisplayHeader = true;
                converter.Header.DisplayOnFirstPage = true;
                converter.Header.DisplayOnOddPages = true;
                converter.Header.DisplayOnEvenPages = true;
                converter.Header.Height = headerHeight;

                PdfHtmlSection headerHtml = new PdfHtmlSection(emailHeader, baseUrl);
                headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Header.Add(headerHtml);

                // footer settings
                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = footerHeight;
                converter.Options.WebPageHeight = 0;
                converter.Options.WebPageFixedSize = false;
                converter.Options.CssMediaType = HtmlToPdfCssMediaType.Print;
                //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                PdfHtmlSection footerHtml = new PdfHtmlSection(Path.Combine(BasePathTemplate, "ReportFooter.html"));
                footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;

                converter.Footer.Add(footerHtml);


                // add page numbering element to the footer

                converter.Options.CustomCSS = "background-color:black";
                // create a new pdf document converting an url
                converter.Options.CssMediaType = HtmlToPdfCssMediaType.Screen;
                PdfDocument doc = converter.ConvertHtmlString(emailBody, baseUrl);

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
                var BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/ReportPDF");
                string FileName = "LowStockItem.pdf";
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
                response.TAID = _dataConfig.FilePath + "ReportPDF/" + FileName;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("LowStockItemPDF", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
                return response;
            }
        }

        [HttpPost("getStatuswiseQuoteDetails")]
        public async Task<ApiResponse<QuoteModel>> getStatuswiseQuoteDetails(QuoteModel model)
        {
            ApiResponse<QuoteModel> response = new ApiResponse<QuoteModel>() { Data = new List<QuoteModel>() };
            try
            {

                var data = await _ReportService.getStatuswiseQuoteDetails(model);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                string st = _commonMessages.CreateCommonMessage("getLowStockItemList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }

        

       
        #endregion
    }
}
