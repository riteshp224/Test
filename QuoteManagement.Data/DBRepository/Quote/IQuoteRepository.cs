using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace QuoteManagement.Data.DBRepository.Quote
{
   public interface IQuoteRepository
    {
        #region Get
        Task<List<QuoteModel>> GetQuoteList(CommonPaginationModel model); 
        Task<List<QuoteModel>> GetClosedQuoteList(CommonPaginationModel model); 
        Task<List<QuoteModel>> GetCustomerQuoteList(CommonPaginationModel model); 
         Task<QuoteModel> GetQuoteById(long QuoteId);
        Task<QuoteModel> GetQuotecustomerById(long QuoteId);
        Task<QuoteModel> CloneQuote(long QuoteId);
        Task<SettingModel> GetSetting(CommonPaginationModel model);

        Task<List<QuoteCustomerDetail>> GetCustomerList();
        Task<List<StatusDetail>> GetStatusList();
        Task<List<JoinerDetail>> getJoinersList(); 
         Task<List<MultiitemCollectionModel>> getQuoteVersionList(CommonPaginationModel model);
        Task<List<ItemCollectionMasterModel>> GetItemCollectionList();
        Task<QuoteCustomerDetail> GetCustomerDetailById(long CustomerId);
        #endregion

        #region Post
        Task<string> SaveQuoteData(QuoteModel model); 
        Task<string> SaveCustomerQuoteData(QuoteModel model); 
         Task<string> AddVersion(QuoteModel model);
         Task<string> SaveSettingData(SettingModel model);
        
            Task<List<AdditionalInfoModel>> GetAdditionalInfoQuote(CommonPaginationModel model);
        #endregion

        #region Delete
        Task<bool> DeleteQuote(CommonIdModel model);
        #endregion
    }
}
