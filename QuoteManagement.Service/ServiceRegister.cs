using System;
using System.Collections.Generic;
using QuoteManagement.Service.Services.Login;
using QuoteManagement.Service.Services.Menu;
using QuoteManagement.Service.Services.Role;
using QuoteManagement.Service.Services.RoleRights;
using QuoteManagement.Service.Services.User;
using QuoteManagement.Service.Services.ItemCategory;
using QuoteManagement.Service.Services.Item;
using QuoteManagement.Service.Services.UOM;
using QuoteManagement.Service.Services.Customer;
using QuoteManagement.Service.Services.ItemCollection;
using QuoteManagement.Service.Services.Quote;
using QuoteManagement.Service.Services.DashBoard;
using QuoteManagement.Service.Services.Report;

namespace QuoteManagement.Service
{
    public static class ServiceRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var serviceDictonary = new Dictionary<Type, Type>
            {
                { typeof(IUserService), typeof(UserService) },
                { typeof(IRoleService), typeof(RoleService) },
                { typeof(IRoleRightsService), typeof(RoleRightsService) },
                { typeof(IMenuService), typeof(MenuService) },
                { typeof(ILoginService), typeof(LoginService) },
                { typeof(IItemCategoryService), typeof(ItemCategoryService) },
                { typeof(IItemService), typeof(ItemService) },
                { typeof(ICustomerService), typeof(CustomerService) },
                { typeof(IItemCollectionService), typeof(ItemCollectionService) },
                { typeof(IItemCollectionDetailService), typeof(ItemCollectionDetailService) },
                { typeof(IQuoteService), typeof(QuoteService) },
                { typeof(IQuoteDetailService), typeof(QuoteDetailService) },
                { typeof(IUOMService), typeof(UOMService) },
                { typeof(IDashBoardService), typeof(DashBoardService) },
                { typeof(IReportService), typeof(ReportService) },

            };
            return serviceDictonary;
        }
    }
}
