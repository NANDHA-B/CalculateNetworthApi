using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateNetWorthApi.Model
{
    public class StockDetails
    {
        [Key]
        public string StockName { get; set; }
        public int StockCount { get; set; }
    }
}
