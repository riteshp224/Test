using QuoteManagement.Data.DBRepository.Customer;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        #region Fields
        private readonly ICustomerRepository _repository;
        #endregion

        #region Construtor
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<List<CustomerMasterModel>> GetCustomerList(CommonPaginationModel model)
        {
            return await _repository.GetCustomerList(model);
        }
        public async Task<CustomerMasterModel> GetCustomerById(long CustomerId)
        {
            return await _repository.GetCustomerById(CustomerId);
        }
        #endregion

        #region Post

        public async Task<string> SaveCustomerData(CustomerMasterModel model)
        {
            return await _repository.SaveCustomerData(model);
        }

        //public async Task<string> SaveBoardingStep(CustomerMasterModel model)
        //{
        //    return await _repository.SaveBoardingStep(model);
        //}
        #endregion

        #region Delete
        public async Task<bool> DeleteCustomer(CommonIdModel model)
        {
            return await _repository.DeleteCustomer(model);
        }
        #endregion
    }
}
