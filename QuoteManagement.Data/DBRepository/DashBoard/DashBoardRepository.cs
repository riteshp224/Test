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

namespace QuoteManagement.Data.DBRepository.DashBoard
{
    public class DashBoardRepository : BaseRepository, IDashBoardRepository
    {
        #region Fields
        private IConfiguration _config;
        private readonly DataConfig _dataConfig;
        #endregion

        #region Constructor
        public DashBoardRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
            _dataConfig = dataConfig.Value;
        }
        #endregion

        #region Get
       
       
        public async Task<DashBoardModel> GetTotalCount()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                return await QueryFirstOrDefaultAsync<DashBoardModel>("SP_DashoBoard_Data", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Comparsionchart>> GetComparsionChartData()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 2);
                var data = await QueryAsync<Comparsionchart>("SP_DashoBoard_Data", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ItemMasterModel>> GetPopularItemData()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 3);
                param.Add("@Path", _dataConfig.FilePath + "Items/");
                var data = await QueryAsync<ItemMasterModel>("SP_DashoBoard_Data", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<QuoteModel>> GetOngoingProject()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 4);
                var data = await QueryAsync<QuoteModel>("SP_DashoBoard_Data", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<QuoteModel>> GetOngoingQuotes()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 5);
                var data = await QueryAsync<QuoteModel>("SP_DashoBoard_Data", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<QuoteDataModel>> GetQuoteData(QuoteDataModel quoteDataModel)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 6);
                param.Add("@Fromdate", quoteDataModel.FromDate.Date.ToString("yyyy-MM-dd"));
                param.Add("@Todate", quoteDataModel.ToDate.Date.ToString("yyyy-MM-dd"));
                var data = await QueryAsync<QuoteDataModel>("SP_DashoBoard_Data", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<MonthwiseQuoteDataModel>> GetMonthWiseQuoteData(Int64 QuoteId,DateTime FromDate,DateTime ToDate)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 7);
                param.Add("@QuoteId", QuoteId);
                param.Add("@Fromdate", FromDate.Date.ToString("yyyy-MM-dd"));
                param.Add("@Todate", ToDate.Date.ToString("yyyy-MM-dd"));
                var data = await QueryAsync<MonthwiseQuoteDataModel>("SP_DashoBoard_Data", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
