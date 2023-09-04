using QuoteManagement.Data.DBRepository.Item;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Item
{
    public class ItemService : IItemService
    {
        #region Fields
        private readonly IItemRepository _repository;
        #endregion

        #region Construtor
        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<List<ItemMasterModel>> GetItemList()
        {
            return await _repository.GetItemList();
        }
        public async Task<List<ItemMasterModel>> GetItemListCollection()
        {
            return await _repository.GetItemListCollection();
        }
        public async Task<ItemMasterModel> GetItemById(long ItemId)
        {
            return await _repository.GetItemById(ItemId);
        }
        #endregion

        #region Post

        public async Task<string> SaveItemData(ItemMasterModel model)
        {
            return await _repository.SaveItemData(model);
        }

        //public async Task<string> SaveBoardingStep(ItemMasterModel model)
        //{
        //    return await _repository.SaveBoardingStep(model);
        //}
        #endregion

        #region Delete
        public async Task<bool> DeleteItem(CommonIdModel model)
        {
            return await _repository.DeleteItem(model);
        }

    
        #endregion
    }
}
