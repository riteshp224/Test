using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteManagement.Model.Models
{
    public class QuoteModel : CommonModel
    {
        public long QuoteId { get; set; }
        public string QuoteName { get; set; }
        public string QuoteDate { get; set; }
        public string QuoteNo { get; set; }
        public long CustomerId { get; set; }
        public long ItemCollectionId { get; set; }
        public string ItemCollectionName { get; set; }
        public string CustomerName { get; set; }
        public string SiteAddress1 { get; set; }
        public string SiteAddress2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public decimal? NetTotal { get; set; }
        public long QuoteVersionId { get; set; }
        public string OldQuoteStatusId { get; set; }
        public string Description { get; set; }
        public string QuoteStatus { get; set; }
        public string QuoteStatusId { get; set; }
        public long JoinerId { get; set; }
        public bool statusId { get; set; }
        public string Joiners { get; set; }
        public string JoinerEmailId { get; set; }
        public decimal? labourDays { get; set; }
        public decimal? ActullabourDays { get; set; }
        public int? JoinerUpdateStatus { get; set; }
        public string JoinerUpdateOn { get; set; }
        public int CreateNewCollection { get; set; }
        public List<MultiitemCollectionModel> multiitemCollection { get; set; }
        public List<ItemDetailModel> quoteDetails { get; set; }
        public List<AdditionalInfoModel> AdditionalInfo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal? Total { get; set; }


        public bool IsPlanSupplied { get; set; }
        public string JoineryInformation { get; set; }
        public string DoorThickness { get; set; }
        public string CabinetThickness { get; set; }
        public string ShelfThickness { get; set; }
        public string OtherInformation { get; set; }
        public string Plinth { get; set; }
        public string Fillers { get; set; }
        public string Handles { get; set; }
        public string KnobsOrHandles { get; set; }
        public string WhatIsTheSpec { get; set; }
        public string TypeOfHinges { get; set; }
        public string ShadowGap { get; set; }
        public string Paintfinish { get; set; }
        public string Painter { get; set; }
        public string Spray { get; set; }
        public string ColourInside { get; set; }
        public string ColourOutside { get; set; }
        public string Lighting { get; set; }
        public string SpecNeedToOrder { get; set; }
        public bool IsNeedPrePainting { get; set; }
        public bool IsElectricianRequired { get; set; }
        public string DetailRequirements { get; set; }
        public bool IsMirrors { get; set; }
        public string MirrorSize { get; set; }
        public bool IsGlassShelves { get; set; }
        public string GlassShelveSize { get; set; }
        public string OtherItems { get; set; }
       public DateTime? DateOfFinalMeasurement { get; set; }
    }
    public class MultiitemCollectionModel
    {
        public long QuoteVersionId { get; set; }
        public long QuoteId { get; set; }
        public string VersionNo { get; set; }
        public decimal? totalAmt { get; set; }
        public decimal? markupper { get; set; }
        public decimal? makrupval { get; set; }
        public decimal? labourDays { get; set; }
        public decimal? ActullabourDays { get; set; }
        public decimal? labourRate { get; set; }
        public decimal? labourCost { get; set; }
        public decimal? markupper1 { get; set; }
        public decimal? makrupval1 { get; set; }
        public decimal? total { get; set; }
        public decimal? vat { get; set; }
        public decimal? netTotal { get; set; }
        public Int32? QuoteNo { get; set; }

        public decimal? actualTotalAmt { get; set; }
        public decimal? actualMakrupval { get; set; }
        public decimal? actuaLabourCost { get; set; }
        public decimal? actualMakrupval1 { get; set; }
        public decimal? actualTotal { get; set; }
        public decimal? actualvat { get; set; }
        public decimal? actualnetTotal { get; set; }

        public List<ItemCollectionModel> itemCollection { get; set; }
        public List<ItemDetailModel> itemDetail { get; set; }
        public List<AdditionalInfoModel> additionalInfo { get; set; }

    }
    public class ItemCollectionModel
    {
        public long QuoteCollectionDetailId { get; set; }
        public long QuoteId { get; set; }
        public long QuoteVersionId { get; set; }
        public string collectionNo { get; set; }
        public long collectionId { get; set; }
        public string collectionName { get; set; }
        public decimal? actualCost { get; set; }
        public decimal? quoteCost { get; set; }
    }
    public class ItemDetailModel
    {
        public long QuoteVersionId { get; set; }
        public long QuoteDetailId { get; set; }
        public long CollectionId { get; set; }
        public long QuoteId { get; set; }
        public long itemId { get; set; }
        public string itemName { get; set; }
        public decimal? actualQty { get; set; }
        public decimal? quotedQty { get; set; }
        public decimal? actualCost { get; set; }
        public decimal? quotedCost { get; set; }
        public decimal? ItemCost { get; set; }
    }
    public class AdditionalInfoModel
    {
        public long QuoteAddInfoDetailId { get; set; }
        public long QuoteId { get; set; }
        public long QuoteVersionId { get; set; }
        public string details { get; set; }
        public string description { get; set; }
        public decimal? quotedQty { get; set; }
        public decimal? cost { get; set; }
        public decimal? Qty { get; set; }
        public decimal? actualQty { get; set; }
        public decimal? actualCost { get; set; }
    }
    public class QuoteCustomerDetail
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
    }
    public class StatusDetail
    {
        public long StatusId { get; set; }
        public string StatusName { get; set; }
        public int srno { get; set; }
    }
    public class JoinerDetail
    {
        public long UserId { get; set; }
        public string Name { get; set; }
    }
    public class QuoteDetailModel : CommonModel
    {
        public long QuoteDetailId { get; set; }
        public long QuoteId { get; set; }
        public long ItemId { get; set; }
        public long ItemCategoryId { get; set; }
        public string ItemName { get; set; }
        public string ItemCategoryName { get; set; }
        public string UOM { get; set; }
        public decimal? Dimension { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Qty { get; set; }
        public decimal? ActualQty { get; set; }
        public long CollectionId { get; set; }
        public decimal? ItemCost { get; set; }
        public bool isSelected { get; set; }
        public bool isdisable { get; set; }
        public bool IsStock { get; set; }

    }
        public class SettingModel: CommonModel
        {
        public long? LaborRate { get; set; }
        public long? MarkUp1 { get; set; }
        public long? MarkUp2 { get; set; }
    }
}
