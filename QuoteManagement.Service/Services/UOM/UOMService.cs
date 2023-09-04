using QuoteManagement.Data.DBRepository.UOM;
using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.UOM
{
    class UOMService : IUOMService
    {

        #region Fields
        private readonly IUOMRepository _repository;
        #endregion

        #region Construtor
        public UOMService(IUOMRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Get
        public async Task<List<UOMMasterModel>> GetUOMList()
        {
            return await _repository.GetUOMList();
        }
        public async Task<UOMMasterModel> GetUOMById(long UOMId)
        {
            return await _repository.GetUOMById(UOMId);
        }
        #endregion

        #region Post

        public async Task<string> SaveUOMData(UOMMasterModel model)
        {
            return await _repository.SaveUOMData(model);
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteUOM(CommonIdModel model)
        {
            return await _repository.DeleteUOM(model);
        }
        #endregion
    }
}
