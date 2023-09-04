using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.User
{
    public interface IUserService
    {
        #region Get
        Task<List<UserMasterModel>> GetUserList();
        Task<UserMasterModel> GetUserById(long userId);
        #endregion

        #region Post
        Task<string> SaveUserData(UserMasterModel model);
        //Task<string> SaveBoardingStep(UserMasterModel model);
        #endregion

        #region Delete
        Task<bool> DeleteUser(CommonIdModel model);
        #endregion
    }
}
