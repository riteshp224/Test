using QuoteManagement.Data.DBRepository.ItemCategory;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.ItemCategory
{
    class ItemCategoryService : IItemCategoryService
    {
        #region Fields
        private readonly IItemCategoryRepository _repository;
        #endregion

        #region Construtor
        public ItemCategoryService(IItemCategoryRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<List<ItemCategoryMasterModel>> GetItemCategoryList()
        {
            return await _repository.GetItemCategoryList();
        }
        public async Task<ItemCategoryMasterModel> GetItemCategoryById(long ItemCategoryId)
        {
            return await _repository.GetItemCategoryById(ItemCategoryId);
        }
        #endregion

        #region Post

        public async Task<string> SaveItemCategoryData(ItemCategoryMasterModel model)
        {
            return await _repository.SaveItemCategoryData(model);
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteItemCategory(CommonIdModel model)
        {
            return await _repository.DeleteItemCategory(model);
        }
        #endregion
    }
}
