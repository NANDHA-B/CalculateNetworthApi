using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateNetWorthApi.Model
{
    public class AssetSaleResponse
    {
        public bool SaleStatus { get; set; }

        public double NetWorth { get; set; }

        public static implicit operator AssetSaleResponse(PortFolioDetails v)
        {
            throw new NotImplementedException();
        }
    }
}
