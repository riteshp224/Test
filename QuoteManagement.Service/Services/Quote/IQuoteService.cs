using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Quote
{
    public interface IQuoteService
    {
        #region Get
        Task<List<QuoteModel>> GetQuoteList(CommonPaginationModel model); 
        Task<List<QuoteModel>> GetClosedQuoteList(CommonPaginationModel model); 
        Task<List<QuoteModel>> GetCustomerQuoteList(CommonPaginationModel model); 
         Task<QuoteModel> GetQuoteById(long QuoteId);
        Task<QuoteModel> GetQuotecustomerById(long QuoteId);
        Task<QuoteModel> CloneQuote(long QuoteId);

        Task<List<QuoteCustomerDetail>> GetCustomerList();
        Task<List<StatusDetail>> GetStatusList();
        Task<List<JoinerDetail>> getJoinersList();
        Task<List<MultiitemCollectionModel>> getQuoteVersionList(CommonPaginationModel model);
        Task<QuoteCustomerDetail> GetCustomerDetailById(long CustomerId);

        Task<List<ItemCollectionMasterModel>> GetItemCollectionList();
        #endregion

        #region Post
        Task<string> SaveQuoteData(QuoteModel model);
        Task<string> SaveSettingData(SettingModel model); 
        Task<string> SaveCustomerQuoteData(QuoteModel model); 
         Task<string> AddVersion(QuoteModel model); 
         Task<List<AdditionalInfoModel>> GetAdditionalInfoQuote(CommonPaginationModel model);
        Task<SettingModel> GetSetting(CommonPaginationModel model);

        #endregion

        #region Delete
        Task<bool> DeleteQuote(CommonIdModel model);
        #endregion
    }
}
