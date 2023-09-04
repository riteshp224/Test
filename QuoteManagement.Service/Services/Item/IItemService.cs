using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Item
{
    public interface IItemService
    {
        #region GetCollection
        Task<List<ItemMasterModel>> GetItemList();
        Task<List<ItemMasterModel>> GetItemListCollection();
        Task<ItemMasterModel> GetItemById(long userId);
        #endregion

        #region Post
        Task<string> SaveItemData(ItemMasterModel model);
        //Task<string> SaveBoardingStep(UserMasterModel model);
        #endregion

        #region Delete
        Task<bool> DeleteItem(CommonIdModel model);
        #endregion
    }
}
