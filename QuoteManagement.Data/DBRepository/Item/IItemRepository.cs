using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Data.DBRepository.Item
{
    public interface IItemRepository
    {
        #region Get
        Task<List<ItemMasterModel>> GetItemList();
        Task<List<ItemMasterModel>> GetItemListCollection();
        Task<ItemMasterModel> GetItemById(long ItemId);
        #endregion

        #region Post
        Task<string> SaveItemData(ItemMasterModel model);
        //Task<string> SaveBoardingStep(ItemMasterModel model);
        #endregion

        #region Delete
        Task<bool> DeleteItem(CommonIdModel model);
        #endregion
    }
}
