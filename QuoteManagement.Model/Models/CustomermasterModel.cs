using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteManagement.Model.Models
{
   public class CustomerMasterModel : CommonModel
    {
        public long CustomerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CityTown { get; set; }
        public string PostCode { get; set; }
        public string LeadSource { get; set; }
    }
}
