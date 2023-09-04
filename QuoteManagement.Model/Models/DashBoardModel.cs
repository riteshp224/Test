using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteManagement.Model.Models
{
    public class DashBoardModel
    {
        public long CustomerCount { get; set; }
        public string QuotesCount { get; set; }
        public string QuotesvalueCount { get; set; }
        public string WinAmount { get; set; }
    }

    public class Comparsionchart
    {
        public long QuoteId { get; set; }
        public string QuoteNo { get; set; }
        public decimal? netTotal { get; set; }
        public decimal? actualnetTotal { get; set; }
    }

    public class QuoteDataModel
    {
        public long QuoteId { get; set; }
        public string QuoteNo { get; set; }
        public string Month { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<MonthwiseQuoteDataModel> monthwiseQuoteDataList { get; set; }
    }
    public class MonthwiseQuoteDataModel
    {
        public long quoteid { get; set; }
        public string QuoteNo { get; set; }
        public string MonthName { get; set; }
        public string totalAmt { get; set; }
    }

}
