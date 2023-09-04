using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Data.DBRepository.Menu
{
    public interface IMenuRepository
    {
        #region Get
        Task<List<MenuMasterModel>> GetMenuList();
        Task<List<MenuMasterModel>> GetParentMenuList();
        Task<MenuMasterModel> GetMenuData(long MenuId);
        #endregion

        #region Post
        Task<string> SaveMenuData(MenuMasterModel model);
        #endregion

        #region Delete
        Task<bool> DeleteMenu(MenuMasterModel model);
        #endregion
    }
}
