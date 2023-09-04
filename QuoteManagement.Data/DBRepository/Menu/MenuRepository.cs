using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Data.DBRepository.Menu
{
    public class MenuRepository : BaseRepository, IMenuRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public MenuRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Get
        public async Task<List<MenuMasterModel>> GetMenuList()
        {
            try
            {
                var param = new DynamicParameters();
                //param.Add("@menuName", menuName);
                param.Add("@Type", 1);
                var data = await QueryAsync<MenuMasterModel>("SP_MenuMaster", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<MenuMasterModel>> GetParentMenuList()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 6);
                var data = await QueryAsync<MenuMasterModel>("SP_MenuMaster", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<MenuMasterModel> GetMenuData(long MenuId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MenuId", MenuId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<MenuMasterModel>("SP_MenuMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Post
        public async Task<string> SaveMenuData(MenuMasterModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@menuId", model.menuId);
                param.Add("@parentmenuId", model.Parentmenuid);
                param.Add("@menuName", model.menuName);
                param.Add("@MenuURL", model.menuUrl);
                param.Add("@isActive", model.isActive);
                param.Add("@userId", model.userId);
                if (model.menuId != 0)
                    param.Add("@Type", 4);
                else
                    param.Add("@Type", 3);
                return await QueryFirstOrDefaultAsync<string>("SP_MenuMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteMenu(MenuMasterModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@menuId", model.menuId);
                param.Add("@Type", 5);
                var result = await QueryFirstOrDefaultAsync<string>("SP_MenuMaster", param, commandType: CommandType.StoredProcedure);
                if (string.IsNullOrEmpty(result))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        #endregion
    }
}
