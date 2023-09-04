using QuoteManagement.Data.DBRepository.Menu;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Menu
{
    public class MenuService : IMenuService
    {
        #region Fields
        private readonly IMenuRepository _repository;
        #endregion

        #region Construtor
        public MenuService(IMenuRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<List<MenuMasterModel>> GetMenuList()
        {
            return await _repository.GetMenuList();
        }
        public async Task<List<MenuMasterModel>> GetParentMenuList()
        {
            return await _repository.GetParentMenuList();
        }
        public async Task<MenuMasterModel> GetMenuData(long MenuId)
        {
            return await _repository.GetMenuData(MenuId);
        }
        #endregion

        #region Post

        public async Task<string> SaveMenuData(MenuMasterModel model)
        {
            return await _repository.SaveMenuData(model);
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteMenu(MenuMasterModel model)
        {
            return await _repository.DeleteMenu(model);
        }
        #endregion
    }
}
