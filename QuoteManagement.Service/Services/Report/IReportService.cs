using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Report
{
    public interface IReportService
    {
        #region Get
        Task<List<ReportModel>> GetJoinerPendingUpdateList(CommonPaginationModel model);
        Task<List<StatusWiseQuoteDetailModel>> getStatusWiseQuoteList(StatusWiseQuoteDetailModel model);
        Task<List<CompletedQuoteDetailModel>> getCompletedQuoteList(CompletedQuoteDetailModel model);
        Task<List<CustomerDetailModel>> GetCustomerListReport(CustomerDetailModel model);
        Task<List<LowItemStockDetailModel>> getLowStockItemList(LowItemStockDetailModel model);
        Task<List<QuoteModel>> getStatuswiseQuoteDetails(QuoteModel model);

        #endregion


    }
}
