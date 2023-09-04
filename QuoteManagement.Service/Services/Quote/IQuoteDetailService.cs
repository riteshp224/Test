using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Quote
{
    public interface IQuoteDetailService
    {
        #region Get
        Task<List<QuoteDetailModel>> GetQuoteDetailList(CommonPaginationModel model); 
        Task<List<QuoteDetailModel>> GetQuotecustomerDetailList(CommonPaginationModel model); 
         Task<List<QuoteDetailModel>> GetItemCollectionDetailList(CommonPaginationModel model);
        Task<QuoteDetailModel> GetQuoteDetailById(long QuoteDetailId);
        Task<List<QuoteDetailModel>> CloneQuoteDetailList(CommonPaginationModel model);
        #endregion

        #region Post
        Task<string> SaveQuoteDetailData(QuoteDetailModel model);
        #endregion

        #region Delete
        Task<bool> DeleteQuoteDetail(CommonIdModel model);
        #endregion

        #region QuoteVersioning Page
        Task<List<MultiitemCollectionModel>> GetQuoteVersionList(CommonPaginationModel model);
        Task<List<ItemDetailModel>> GetQuoteItemDetailList(CommonPaginationModel model);
        Task<List<ItemCollectionModel>> GetQuoteCollectionDetailList(CommonPaginationModel model);
        Task<List<AdditionalInfoModel>> GetQuoteAdditionalInfoList(CommonPaginationModel model);
        #endregion

        #region QuoteCloneVersioning Page
        Task<List<MultiitemCollectionModel>> GetCloneQuoteVersionList(CommonPaginationModel model);
        Task<List<ItemDetailModel>> GetCloneQuoteItemDetailList(CommonPaginationModel model);
        Task<List<ItemCollectionModel>> GetCloneQuoteCollectionDetailList(CommonPaginationModel model);
        Task<List<AdditionalInfoModel>> GetCloneQuoteAdditionalInfoList(CommonPaginationModel model);
        #endregion
    }
}
