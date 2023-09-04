using QuoteManagement.Data.DBRepository.Quote;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace QuoteManagement.Service.Services.Quote
{
    public class QuoteDetailService : IQuoteDetailService
    {
        #region Fields
        private readonly IQuoteDetailRepository _repository;
        #endregion

        #region Construtor
        public QuoteDetailService(IQuoteDetailRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<List<QuoteDetailModel>> GetQuoteDetailList(CommonPaginationModel model)
        {
            return await _repository.GetQuoteDetailList(model);
        }
        public async Task<List<QuoteDetailModel>> GetQuotecustomerDetailList(CommonPaginationModel model)
        {
            return await _repository.GetQuotecustomerDetailList(model);
        }
        public async Task<List<QuoteDetailModel>> GetItemCollectionDetailList(CommonPaginationModel model)
        {
            return await _repository.GetItemCollectionDetailList(model);
        }
        public async Task<QuoteDetailModel> GetQuoteDetailById(long QuoteDetailId)
        {
            return await _repository.GetQuoteDetailById(QuoteDetailId);
        }

        public async Task<List<QuoteDetailModel>> CloneQuoteDetailList(CommonPaginationModel model)
        {
            return await _repository.CloneQuoteDetailList(model);
        }
        #endregion

        #region Post

        public async Task<string> SaveQuoteDetailData(QuoteDetailModel model)
        {
            return await _repository.SaveQuoteDetailData(model);
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteQuoteDetail(CommonIdModel model)
        {
            return await _repository.DeleteQuoteDetail(model);
        }
        #endregion


        #region QuoteVersioning Page

        public async Task<List<MultiitemCollectionModel>> GetQuoteVersionList(CommonPaginationModel model)
        {
            return await _repository.GetQuoteVersionList(model);
        }
        public async Task<List<ItemDetailModel>> GetQuoteItemDetailList(CommonPaginationModel model)
        {
            return await _repository.GetQuoteItemDetailList(model);
        }
        public async Task<List<ItemCollectionModel>> GetQuoteCollectionDetailList(CommonPaginationModel model)
        {
            return await _repository.GetQuoteCollectionDetailList(model);
        }
        public async Task<List<AdditionalInfoModel>> GetQuoteAdditionalInfoList(CommonPaginationModel model)
        {
            return await _repository.GetQuoteAdditionalInfoList(model);
        }
        #endregion

        #region QuoteCloneVersioning Page

        public async Task<List<MultiitemCollectionModel>> GetCloneQuoteVersionList(CommonPaginationModel model)
        {
            return await _repository.GetCloneQuoteVersionList(model);
        }
        public async Task<List<ItemDetailModel>> GetCloneQuoteItemDetailList(CommonPaginationModel model)
        {
            return await _repository.GetCloneQuoteItemDetailList(model);
        }
        public async Task<List<ItemCollectionModel>> GetCloneQuoteCollectionDetailList(CommonPaginationModel model)
        {
            return await _repository.GetCloneQuoteCollectionDetailList(model);
        }
        public async Task<List<AdditionalInfoModel>> GetCloneQuoteAdditionalInfoList(CommonPaginationModel model)
        {
            return await _repository.GetCloneQuoteAdditionalInfoList(model);
        }
        #endregion
    }
}
