using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateNetWorthApi.Model
{
    public class PortFolioDetails
    {
        [Key]
        public int PortFolioId { get; set; }
        //[ForeignKey("StockDetails")]
        public List<StockDetails> StockList { get; set; }
        //[ForeignKey("MutualFundDetails")]
        public List<MutualFundDetails> MutualFundList { get; set; }
        public string AssetTypeToBeSold { get; set; }
        public string AssetNameToBeSold { get; set; }
       
    }
}
