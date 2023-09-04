using QuoteManagement.Data.DBRepository.Report;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Report
{
   public class ReportService : IReportService
    {
        #region Fields
        private readonly IReportRepository _repository;
        #endregion

        #region Construtor
        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<List<ReportModel>> GetJoinerPendingUpdateList(CommonPaginationModel model)
        {
            return await _repository.GetJoinerPendingUpdateList(model);
        }
        public async Task<List<StatusWiseQuoteDetailModel>> getStatusWiseQuoteList(StatusWiseQuoteDetailModel model)
        {
            return await _repository.getStatusWiseQuoteList(model);
        }
        public async Task<List<CompletedQuoteDetailModel>> getCompletedQuoteList(CompletedQuoteDetailModel model)
        {
            return await _repository.getCompletedQuoteList(model);
        }
        public async Task<List<CustomerDetailModel>> GetCustomerListReport(CustomerDetailModel model)
        {
            return await _repository.GetCustomerListReport(model);
        }
        public async Task<List<LowItemStockDetailModel>> getLowStockItemList(LowItemStockDetailModel model)
        {
            return await _repository.getLowStockItemList(model);
        }

        public async Task<List<QuoteModel>> getStatuswiseQuoteDetails(QuoteModel model)
        {
            return await _repository.getStatuswiseQuoteDetails(model);
        }
        #endregion
    }
}
