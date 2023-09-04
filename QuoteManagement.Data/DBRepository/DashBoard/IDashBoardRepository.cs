using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Data.DBRepository.DashBoard
{
    public interface IDashBoardRepository
    {
        #region Get
        Task<DashBoardModel> GetTotalCount();
        Task<List<Comparsionchart>> GetComparsionChartData();
        Task<List<ItemMasterModel>> GetPopularItemData();
       Task<List<QuoteModel>> GetOngoingProject();
        Task<List<QuoteModel>> GetOngoingQuotes();
        Task<List<QuoteDataModel>> GetQuoteData(QuoteDataModel quoteDataModel);
        Task<List<MonthwiseQuoteDataModel>> GetMonthWiseQuoteData(Int64 QuoteId, DateTime FromDate, DateTime ToDate);
        #endregion

    }
}
