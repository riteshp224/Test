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
    public class ItemCollectionRepository: BaseRepository,IItemCollectionRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public ItemCollectionRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Get
        public async Task<List<ItemCollectionMasterModel>> GetItemCollectionList()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                var data = await QueryAsync<ItemCollectionMasterModel>("SP_ItemCollectionMaster", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ItemCollectionMasterModel> GetItemCollectionById(long ItemCollectionId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemCollectionId", ItemCollectionId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<ItemCollectionMasterModel>("SP_ItemCollectionMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Post
        public async Task<string> SaveItemCollectionData(ItemCollectionMasterModel model)
        {
            try
            {

                DataTable dtItemCollectionDetails = new DataTable("tbl_ItemCollectionDetail");
                dtItemCollectionDetails.Columns.Add("ItemCollectionDetailId");
                dtItemCollectionDetails.Columns.Add("ItemCollectionId");
                dtItemCollectionDetails.Columns.Add("ItemId");
                dtItemCollectionDetails.Columns.Add("isDelete");
                dtItemCollectionDetails.Columns.Add("Cost");
                dtItemCollectionDetails.Columns.Add("Quantity");

                if (model.itemCollectionDetails.Count > 0)
                {
                    foreach (var item in model.itemCollectionDetails)
                    {
                        DataRow dtRow = dtItemCollectionDetails.NewRow();
                        dtRow["ItemCollectionDetailId"] = item.ItemCollectionDetailId;
                        dtRow["ItemCollectionId"] = model.ItemCollectionId;
                        dtRow["ItemId"] = item.ItemId;
                        dtRow["isDelete"] = item.isDelete;
                        dtRow["Cost"] = item.Cost;
                        dtRow["Quantity"] = item.Quantity;
                        dtItemCollectionDetails.Rows.Add(dtRow);
                    }
                }

                var param = new DynamicParameters();
                param.Add("@ItemCollectionId", model.ItemCollectionId);
                param.Add("@ItemCollectionName", model.ItemCollectionName);
                param.Add("@Length",  (!string.IsNullOrEmpty(model.Length)?Convert.ToDecimal(model.Length):0));
                param.Add("@Width", (!string.IsNullOrEmpty(model.Width) ? Convert.ToDecimal(model.Width) : 0));
                param.Add("@Height", (!string.IsNullOrEmpty(model.Height) ? Convert.ToDecimal(model.Height) : 0));
                param.Add("@LabourDays", (!string.IsNullOrEmpty(model.LabourDays) ? Convert.ToDecimal(model.LabourDays) : 0));
                param.Add("@ItemPhoto", model.ItemPhoto);
                param.Add("@ItemCollectionNo", model.ItemCollectionNo);
                param.Add("@isActive", model.isActive);
                param.Add("@userId", model.LoggedInUserId);
                if (model.ItemCollectionId != 0)
                    param.Add("@Type", 4);
                else
                    param.Add("@Type", 3);
                param.Add("@itemCollectionDetail", dtItemCollectionDetails.AsTableValuedParameter("[dbo].[tbl_ItemCollectionDetail]"));
                return await QueryFirstOrDefaultAsync<string>("SP_ItemCollectionMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteItemCollection(CommonIdModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ItemCollectionId", model.id);
                param.Add("@Type", 5);
                var result = await QueryFirstOrDefaultAsync<string>("SP_ItemCollectionMaster", param, commandType: CommandType.StoredProcedure);
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
