using QuoteManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuoteManagement.Service.Services.Login
{
    public interface ILoginService
    {
        #region Post
        Task<LoginModel> LoginUser(LoginModel model);
        Task<long> ValidateUserEmail(string email);
        Task<string> ResetForgotPassword(UserForgotPasswordModel model);
        #endregion
    }
}
