using System;
using System.Collections.Generic;
using QuoteManagement.Data.DBRepository.Login;
using QuoteManagement.Data.DBRepository.Menu;
using QuoteManagement.Data.DBRepository.Role;
using QuoteManagement.Data.DBRepository.RoleRights;
using QuoteManagement.Data.DBRepository.User;
using QuoteManagement.Data.DBRepository.ItemCategory;
using QuoteManagement.Data.DBRepository.Item;
using QuoteManagement.Data.DBRepository.UOM;
using QuoteManagement.Data.DBRepository.Customer;
using QuoteManagement.Data.DBRepository.ItemCollection;
using QuoteManagement.Data.DBRepository.Quote;
using QuoteManagement.Data.DBRepository.DashBoard;
using QuoteManagement.Data.DBRepository.Report;

namespace QuoteManagement.Data
{
    public static class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dataDictionary = new Dictionary<Type, Type>
            {
                { typeof(IUserRepository), typeof(UserRepository) },
                { typeof(IRoleRepository), typeof(RoleRepository) },
                { typeof(IRoleRightsRepository), typeof(RoleRightsRepository) },
                { typeof(IMenuRepository), typeof(MenuRepository) },
                { typeof(ILoginRepository), typeof(LoginRepository) },
                { typeof(IItemCategoryRepository), typeof(ItemCategoryRepository) },
                { typeof(IItemRepository), typeof(ItemRepository) },
                { typeof(IUOMRepository), typeof(UOMRepository) },
                { typeof(ICustomerRepository), typeof(CustomerRepository) },
                { typeof(IItemCollectionRepository), typeof(ItemCollectionRepository) },
                { typeof(IItemCollectionDetailRepository), typeof(ItemCollectionDetailRepository) },
                { typeof(IQuoteRepository), typeof(QuoteRepository) },
                { typeof(IQuoteDetailRepository), typeof(QuoteDetailRepository) },
                 { typeof(IDashBoardRepository), typeof(DashBoardRepository) },
                 { typeof(IReportRepository), typeof(ReportRepository) },

            };
            return dataDictionary;
        }
    }
}
