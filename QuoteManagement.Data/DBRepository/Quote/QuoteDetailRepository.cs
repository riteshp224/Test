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

namespace QuoteManagement.Data.DBRepository.Quote
{
    public class QuoteDetailRepository : BaseRepository, IQuoteDetailRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public QuoteDetailRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Get
        public async Task<List<QuoteDetailModel>> GetQuoteDetailList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                param.Add("@QuoteId", model.id);
                var data = await QueryAsync<QuoteDetailModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<QuoteDetailModel>> GetQuotecustomerDetailList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 3);
                param.Add("@QuoteId", model.id);
                var data = await QueryAsync<QuoteDetailModel>("SP_Customer_Quote", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<QuoteDetailModel>> CloneQuoteDetailList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@QuoteId", model.id);
                param.Add("@Type", 6);
                var data = await QueryAsync<QuoteDetailModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<QuoteDetailModel>> GetItemCollectionDetailList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 7);
                param.Add("@ItemCollectionId", model.id);
                var data = await QueryAsync<QuoteDetailModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<QuoteDetailModel> GetQuoteDetailById(long QuoteDetailId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@QuoteDetailId", QuoteDetailId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<QuoteDetailModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        #region QuoteVersioning Page
        public async Task<List<MultiitemCollectionModel>> GetQuoteVersionList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 8);
                param.Add("@QuoteId", model.id);
                var data = await QueryAsync<MultiitemCollectionModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ItemDetailModel>> GetQuoteItemDetailList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 9);
                param.Add("@QuoteVersionId", model.id);
                var data = await QueryAsync<ItemDetailModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ItemCollectionModel>> GetQuoteCollectionDetailList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 10);
                param.Add("@QuoteVersionId", model.id);
                var data = await QueryAsync<ItemCollectionModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<AdditionalInfoModel>> GetQuoteAdditionalInfoList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 11);
                param.Add("@QuoteVersionId", model.id);
                var data = await QueryAsync<AdditionalInfoModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region QuoteCloneVersioning Page
        public async Task<List<MultiitemCollectionModel>> GetCloneQuoteVersionList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 12);
                param.Add("@QuoteId", model.id);
                var data = await QueryAsync<MultiitemCollectionModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ItemDetailModel>> GetCloneQuoteItemDetailList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 13);
                param.Add("@QuoteVersionId", model.id);
                var data = await QueryAsync<ItemDetailModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ItemCollectionModel>> GetCloneQuoteCollectionDetailList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 14);
                param.Add("@QuoteVersionId", model.id);
                var data = await QueryAsync<ItemCollectionModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<AdditionalInfoModel>> GetCloneQuoteAdditionalInfoList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 15);
                param.Add("@QuoteVersionId", model.id);
                var data = await QueryAsync<AdditionalInfoModel>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Post
        public async Task<string> SaveQuoteDetailData(QuoteDetailModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@QuoteId", model.QuoteId);
                param.Add("@QuoteDetailId", model.QuoteDetailId);
                param.Add("@ItemId", model.ItemId);
                param.Add("@Cost", model.Cost);
                param.Add("@Qty", model.Qty);
                param.Add("@userId", model.LoggedInUserId);
                if (model.QuoteDetailId != 0)
                    param.Add("@Type", 4);
                else
                    param.Add("@Type", 3);
                return await QueryFirstOrDefaultAsync<string>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Delete
        public async Task<bool> DeleteQuoteDetail(CommonIdModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@QuoteDetailId", model.id);
                param.Add("@Type", 5);
                var result = await QueryFirstOrDefaultAsync<string>("SP_QuoteDetail_v1", param, commandType: CommandType.StoredProcedure);
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
