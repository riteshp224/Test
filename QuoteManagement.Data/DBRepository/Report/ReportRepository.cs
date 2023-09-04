using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Data.DBRepository.Quote;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
namespace QuoteManagement.Data.DBRepository.Report
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public ReportRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Get
        public async Task<List<ReportModel>> GetJoinerPendingUpdateList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                var data = await QueryAsync<ReportModel>("SP_Reports", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<StatusWiseQuoteDetailModel>> getStatusWiseQuoteList(StatusWiseQuoteDetailModel model)
        {
            try
            {
                var today = DateTime.Now;
                var param = new DynamicParameters();
                param.Add("@Type", 2);
                param.Add("@FromDate", (model.FromDate != null ? Convert.ToDateTime(model.FromDate.Date) : new DateTime(today.Year, today.Month, 1)));
                param.Add("@ToDate", (model.ToDate != null ? Convert.ToDateTime(model.ToDate.Date).AddDays(1).AddSeconds(-1) : new DateTime(today.Year, today.Month, 1).AddMonths(1).AddSeconds(-1)));
                var data = await QueryAsync<StatusWiseQuoteDetailModel>("SP_Reports", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CompletedQuoteDetailModel>> getCompletedQuoteList(CompletedQuoteDetailModel model)
        {
            try
            {
                var today = DateTime.Now;
                var param = new DynamicParameters();
                param.Add("@Type", 3);
                param.Add("@FromDate", (model.FromDate != null ? Convert.ToDateTime(model.FromDate.Date) : new DateTime(today.Year, today.Month, 1)));
                param.Add("@ToDate", (model.ToDate != null ? Convert.ToDateTime(model.ToDate.Date).AddDays(1).AddSeconds(-1) : new DateTime(today.Year, today.Month, 1).AddMonths(1).AddSeconds(-1)));
                var data = await QueryAsync<CompletedQuoteDetailModel>("SP_Reports", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<CustomerDetailModel>> GetCustomerListReport(CustomerDetailModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 4);
                param.Add("@LeadSource", model.LeadSource);
                var data = await QueryAsync<CustomerDetailModel>("SP_Reports", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LowItemStockDetailModel>> getLowStockItemList(LowItemStockDetailModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 5);
                var data = await QueryAsync<LowItemStockDetailModel>("SP_Reports", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<QuoteModel>> getStatuswiseQuoteDetails(QuoteModel model)
        {
            try
            {
                var today = DateTime.Now;
                var param = new DynamicParameters();
                param.Add("@Type", 6);
                param.Add("@QuoteStatusId", Convert.ToInt32(model.QuoteStatusId));
                param.Add("@FromDate", (model.FromDate != null ? Convert.ToDateTime(model.FromDate.Date) : new DateTime(today.Year, today.Month, 1)));
                param.Add("@ToDate", (model.ToDate != null ? Convert.ToDateTime(model.ToDate.Date).AddDays(1).AddSeconds(-1) : new DateTime(today.Year, today.Month, 1).AddMonths(1).AddSeconds(-1)));
                var data = await QueryAsync<QuoteModel>("SP_Reports", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
