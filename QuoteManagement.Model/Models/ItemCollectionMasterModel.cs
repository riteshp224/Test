using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteManagement.Model.Models
{
   public class ItemCollectionMasterModel : CommonModel
    {
        public long ItemCollectionId { get; set; }
        public string ItemCollectionNo { get; set; }
        public string ItemCollectionName { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public decimal? Cost { get; set; }
        public string LabourDays { get; set; }
        public string ItemPhoto { get; set; }
        public string imageURL { get; set; }
        public IFormFile ItemPhotoFile { get; set; }
        public List<ItemCollectionDetailModel> itemCollectionDetails { get; set; }
    }
    public class ItemCollectionDetailModel : CommonModel
    {
        public long ItemCollectionDetailId { get; set; }
        public long ItemCollectionId { get; set; }
        public long ItemId { get; set; }
        public long ItemCategoryId { get; set; }
        public string ItemCollectionName { get; set; }
        public string Length { get; set; }
        public string ItemName { get; set; }
        public string ItemCategoryName { get; set; }
        public decimal? Dimension { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Quantity { get; set; }
    }
}
