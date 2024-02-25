﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace APIRetail.Models.Database;

public partial class DailyStock
{
    public long Id { get; set; }

    public long? CompanyId { get; set; }

    public long? BranchId { get; set; }

    public long? ProductTypeId { get; set; }

    public long? ProductId { get; set; }

    public long? StockFirst { get; set; }

    public long? StockBuy { get; set; }

    public long? StockBuyPrice { get; set; }

    public long? StockSell { get; set; }

    public long? StockSellPrice { get; set; }

    public long? StockLast { get; set; }

    public DateOnly StockDate { get; set; }

    public string CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Branch Branch { get; set; }

    public virtual Company Company { get; set; }

    public virtual Product Product { get; set; }

    public virtual ProductType ProductType { get; set; }
}