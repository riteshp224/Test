using QuoteManagement.Data.DBRepository.DashBoard;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.DashBoard
{
    public class DashBoardService : IDashBoardService
    {
    
    #region Fields
    private readonly IDashBoardRepository _repository;
        #endregion

        #region Construtor
        public DashBoardService(IDashBoardRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<DashBoardModel> GetTotalCount()
        {
            return await _repository.GetTotalCount();
        }
        public async Task<List<Comparsionchart>> GetComparsionChartData()
        {
            return await _repository.GetComparsionChartData();

        }
        public async Task<List<ItemMasterModel>> GetPopularItemData()
        {
            return await _repository.GetPopularItemData();
        }
        public async Task<List<QuoteModel>> GetOngoingProject()
        {
            return await _repository.GetOngoingProject();
        }
        public async Task<List<QuoteModel>> GetOngoingQuotes()
        {
            return await _repository.GetOngoingQuotes();
        }

        public async Task<List<QuoteDataModel>> GetQuoteData(QuoteDataModel quoteDataModel)
        {
            return await _repository.GetQuoteData(quoteDataModel);
        }
        public async Task<List<MonthwiseQuoteDataModel>> GetMonthWiseQuoteData(Int64 QuoteId, DateTime FromDate, DateTime ToDate)
        {
            return await _repository.GetMonthWiseQuoteData(QuoteId, FromDate,ToDate);
        }
        #endregion

    }
}
