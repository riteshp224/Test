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

namespace QuoteManagement.Data.DBRepository.ItemCategory
{
    public class ItemCategoryRepository : BaseRepository, IItemCategoryRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion
        #region Constructor
        public ItemCategoryRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
        }
        #endregion
        #region Get
        public async Task<List<ItemCategoryMasterModel>> GetItemCategoryList()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                var data = await QueryAsync<ItemCategoryMasterModel>("SP_ItemCategoryMaster", param , commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ItemCategoryMasterModel> GetItemCategoryById(long ItemCategoryId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemCategoryId", ItemCategoryId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<ItemCategoryMasterModel>("SP_ItemCategoryMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
#endregion
        #region Post
        public async Task<string> SaveItemCategoryData(ItemCategoryMasterModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemCategoryId", model.ItemCategoryId);
                param.Add("@ItemCategoryName", model.ItemCategoryName);
                param.Add("@isActive", model.isActive);
                param.Add("@userId", model.LoggedInUserId);
                if(model.ItemCategoryId!=0)
                    param.Add("@Type", 4);
                else
                    param.Add("@Type", 3);
                return await QueryFirstOrDefaultAsync<string>("SP_ItemCategoryMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteItemCategory(CommonIdModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemCategoryId", model.id);
                param.Add("@Type", 5);
                var result = await QueryFirstOrDefaultAsync<string>("SP_ItemCategoryMaster", param, commandType: CommandType.StoredProcedure);
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
