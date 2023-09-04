using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Data.DBRepository.Customer
{
   public interface ICustomerRepository
    {
        #region Get
        Task<List<CustomerMasterModel>> GetCustomerList(CommonPaginationModel model);
        Task<CustomerMasterModel> GetCustomerById(long CustomerId);
        #endregion

        #region Post
        Task<string> SaveCustomerData(CustomerMasterModel model);
        //Task<string> SaveBoardingStep(CustomerMasterModel model);
        #endregion

        #region Delete
        Task<bool> DeleteCustomer(CommonIdModel model);
        #endregion
    }
}
