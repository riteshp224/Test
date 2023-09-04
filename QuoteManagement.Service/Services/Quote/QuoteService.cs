using QuoteManagement.Data.DBRepository.Quote;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Quote
{
   public class QuoteService : IQuoteService
    {
        #region Fields
        private readonly IQuoteRepository _repository;
        #endregion

        #region Construtor
        public QuoteService(IQuoteRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<List<QuoteModel>> GetQuoteList(CommonPaginationModel model)
        {
            return await _repository.GetQuoteList(model);
        }
        public async Task<List<QuoteModel>> GetClosedQuoteList(CommonPaginationModel model)
        {
            return await _repository.GetClosedQuoteList(model);
        }
        public async Task<List<QuoteModel>> GetCustomerQuoteList(CommonPaginationModel model)
        {
            return await _repository.GetCustomerQuoteList(model);
        }
        
        public async Task<QuoteModel> GetQuoteById(long QuoteId)
        {
            return await _repository.GetQuoteById(QuoteId);
        }
        public async Task<QuoteModel> GetQuotecustomerById(long QuoteId)
        {
            return await _repository.GetQuotecustomerById(QuoteId);
        }
        public async Task<QuoteModel> CloneQuote(long QuoteId)
        {
            return await _repository.CloneQuote(QuoteId);
        }
        public async Task<List<QuoteCustomerDetail>> GetCustomerList()
        {
            return await _repository.GetCustomerList();
        }
        public async Task<List<MultiitemCollectionModel>> getQuoteVersionList(CommonPaginationModel model)
        {
            return await _repository.getQuoteVersionList(model);
        }
        public async Task<List<ItemCollectionMasterModel>> GetItemCollectionList()
        {
            return await _repository.GetItemCollectionList();
        }
        public async Task<QuoteCustomerDetail> GetCustomerDetailById(long CustomerId)
        {
            return await _repository.GetCustomerDetailById(CustomerId);
        }
        public async Task<List<StatusDetail>> GetStatusList()
        {
            return await _repository.GetStatusList();
        }
        public async Task<List<JoinerDetail>> getJoinersList()
        {
            return await _repository.getJoinersList();
        }
        public async Task<SettingModel> GetSetting(CommonPaginationModel model)
        {
            return await _repository.GetSetting(model);
        }
        #endregion

        #region Post

        public async Task<string> SaveQuoteData(QuoteModel model)
        {
            return await _repository.SaveQuoteData(model);
        }
        public async Task<string> SaveCustomerQuoteData(QuoteModel model)
        {
            return await _repository.SaveCustomerQuoteData(model);
        }
        public async Task<string> AddVersion(QuoteModel model)
        {
            return await _repository.AddVersion(model);
        }
        public async Task<string> SaveSettingData(SettingModel model)
        {
            return await _repository.SaveSettingData(model);
        }
        
        #endregion

        #region Delete
        public async Task<bool> DeleteQuote(CommonIdModel model)
        {
            return await _repository.DeleteQuote(model);
        }
        public async Task<List<AdditionalInfoModel>> GetAdditionalInfoQuote(CommonPaginationModel model)
        {
            return await _repository.GetAdditionalInfoQuote(model);
        }


        #endregion
    }
}
