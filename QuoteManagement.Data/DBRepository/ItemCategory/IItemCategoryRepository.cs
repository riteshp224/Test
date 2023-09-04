using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Data.DBRepository.ItemCategory
{
    public interface IItemCategoryRepository
    {
        #region Get
        Task<List<ItemCategoryMasterModel>> GetItemCategoryList();
        Task<ItemCategoryMasterModel> GetItemCategoryById(long ItemCategoryId);
        #endregion


        #region Post
        Task<string> SaveItemCategoryData(ItemCategoryMasterModel model);
        #endregion

        #region Delete
        Task<bool> DeleteItemCategory(CommonIdModel model);
        #endregion
    }
}
