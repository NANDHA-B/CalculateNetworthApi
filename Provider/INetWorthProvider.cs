using CalculateNetWorthApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateNetWorthApi.Provider
{
    public interface INetWorthProvider
    {
        public Task<AssetSaleResponse> calculateNetWorthAsync(PortFolioDetails portFolioDetails);

        public AssetSaleResponse SellAsset(PortFolioDetails portfolioDetails);
        public PortFolioDetails GetPortFolioDetailsByID(int id);
    }
}
