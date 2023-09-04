using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteManagement.Model.Models
{
    public class ReportModel : CommonModel
    {
        public long QuoteId { get; set; }
        public string QuoteName { get; set; }
        public int QuoteNo { get; set; }
        public string CustomerName { get; set; }
        public string Joiners { get; set; }

    }
    public class StatusWiseQuoteDetailModel : CommonPaginationModel
    {
        public string Status { get; set; }
        public int NoOfQuotes { get; set; }
        public decimal ToalValueOfQuotes { get; set; }
        public string QuoteStatusId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate{ get; set; }
    }
    public class CompletedQuoteDetailModel : CommonPaginationModel
    {
        public int CompletedQuote { get; set; }
        public decimal ToalQuotedAmount { get; set; }
        public decimal ToalActualAmount { get; set; }
        public decimal ProfitAmount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class CustomerDetailModel : CommonPaginationModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LeadSource { get; set; }
        public string address { get; set; }
    }
    public class LowItemStockDetailModel : CommonPaginationModel
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal? AvailableStock { get; set; }
        public string imageURL { get; set; }
        public bool? IsStock { get; set; }
        public decimal? Cost { get; set; }
    }

    public class StatusWiseQuoteDetails : CommonPaginationModel
    {
        public long QuoteId { get; set; }

        public string QuoteName { get; set; }

        public string CustomerName { get; set; }

        public string SiteAddress1 { get; set; }

        public string Description { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
