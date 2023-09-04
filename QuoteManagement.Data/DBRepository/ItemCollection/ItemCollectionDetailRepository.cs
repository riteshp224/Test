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

namespace QuoteManagement.Data.DBRepository.ItemCollection
{
    class ItemCollectionDetailRepository : BaseRepository, IItemCollectionDetailRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public ItemCollectionDetailRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Get
        public async Task<List<ItemCollectionDetailModel>> GetItemCollectionDetailList(long ItemCollectionId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                param.Add("@ItemCollectionId", ItemCollectionId);
                var data = await QueryAsync<ItemCollectionDetailModel>("SP_ItemCollectionDetail", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ItemCollectionDetailModel> GetItemCollectionDetailById(long ItemCollectionDetailId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemCollectionDetailId", ItemCollectionDetailId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<ItemCollectionDetailModel>("SP_ItemCollectionDetail", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Post
        public async Task<string> SaveItemCollectionDetailData(ItemCollectionDetailModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemCollectionId", model.ItemCollectionId);
                param.Add("@ItemCollectionDetailId", model.ItemCollectionDetailId);
                param.Add("@ItemId", model.ItemId);
                param.Add("@userId", model.LoggedInUserId);
                if (model.ItemCollectionDetailId != 0)
                    param.Add("@Type", 4);
                else
                    param.Add("@Type", 3);
                return await QueryFirstOrDefaultAsync<string>("SP_ItemCollectionDetail", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteItemCollectionDetail(CommonIdModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemCollectionDetailId", model.id);
                param.Add("@Type", 5);
                var result = await QueryFirstOrDefaultAsync<string>("SP_ItemCollectionDetail", param, commandType: CommandType.StoredProcedure);
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
