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

namespace QuoteManagement.Data.DBRepository.UOM
{
    public class UOMRepository : BaseRepository, IUOMRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion
        #region Constructor
        public UOMRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
        }
        #endregion
        #region Get
        public async Task<List<UOMMasterModel>> GetUOMList()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                var data = await QueryAsync<UOMMasterModel>("SP_UOMMaster", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UOMMasterModel> GetUOMById(long UOMId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UOMId", UOMId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<UOMMasterModel>("SP_UOMMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Post
        public async Task<string> SaveUOMData(UOMMasterModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UOMId", model.UOMId);
                param.Add("@UOMName", model.UOMName);
                param.Add("@isActive", model.isActive);
                param.Add("@userId", model.LoggedInUserId);
                if (model.UOMId != 0)
                    param.Add("@Type", 4);
                else
                    param.Add("@Type", 3);
                return await QueryFirstOrDefaultAsync<string>("SP_UOMMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteUOM(CommonIdModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UOMId", model.id);
                param.Add("@Type", 5);
                var result = await QueryFirstOrDefaultAsync<string>("SP_UOMMaster", param, commandType: CommandType.StoredProcedure);
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
