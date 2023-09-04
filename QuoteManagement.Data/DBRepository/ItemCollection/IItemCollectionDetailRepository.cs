using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Data.DBRepository.ItemCollection
{
  public  interface IItemCollectionDetailRepository
    {
        #region Get
        Task<List<ItemCollectionDetailModel>> GetItemCollectionDetailList( long ItemCollectionId);
        Task<ItemCollectionDetailModel> GetItemCollectionDetailById(long ItemCollectionDetailId);
        #endregion

        #region Post
        Task<string> SaveItemCollectionDetailData(ItemCollectionDetailModel model);
        #endregion

        #region Delete
        Task<bool> DeleteItemCollectionDetail(CommonIdModel model);
        #endregion
    }
}
