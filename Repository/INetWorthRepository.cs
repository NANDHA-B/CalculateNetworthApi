using CalculateNetWorthApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateNetWorthApi.Repository
{
    public interface INetWorthRepository
    {
        public Task<AssetSaleResponse> calculateNetWorthAsync(PortFolioDetails portFolioDetails);
        public PortFolioDetails GetPortFolioDetailsByID(int id);
    }
}
