using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Data.DBRepository.UOM
{
    public interface IUOMRepository
    {
        #region Get
        Task<List<UOMMasterModel>> GetUOMList();
        Task<UOMMasterModel> GetUOMById(long UOMId);
        #endregion


        #region Post
        Task<string> SaveUOMData(UOMMasterModel model);
        #endregion

        #region Delete
        Task<bool> DeleteUOM(CommonIdModel model);
        #endregion
    }
}
