using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateNetWorthApi.Model
{
    public class NetWorthContext:DbContext
    {
        public NetWorthContext() { }
        public NetWorthContext(DbContextOptions options) : base(options) { }
        public DbSet<PortFolioDetails> portFolioDetails { get; set; }
        public DbSet<StockDetails> stockDetails { get; set; }
        public DbSet<MutualFundDetails> mutualFundDetails { get; set; }
    }
}
