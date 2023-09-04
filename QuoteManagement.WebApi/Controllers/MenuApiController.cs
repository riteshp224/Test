using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Common;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
using QuoteManagement.Service.Services.Menu;
using QuoteManagement.WebApi.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManagement.WebApi.Controllers
{
    [Route("api/menu")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuApiController : ControllerBase
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private IMenuService _menuService;
        private IConfiguration _config;
        private readonly CommonMessages _commonMessages;
        #endregion

        #region Constructor
        public MenuApiController(ILoggerManager logger, IMenuService menuService, IConfiguration config, IOptions<CommonMessages> commonMessages)
        {
            _logger = logger;
            _menuService = menuService;
            _config = config;
            _commonMessages = commonMessages.Value;
        }
        #endregion

        #region Post
        [HttpPost("SaveMenu")]
        public async Task<BaseApiResponse> SaveMenu([FromForm] MenuMasterModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _menuService.SaveMenuData(model);
                if (string.IsNullOrEmpty(result))
                {
                    response.Message = _commonMessages.Menu.SaveSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = _commonMessages.Menu.SaveError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("SaveMenu", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Get
        [HttpGet("GetMenuById/{menuId}")]
        public async Task<ApiPostResponse<MenuMasterModel>> GetMenuById(long MenuId)
        {
            ApiPostResponse<MenuMasterModel> response = new ApiPostResponse<MenuMasterModel>() { Data = new MenuMasterModel() };
            try
            {
                var data = await _menuService.GetMenuData(MenuId);
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("GetMenuById", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }


        [HttpGet("GetMenuList")]
        public async Task<ApiResponse<MenuMasterModel>> GetMenuList()
        {
            ApiResponse<MenuMasterModel> response = new ApiResponse<MenuMasterModel>() { Data = new List<MenuMasterModel>() };
            try
            {
                var data = await _menuService.GetMenuList();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("GetMenuList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        [HttpGet("GetParentMenuList")]
        public async Task<ApiResponse<MenuMasterModel>> GetParentMenuList()
        {
            ApiResponse<MenuMasterModel> response = new ApiResponse<MenuMasterModel>() { Data = new List<MenuMasterModel>() };
            try
            {
                var data = await _menuService.GetParentMenuList();
                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("GetParentMenuList", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteMenu")]
        public async Task<BaseApiResponse> DeleteMenu(MenuMasterModel model)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _menuService.DeleteMenu(model);
                if (result)
                {
                    response.Message = _commonMessages.Menu.DeleteSuccess;
                    response.Success = true;
                }
                else
                {
                    response.Message = _commonMessages.Menu.DeleteError;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {

                string st = _commonMessages.CreateCommonMessage("DeleteMenu", ex.ToString());
                _logger.Information(st.ToString());
                response.Success = false;
                response.Message = _commonMessages.Error;
            }
            return response;
        }
        #endregion
    }
}
