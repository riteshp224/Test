using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;

namespace QuoteManagement.Data.DBRepository.Item
{
    public class ItemRepository : BaseRepository, IItemRepository
    {
        #region Fields
        private IConfiguration _config;
        private readonly DataConfig _dataConfig;
        #endregion

        #region Constructor
        public ItemRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
            _dataConfig = dataConfig.Value;
        }
        #endregion
        
        #region Get
        public async Task<List<ItemMasterModel>> GetItemList()
        {
            try
            {
                 var param = new DynamicParameters();
                param.Add("@Type", 1);
                param.Add("@Path", _dataConfig.FilePath + "Items/");
                var data = await QueryAsync<ItemMasterModel>("SP_ItemMaster",param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ItemMasterModel>> GetItemListCollection()
        {
            try
            {
                var data = await QueryAsync<ItemMasterModel>("SP_ItemMasterCollection", commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ItemMasterModel> GetItemById(long ItemId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemId", ItemId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<ItemMasterModel>("SP_ItemMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Post
        public async Task<string> SaveItemData(ItemMasterModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemId", model.ItemId);
                param.Add("@ItemCategoryId", model.ItemCategoryId);
                param.Add("@UOMId", model.UOMId);
                param.Add("@ItemName", model.ItemName);
                param.Add("@Cost", model.Cost);
                param.Add("@Length", (!string.IsNullOrEmpty(model.Length) ? Convert.ToDecimal(model.Length) : 0));
                param.Add("@Height", (!string.IsNullOrEmpty(model.Height) ? Convert.ToDecimal(model.Height) : 0));
                param.Add("@Width", (!string.IsNullOrEmpty(model.Width) ? Convert.ToDecimal(model.Width) : 0));
                param.Add("@Dimension", model.Dimension);
                param.Add("@AvailableStock", model.AvailableStock);
                param.Add("@Description", model.Description); //(model.Description=="null"?null: model.Description));
                param.Add("@IsPopular", model.IsPopular);
                param.Add("@ItemPhoto", model.ItemPhoto);
                param.Add("@isActive", model.isActive);
                param.Add("@userId", model.LoggedInUserId);
                param.Add("@SundriesLine", (!string.IsNullOrEmpty(model.SundriesLine) ? Convert.ToDecimal(model.SundriesLine) : 0));
                param.Add("@Price", (!string.IsNullOrEmpty(model.Price) ? Convert.ToDecimal(model.Price) : 0));
                if (model.ItemId != 0)
                    param.Add("@Type", 4);
                else
                    param.Add("@Type", 3);
                return await QueryFirstOrDefaultAsync<string>("SP_ItemMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteItem(CommonIdModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemId", model.id);
                param.Add("@Type", 5);
                var result = await QueryFirstOrDefaultAsync<string>("SP_ItemMaster", param, commandType: CommandType.StoredProcedure);
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
