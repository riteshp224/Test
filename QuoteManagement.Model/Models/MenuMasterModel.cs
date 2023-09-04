using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteManagement.Model.Models
{
    public class MenuMasterModel : CommonModel
    {
        public long menuId { get; set; }
        public long Parentmenuid { get; set; }
        public string menuName { get; set; }
        public string Parentmenuname { get; set; }
        public string menuUrl { get; set; }
        public string menuIcon { get; set; }
        public long userId { get; set; }
        
    }
}
