using CalculateNetWorthApi.Model;
using CalculateNetWorthApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateNetWorthApi.Provider
{
    public class NetWorthProvider : INetWorthProvider
    {
        private readonly INetWorthRepository _netWorthRepository;
        
        public NetWorthProvider(INetWorthRepository netWorthRepository)
        {
            _netWorthRepository = netWorthRepository;
        }

        public Task<AssetSaleResponse> calculateNetWorthAsync(PortFolioDetails portFolioDetails)
        {
             AssetSaleResponse networth = new AssetSaleResponse();
       
            if (portFolioDetails.PortFolioId == 0)
            {
                return null;
            }
            networth = _netWorthRepository.calculateNetWorthAsync(portFolioDetails).Result;
        
            return Task.FromResult(networth);


        }
               
        public AssetSaleResponse SellAsset(PortFolioDetails portfolioDetails)
        {

            AssetSaleResponse _networth = new AssetSaleResponse();
            AssetSaleResponse assetSaleResponse = null;


            bool saleStatus = false;

            if (portfolioDetails.AssetTypeToBeSold == "Stock")
            {
                StockDetails stockToBeSold = portfolioDetails.StockList.FirstOrDefault(x => x.StockName.ToLower() == portfolioDetails.AssetNameToBeSold.ToLower());

                saleStatus = portfolioDetails.StockList.Remove(stockToBeSold);
            }
            else
            {

                MutualFundDetails mutualFundToBeSold = portfolioDetails.MutualFundList.FirstOrDefault(x => x.MutualFundName.ToLower() == portfolioDetails.AssetNameToBeSold.ToLower());

                saleStatus = portfolioDetails.MutualFundList.Remove(mutualFundToBeSold);
            }
            _networth = _netWorthRepository.calculateNetWorthAsync(portfolioDetails).Result;

            assetSaleResponse = new AssetSaleResponse()
            {
                SaleStatus = saleStatus,
                NetWorth = _networth.NetWorth
            };
            return assetSaleResponse;
        }

        public PortFolioDetails GetPortFolioDetailsByID(int id)
        {
            PortFolioDetails portfolioDetails = new PortFolioDetails();
            try
            {
                portfolioDetails = _netWorthRepository.GetPortFolioDetailsByID(id);
            }
            catch (Exception ex)
            {
                
            }
            return portfolioDetails;
        }
    }
}
