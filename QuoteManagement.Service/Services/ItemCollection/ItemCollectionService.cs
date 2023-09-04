using QuoteManagement.Data.DBRepository.ItemCollection;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.ItemCollection
{
    class ItemCollectionService :IItemCollectionService
    {
        #region Fields
        private readonly IItemCollectionRepository _repository;
        #endregion

        #region Construtor
        public ItemCollectionService(IItemCollectionRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<List<ItemCollectionMasterModel>> GetItemCollectionList()
        {
            return await _repository.GetItemCollectionList();
        }
        public async Task<ItemCollectionMasterModel> GetItemCollectionById(long ItemCollectionId)
        {
            return await _repository.GetItemCollectionById(ItemCollectionId);
        }
       
        #endregion

        #region Post

        public async Task<string> SaveItemCollectionData(ItemCollectionMasterModel model)
        {
            return await _repository.SaveItemCollectionData(model);
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteItemCollection(CommonIdModel model)
        {
            return await _repository.DeleteItemCollection(model);
        }
        #endregion
    }
}
