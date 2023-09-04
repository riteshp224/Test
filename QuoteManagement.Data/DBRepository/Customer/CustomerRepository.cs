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


namespace QuoteManagement.Data.DBRepository.Customer
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public CustomerRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Get
        public async Task<List<CustomerMasterModel>> GetCustomerList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                param.Add("@PageNumber", model.PageNumber);
                param.Add("@PageSize", model.PageSize);
                param.Add("@strSearch", model.StrSearch);
                var data = await QueryAsync<CustomerMasterModel>("SP_CustomerMaster", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CustomerMasterModel> GetCustomerById(long CustomerId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", CustomerId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<CustomerMasterModel>("SP_CustomerMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Post
        public async Task<string> SaveCustomerData(CustomerMasterModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", model.CustomerId);
                param.Add("@FirstName", model.firstName);
                param.Add("@LastName", model.lastName);
                param.Add("@Email", model.email);
                param.Add("@Phone", model.phone);
                param.Add("@Address1", model.Address1);
                param.Add("@Address2", model.Address2);
                param.Add("@CityTown", model.CityTown);
                param.Add("@PostCode", model.PostCode);
                param.Add("@LeadSource", model.LeadSource);
                param.Add("@IsActive", model.isActive);
                param.Add("@userId", model.LoggedInUserId);
                if (model.CustomerId != 0)
                    param.Add("@Type", 4);
                else
                    param.Add("@Type", 3);
                return await QueryFirstOrDefaultAsync<string>("SP_CustomerMaster", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Delete
        public async Task<bool> DeleteCustomer(CommonIdModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", model.id);
                param.Add("@Type", 5);
                var result = await QueryFirstOrDefaultAsync<string>("SP_CustomerMaster", param, commandType: CommandType.StoredProcedure);
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
