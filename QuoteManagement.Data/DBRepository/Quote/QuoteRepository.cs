using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuoteManagement.Data.DBRepository.Quote;
using QuoteManagement.Model;
using QuoteManagement.Model.Models;
namespace QuoteManagement.Data.DBRepository.Quote
{
    public class QuoteRepository : BaseRepository, IQuoteRepository
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public QuoteRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
        }
        #endregion

        #region Get
        public async Task<List<QuoteModel>> GetQuoteList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                var data = await QueryAsync<QuoteModel>("SP_Quote_v2", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<QuoteModel>> GetClosedQuoteList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 9);
                var data = await QueryAsync<QuoteModel>("SP_Quote_v2", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<QuoteModel>> GetCustomerQuoteList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                param.Add("@userId", model.LoggedInUserId);
                var data = await QueryAsync<QuoteModel>("SP_Customer_Quote", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<string> AddVersion(QuoteModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@QuoteId", model.QuoteId);
                param.Add("@userId", model.LoggedInUserId);
                param.Add("@Type", 7);
                return await QueryFirstOrDefaultAsync<string>("SP_Quote_v2", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> SaveSettingData(SettingModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@LaborRate", model.LaborRate);
                param.Add("@MarkUp1", model.MarkUp1);
                param.Add("@MarkUp2", model.MarkUp2);
                param.Add("@userId", model.LoggedInUserId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<string>("SP_GetSetting", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<QuoteModel> GetQuoteById(long QuoteId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@QuoteId", QuoteId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<QuoteModel>("SP_Quote_v2", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SettingModel> GetSetting(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@userId", model.LoggedInUserId);
                param.Add("@Type", 1);
                return await QueryFirstOrDefaultAsync<SettingModel>("SP_GetSetting", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
          public async Task<QuoteModel> GetQuotecustomerById(long QuoteId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@QuoteId", QuoteId);
                param.Add("@Type", 2);
                return await QueryFirstOrDefaultAsync<QuoteModel>("SP_Customer_Quote", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<QuoteCustomerDetail>> GetCustomerList()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);

                var data = await QueryAsync<QuoteCustomerDetail>("SP_CustomerDetail", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<StatusDetail>> GetStatusList()
        {
            try
            {
                var param = new DynamicParameters();
                var data = await QueryAsync<StatusDetail>("SP_StatusDetails", commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<JoinerDetail>> getJoinersList()
        {
            try
            {
                var param = new DynamicParameters();
                var data = await QueryAsync<JoinerDetail>("SP_JoinersDetails", commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<MultiitemCollectionModel>> getQuoteVersionList(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                param.Add("@QuoteId", model.id);
                var data = await QueryAsync<MultiitemCollectionModel>("SP_QuoteVersionDetail", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ItemCollectionMasterModel>> GetItemCollectionList()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 1);
                var data = await QueryAsync<ItemCollectionMasterModel>("SP_ItemCollectionMaster", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<QuoteCustomerDetail> GetCustomerDetailById(long CustomerId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", CustomerId);
                param.Add("@Type", 0);
                return await QueryFirstOrDefaultAsync<QuoteCustomerDetail>("SP_CustomerDetail", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<List<QuoteModel>> GetQuoteById(long QuoteId)
        //{
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("@QuoteId", QuoteId);
        //        param.Add("@Type", 2);

        //        using (SqlConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
        //        {
        //            await con.OpenAsync();
        //            var data = await con.QueryMultipleAsync("SP_Quote_v1", param, commandType: CommandType.StoredProcedure);
        //            List<QuoteModel> DetailsList = null;
        //            if (data != null)
        //            {
        //                Task<IEnumerable<QuoteModel>> result1 = data.ReadAsync<QuoteModel>();
        //                Task<IEnumerable<QuoteModel>> result2 = data.ReadAsync<QuoteModel>();
        //                foreach (var item in result1.Result)
        //                {
        //                    var obj = new QuoteModel();
        //                    obj.quoteDetails = new List<QuoteDetailModel>();
        //                    foreach (var val in result2.Result)
        //                    {
        //                        //QuoteDetailModel quoteDetail=new QuoteDetailModel();
        //                        obj.quoteDetails.Add(new QuoteDetailModel()
        //                        {
        //                            QuoteDetailId = Convert.ToInt64(((IDictionary<string, object>)val)["QuoteDetailId"]),
        //                            QuoteId = Convert.ToInt64(((IDictionary<string, object>)val)["QuoteId"]),
        //                            ItemId = Convert.ToInt64(((IDictionary<string, object>)val)["ItemId"]),
        //                            ItemCategoryId = Convert.ToInt64(((IDictionary<string, object>)val)["ItemCategoryId"]),
        //                            ItemName = Convert.ToString(((IDictionary<string, object>)val)["ItemName"]),
        //                            ItemCategoryName = Convert.ToString(((IDictionary<string, object>)val)["ItemCategoryName"]),
        //                            UOM = Convert.ToString(((IDictionary<string, object>)val)["UOM"]),
        //                            Dimension = Convert.ToDecimal(((IDictionary<string, object>)val)["Dimension"]),
        //                            Cost = Convert.ToDecimal(((IDictionary<string, object>)val)["Cost"]),
        //                            Qty = Convert.ToDecimal(((IDictionary<string, object>)val)["Qty"]),
        //                            isDelete = Convert.ToBoolean(((IDictionary<string, object>)val)["isDelete"]),
        //                        });

        //                        obj.QuoteId = Convert.ToInt64(((IDictionary<string, object>)item)["QuoteId"]);
        //                        obj.QuoteName = Convert.ToString(((IDictionary<string, object>)item)["[QuoteName]"]);
        //                        obj.CustomerName = Convert.ToString(((IDictionary<string, object>)item)["[CustomerName]"]);
        //                        obj.Description = Convert.ToString(((IDictionary<string, object>)item)["[Description]"]);
        //                        obj.ItemCollectionId = Convert.ToInt64(((IDictionary<string, object>)item)["[ItemCollectionId]"]);
        //                        obj.Address = Convert.ToString(((IDictionary<string, object>)item)["[Address]"]);
        //                        obj.isActive = Convert.ToBoolean(((IDictionary<string, object>)item)["[isActive]"]);
        //                        obj.isDelete = Convert.ToBoolean(((IDictionary<string, object>)item)["[isDelete]"]);
        //                        DetailsList.Add(obj);
        //                    }
        //                }

        //            }
        //            return DetailsList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task<QuoteModel> CloneQuote(long QuoteId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@QuoteId", QuoteId);
                param.Add("@Type", 6);
                return await QueryFirstOrDefaultAsync<QuoteModel>("SP_Quote_v2", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Post 
        public async Task<string> SaveQuoteData(QuoteModel model)
        {
            try
            {

                DataTable dtQuoteVersions = new DataTable("tbl_QuoteVersion");
                dtQuoteVersions.Columns.Add("QuoteVersionId");
                dtQuoteVersions.Columns.Add("QuoteId");
                dtQuoteVersions.Columns.Add("VersionNo");
                dtQuoteVersions.Columns.Add("totalAmt");
                dtQuoteVersions.Columns.Add("markupper");
                dtQuoteVersions.Columns.Add("makrupval");
                dtQuoteVersions.Columns.Add("labourDays");
                dtQuoteVersions.Columns.Add("ActullabourDays");
                dtQuoteVersions.Columns.Add("labourRate");
                dtQuoteVersions.Columns.Add("labourCost");
                dtQuoteVersions.Columns.Add("markupper1");
                dtQuoteVersions.Columns.Add("makrupval1");
                dtQuoteVersions.Columns.Add("vat");
                dtQuoteVersions.Columns.Add("netTotal");

                dtQuoteVersions.Columns.Add("actualTotalAmt");
                dtQuoteVersions.Columns.Add("actualMakrupval");
                dtQuoteVersions.Columns.Add("actuaLabourCost");
                dtQuoteVersions.Columns.Add("actualMakrupval1");
                dtQuoteVersions.Columns.Add("actualvat");
                dtQuoteVersions.Columns.Add("actualnetTotal");



                dtQuoteVersions.Columns.Add("isDelete");

                DataTable dtQuoteCollectionDetails = new DataTable("tbl_QuoteCollectionDetail");
                dtQuoteCollectionDetails.Columns.Add("QuoteCollectionDetailId");
                dtQuoteCollectionDetails.Columns.Add("QuoteId");
                dtQuoteCollectionDetails.Columns.Add("CollectionId");
                dtQuoteCollectionDetails.Columns.Add("QuoteVersionId");
                dtQuoteCollectionDetails.Columns.Add("Cost");
                dtQuoteCollectionDetails.Columns.Add("ActualCost");
                dtQuoteCollectionDetails.Columns.Add("isDelete");

               

                DataTable dtQuoteDetails = new DataTable("tbl_QuoteDetail_v2");
                dtQuoteDetails.Columns.Add("QuoteDetailId");
                dtQuoteDetails.Columns.Add("QuoteId");
                dtQuoteDetails.Columns.Add("ItemId");
                dtQuoteDetails.Columns.Add("QuoteVersionId");
                dtQuoteDetails.Columns.Add("Cost");
                dtQuoteDetails.Columns.Add("Qty");
                dtQuoteDetails.Columns.Add("CollectionId");
                dtQuoteDetails.Columns.Add("ActualCost");
                dtQuoteDetails.Columns.Add("ActualQty");
                dtQuoteDetails.Columns.Add("isDelete");

                DataTable dtQuoteAddInfoDetails = new DataTable("tbl_QuoteAddInfoDetail");
                dtQuoteAddInfoDetails.Columns.Add("QuoteAddInfoDetailId");
                dtQuoteAddInfoDetails.Columns.Add("QuoteId");
                dtQuoteAddInfoDetails.Columns.Add("QuoteVersionId");
                dtQuoteAddInfoDetails.Columns.Add("Details");
                dtQuoteAddInfoDetails.Columns.Add("Description");
                dtQuoteAddInfoDetails.Columns.Add("Qty");
                dtQuoteAddInfoDetails.Columns.Add("Cost");
                dtQuoteAddInfoDetails.Columns.Add("ActualQty");
                dtQuoteAddInfoDetails.Columns.Add("ActualCost");
                dtQuoteAddInfoDetails.Columns.Add("isDelete");


                if (model.multiitemCollection.Count > 0)
                {
                    foreach (var item in model.multiitemCollection)
                    {
                        DataRow dtRow = dtQuoteVersions.NewRow();
                        dtRow["QuoteVersionId"] = item.QuoteVersionId;
                        dtRow["QuoteId"] = item.QuoteId;
                        dtRow["VersionNo"] = item.VersionNo;
                        dtRow["totalAmt"] = item.totalAmt;
                        dtRow["markupper"] = item.markupper;
                        dtRow["makrupval"] = item.makrupval;
                        dtRow["labourDays"] = item.labourDays;
                        dtRow["ActullabourDays"] = item.ActullabourDays;
                        dtRow["labourRate"] = item.labourRate;
                        dtRow["labourCost"] = item.labourCost;
                        dtRow["markupper1"] = item.markupper1;
                        dtRow["makrupval1"] = item.makrupval1;
                        dtRow["vat"] = item.vat;
                        dtRow["netTotal"] = item.netTotal;

                        dtRow["actualTotalAmt"] = item.actualTotalAmt;
                        dtRow["actualMakrupval"] = item.actualMakrupval;
                        dtRow["actuaLabourCost"] = item.actuaLabourCost;
                        dtRow["actualMakrupval1"] = item.actualMakrupval1;
                        dtRow["actualvat"] = item.actualvat;
                        dtRow["actualnetTotal"] = item.actualnetTotal;



                        foreach (var Collection in item.itemCollection)
                        {
                            if (Collection.collectionId != 0)
                            {
                                DataRow dtQuoteCollectionDetail = dtQuoteCollectionDetails.NewRow();

                                dtQuoteCollectionDetail["QuoteCollectionDetailId"] = Collection.QuoteCollectionDetailId;
                                dtQuoteCollectionDetail["QuoteId"] = item.QuoteId;
                                dtQuoteCollectionDetail["CollectionId"] = Collection.collectionId;
                                dtQuoteCollectionDetail["QuoteVersionId"] = Collection.QuoteVersionId;
                                dtQuoteCollectionDetail["Cost"] = Collection.quoteCost;
                                dtQuoteCollectionDetail["ActualCost"] = Collection.actualCost;
                                dtQuoteCollectionDetail["isDelete"] = 0;
                                dtQuoteCollectionDetails.Rows.Add(dtQuoteCollectionDetail);
                            }

                        }

                        foreach (var detail in item.itemDetail)
                        {
                            if (detail.itemId != 0)
                            {
                                DataRow dtQuoteDetail = dtQuoteDetails.NewRow();

                                dtQuoteDetail["QuoteDetailId"] = detail.QuoteDetailId;
                                dtQuoteDetail["QuoteId"] = item.QuoteId;
                                dtQuoteDetail["ItemId"] = detail.itemId;
                                dtQuoteDetail["QuoteVersionId"] = detail.QuoteVersionId;
                                dtQuoteDetail["Cost"] = detail.quotedCost;
                                dtQuoteDetail["Qty"] = detail.quotedQty;
                                dtQuoteDetail["CollectionId"] = detail.CollectionId;
                                dtQuoteDetail["ActualCost"] = detail.actualCost;
                                dtQuoteDetail["ActualQty"] = detail.actualQty;
                                dtQuoteDetail["isDelete"] = 0;

                                dtQuoteDetails.Rows.Add(dtQuoteDetail);
                            }
                        }

                        foreach (var AddInfo in item.additionalInfo)
                        {
                            if (AddInfo.details != null && AddInfo.details != "")
                            {
                                DataRow dtQuoteAddInfoDetail = dtQuoteAddInfoDetails.NewRow();

                                dtQuoteAddInfoDetail["QuoteAddInfoDetailId"] = AddInfo.QuoteAddInfoDetailId;
                                dtQuoteAddInfoDetail["QuoteId"] = item.QuoteId;
                                dtQuoteAddInfoDetail["QuoteVersionId"] = AddInfo.QuoteVersionId;
                                dtQuoteAddInfoDetail["Details"] = AddInfo.details;
                                dtQuoteAddInfoDetail["Description"] = AddInfo.description;
                                dtQuoteAddInfoDetail["Qty"] = AddInfo.quotedQty;
                                dtQuoteAddInfoDetail["Cost"] = AddInfo.cost;
                                dtQuoteAddInfoDetail["ActualQty"] = AddInfo.actualQty;
                                dtQuoteAddInfoDetail["ActualCost"] = AddInfo.actualCost;
                                dtQuoteAddInfoDetail["isDelete"] = 0;

                                dtQuoteAddInfoDetails.Rows.Add(dtQuoteAddInfoDetail);
                            }
                        }


                        dtQuoteVersions.Rows.Add(dtRow);
                    }
                }


                var param = new DynamicParameters();
                param.Add("@QuoteId", model.QuoteId);
                param.Add("@CustomerId", model.CustomerId);
                param.Add("@QuoteName", model.QuoteName);
                param.Add("@Description", model.Description);
                param.Add("@Address", model.SiteAddress1);
                param.Add("@StatusId", model.QuoteStatusId);
                param.Add("@JoinerId", model.JoinerId);
                param.Add("@isActive", model.isActive);
                param.Add("@userId", model.LoggedInUserId);
                param.Add("@Address2", model.SiteAddress2);
                param.Add("@City", model.City);
                param.Add("@PostCode", model.PostCode);
                param.Add("@QuoteNo", model.QuoteNo);
                param.Add("@NetTotal", model.NetTotal);
                param.Add("@QuoteVersionId", model.QuoteVersionId);
                param.Add("@CreateNewCollection", model.CreateNewCollection);

                param.Add("@IsPlanSupplied", model.IsPlanSupplied);
                param.Add("@JoineryInformation", model.JoineryInformation);
                param.Add("@DoorThickness", model.DoorThickness);
                param.Add("@CabinetThickness", model.CabinetThickness);
                param.Add("@ShelfThickness", model.ShelfThickness);
                param.Add("@OtherInformation", model.OtherInformation);
                param.Add("@Plinth", model.Plinth);
                param.Add("@Fillers", model.Fillers);
                param.Add("@Handles", model.Handles);
                param.Add("@KnobsOrHandles", model.KnobsOrHandles);
                param.Add("@WhatIsTheSpec", model.WhatIsTheSpec);
                param.Add("@TypeOfHinges", model.TypeOfHinges);
                param.Add("@ShadowGap", model.ShadowGap);
                param.Add("@Paintfinish", model.Paintfinish);
                param.Add("@Painter", model.Painter);
                param.Add("@Spray", model.Spray);
                param.Add("@ColourInside", model.ColourInside);
                param.Add("@ColourOutside", model.ColourOutside);
                param.Add("@Lighting", model.Lighting);
                param.Add("@SpecNeedToOrder", model.SpecNeedToOrder);
                param.Add("@IsNeedPrePainting", model.IsNeedPrePainting);
                param.Add("@IsElectricianRequired", model.IsElectricianRequired);
                param.Add("@DetailRequirements", model.DetailRequirements);
                param.Add("@IsMirrors", model.IsMirrors);
                param.Add("@MirrorSize", model.MirrorSize);
                param.Add("@IsGlassShelves", model.IsGlassShelves);
                param.Add("@GlassShelveSize", model.GlassShelveSize);
                param.Add("@OtherItems", model.OtherItems);
                param.Add("@DateOfFinalMeasurement", model.DateOfFinalMeasurement);
                
                param.Add("@quoteVersionDetail", dtQuoteVersions.AsTableValuedParameter("[dbo].[tbl_QuoteVersion]"));
                param.Add("@quoteDetail", dtQuoteDetails.AsTableValuedParameter("[dbo].[tbl_QuoteDetail_V2]"));
                param.Add("@quoteAddInfoDetail", dtQuoteAddInfoDetails.AsTableValuedParameter("[dbo].[tbl_QuoteAddInfoDetail]"));
                param.Add("@quoteCollectionDetail", dtQuoteCollectionDetails.AsTableValuedParameter("[dbo].[tbl_QuoteCollectionDetail]"));
                if (model.QuoteId != 0)
                    param.Add("@Type", 4);
                else
                    param.Add("@Type", 3);
                return await QueryFirstOrDefaultAsync<string>("SP_Quote_v2", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<string> SaveCustomerQuoteData(QuoteModel model)
        {
            try
            {

                DataTable QuoteItemDetail = new DataTable("tbl_QuoteItemDetail");
                QuoteItemDetail.Columns.Add("QuoteId");
                QuoteItemDetail.Columns.Add("QuoteDetailId");
                QuoteItemDetail.Columns.Add("QuoteVersionId");
                QuoteItemDetail.Columns.Add("ItemId");
                QuoteItemDetail.Columns.Add("ActualQty");
                QuoteItemDetail.Columns.Add("Qty");
                QuoteItemDetail.Columns.Add("ActualCost");
                QuoteItemDetail.Columns.Add("Cost"); 
                QuoteItemDetail.Columns.Add("CollectionId"); 


                 DataTable dtAdditionalInfo = new DataTable("tbl_AdditionalInfo");
                dtAdditionalInfo.Columns.Add("QuoteAddInfoDetailId");
                dtAdditionalInfo.Columns.Add("QuoteId");
                dtAdditionalInfo.Columns.Add("Details");
                dtAdditionalInfo.Columns.Add("QuoteVersionId");
                dtAdditionalInfo.Columns.Add("ActualQty");
                dtAdditionalInfo.Columns.Add("Description");
                dtAdditionalInfo.Columns.Add("cost");
                dtAdditionalInfo.Columns.Add("Qty");
                dtAdditionalInfo.Columns.Add("ActualCost");

                if (model.quoteDetails.Count > 0)
                {
                    foreach (var item in model.quoteDetails)
                    {
                        DataRow dtRow = QuoteItemDetail.NewRow();
                        dtRow["QuoteId"] = model.QuoteId;
                        dtRow["QuoteDetailId"] = item.QuoteDetailId;
                        dtRow["QuoteVersionId"] = model.QuoteVersionId;
                        dtRow["ItemId"] = item.itemId;
                        dtRow["ActualQty"] = item.actualQty;
                        dtRow["Qty"] = item.quotedQty;
                        dtRow["ActualCost"] = item.actualCost;
                        dtRow["Cost"] = item.quotedCost;
                        dtRow["CollectionId"] = item.CollectionId;

                        QuoteItemDetail.Rows.Add(dtRow);
                    }
                }

                if (model.AdditionalInfo.Count > 0)
                {
                    foreach (var item in model.AdditionalInfo)
                    {
                        if (item.details != null && item.details != "")
                        {
                            DataRow dtAddInfoDetail = dtAdditionalInfo.NewRow();

                            dtAddInfoDetail["QuoteAddInfoDetailId"] = item.QuoteAddInfoDetailId;
                            dtAddInfoDetail["QuoteId"] = model.QuoteId;
                            dtAddInfoDetail["Details"] = item.details;
                            dtAddInfoDetail["QuoteVersionId"] = model.QuoteVersionId;
                            dtAddInfoDetail["ActualQty"] = item.actualQty;
                            dtAddInfoDetail["Description"] = item.description;
                            dtAddInfoDetail["cost"] = item.cost;
                            dtAddInfoDetail["Qty"] = item.Qty;
                            dtAddInfoDetail["ActualCost"] = item.actualCost;
                            dtAdditionalInfo.Rows.Add(dtAddInfoDetail);
                        }
                    }

                }
            


                var param = new DynamicParameters();
                param.Add("@QuoteId", model.QuoteId);
                param.Add("@QuoteName", model.QuoteName);
                param.Add("@QuoteNo", model.QuoteNo);
                param.Add("@QuoteVersionId", model.QuoteVersionId);
                param.Add("@Description", model.Description);
                param.Add("@ActullabourDays", model.ActullabourDays);
                param.Add("@JoinerId", model.JoinerId);
                param.Add("@labourDays", model.labourDays);
                param.Add("@userId", model.LoggedInUserId);
                //param.Add("@JoinerId", model.JoinerId);

                param.Add("@QuoteItemDetail", QuoteItemDetail.AsTableValuedParameter("[dbo].[tbl_QuoteItemDetail]"));
                param.Add("@QuoteAdditionalInfo", dtAdditionalInfo.AsTableValuedParameter("[dbo].[tbl_AdditionalInfo]"));
                if (model.QuoteId != 0)
                    param.Add("@Type", 4);
             
                return await QueryFirstOrDefaultAsync<string>("SP_Customer_Quote", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteQuote(CommonIdModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@QuoteId", model.id);
                param.Add("@Type", 5);
                var result = await QueryFirstOrDefaultAsync<string>("SP_Quote_v2", param, commandType: CommandType.StoredProcedure);
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
        public async Task<List<AdditionalInfoModel>> GetAdditionalInfoQuote(CommonPaginationModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 7);
                param.Add("@QuoteId", model.id);
                var data = await QueryAsync<AdditionalInfoModel>("SP_Customer_Quote", param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
