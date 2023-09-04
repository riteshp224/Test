using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteManagement.Model.Models
{
    public class ItemMasterModel : CommonModel
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public long ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public long? UOMId { get; set; }
        public string UOMName { get; set; }
        public decimal? Cost { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public decimal? Dimension { get; set; }
        public decimal? AvailableStock { get; set; }
        public bool? IsStock { get; set; }
        public string Description { get; set; }
        public bool? IsPopular { get; set; }
        public string ItemPhoto { get; set; }
        public string imageURL { get; set; }
        public IFormFile ItemPhotoFile { get; set; }
        public string SundriesLine { get; set; }
        public string Price{ get; set; }






    }
}
