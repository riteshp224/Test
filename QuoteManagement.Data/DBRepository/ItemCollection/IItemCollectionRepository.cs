using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Data.DBRepository.ItemCollection
{
   public interface IItemCollectionRepository
    {
        #region Get
        Task<List<ItemCollectionMasterModel>> GetItemCollectionList();
        Task<ItemCollectionMasterModel> GetItemCollectionById(long ItemCollectionId);
        
        #endregion

        #region Post
        Task<string> SaveItemCollectionData(ItemCollectionMasterModel model);
        #endregion

        #region Delete
        Task<bool> DeleteItemCollection(CommonIdModel model);
        #endregion
    }
}
