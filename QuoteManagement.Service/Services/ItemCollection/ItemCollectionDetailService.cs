using QuoteManagement.Data.DBRepository.ItemCollection;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.ItemCollection
{
    public class ItemCollectionDetailService : IItemCollectionDetailService
    {
        #region Fields
        private readonly IItemCollectionDetailRepository _repository;
        #endregion

        #region Construtor
        public ItemCollectionDetailService(IItemCollectionDetailRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<List<ItemCollectionDetailModel>> GetItemCollectionDetailList( long ItemCollectionId)
        {
            return await _repository.GetItemCollectionDetailList(ItemCollectionId);
        }
        public async Task<ItemCollectionDetailModel> GetItemCollectionDetailById(long ItemCollectionDetailId)
        {
            return await _repository.GetItemCollectionDetailById(ItemCollectionDetailId);
        }
        #endregion

        #region Post

        public async Task<string> SaveItemCollectionDetailData(ItemCollectionDetailModel model)
        {
            return await _repository.SaveItemCollectionDetailData(model);
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteItemCollectionDetail(CommonIdModel model)
        {
            return await _repository.DeleteItemCollectionDetail(model);
        }
        #endregion
    }
}
