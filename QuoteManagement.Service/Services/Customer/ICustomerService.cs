using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Customer
{
    public interface ICustomerService
    {
        #region Get
        Task<List<CustomerMasterModel>> GetCustomerList(CommonPaginationModel model);
        Task<CustomerMasterModel> GetCustomerById(long CustomerId);
        #endregion

        #region Post
        Task<string> SaveCustomerData(CustomerMasterModel model);
        #endregion

        #region Delete
        Task<bool> DeleteCustomer(CommonIdModel model);
        #endregion
    }
}
