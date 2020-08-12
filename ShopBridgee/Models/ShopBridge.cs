using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ShopBridgee.Models
{
    public class ShopBridge : DbContext
    {
        //DbSet<Sale> sales { get; set; }

        public System.Data.Entity.DbSet<ShopBridgee.Models.Sale> Sales { get; set; }

        public System.Data.Entity.DbSet<ShopBridgee.Models.SaleNew> SaleNews { get; set; }
    }
}