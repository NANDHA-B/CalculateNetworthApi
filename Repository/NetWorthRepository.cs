using CalculateNetWorthApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;

namespace CalculateNetWorthApi.Repository
{
    public class NetworthRepository : INetWorthRepository
    {

        private IConfiguration configuration;

        public NetworthRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        

        public static List<PortFolioDetails> _portFolioDetails = new List<PortFolioDetails>()
            {
                new PortFolioDetails{
                    PortFolioId=1001,
                    MutualFundList = new List<MutualFundDetails>()
                    {
                        new MutualFundDetails{MutualFundName = "LIC", MutualFundUnits=4},
                        new MutualFundDetails{MutualFundName = "NIPPON", MutualFundUnits=6},
                        new MutualFundDetails{MutualFundName = "AXIS", MutualFundUnits=15},
                        new MutualFundDetails{MutualFundName = "ICICI", MutualFundUnits=7}
                    },
                    StockList = new List<StockDetails>()
                    {
                        new StockDetails{StockName = "TATASTEEL",StockCount = 6},
                        new StockDetails{StockName = "HDFC",StockCount = 8},
                        new StockDetails{StockName = "SBI",StockCount = 10},
                        new StockDetails{StockName = "BAJAJ",StockCount = 12}
                    },
                    AssetTypeToBeSold="Stock",
                    AssetNameToBeSold="HDFC"

                },
                new PortFolioDetails
                {
                    PortFolioId =1002,
                    MutualFundList = new List<MutualFundDetails>()
                    {
                        new MutualFundDetails{MutualFundName = "AXIS", MutualFundUnits=3},
                        new MutualFundDetails{MutualFundName = "ICICI", MutualFundUnits=5}
                    },
                    StockList = new List<StockDetails>()
                    {
                        new StockDetails{StockName = "TECH",StockCount = 6},
                        new StockDetails{StockName = "HDFC",StockCount = 8}
                    }
                },
                new PortFolioDetails
                {
                    PortFolioId =1003,
                    MutualFundList = new List<MutualFundDetails>()
                    {
                        new MutualFundDetails{MutualFundName = "ICICI", MutualFundUnits=4},
                        new MutualFundDetails{MutualFundName = "KOTAK", MutualFundUnits=7},
                        new MutualFundDetails{MutualFundName = "NIPPON", MutualFundUnits=6}
                    },
                    StockList = new List<StockDetails>()
                    {
                        new StockDetails{StockName = "SBI",StockCount = 10},
                        new StockDetails{StockName = "BAJAJ",StockCount = 12}
                    }
                },
                new PortFolioDetails
                {
                    PortFolioId = 1004,
                    MutualFundList = new List<MutualFundDetails>()
                    {
                        new MutualFundDetails{MutualFundName = "LIC", MutualFundUnits=14},
                        new MutualFundDetails{MutualFundName = "AXIS", MutualFundUnits=20}
                    },
                    StockList = new List<StockDetails>()
                    {
                        new StockDetails{StockName = "TATASTEEL",StockCount = 16},
                        new StockDetails{StockName = "SBI",StockCount = 18}
                    }
                },
                new PortFolioDetails
                {
                    PortFolioId = 1005,
                    MutualFundList = new List<MutualFundDetails>()
                    {
                        new MutualFundDetails{MutualFundName = "AXIS", MutualFundUnits=2},
                        new MutualFundDetails{MutualFundName = "LIC", MutualFundUnits=4},
                        new MutualFundDetails{MutualFundName = "ICICI", MutualFundUnits=6}
                    },
                    StockList = new List<StockDetails>()
                    {
                        new StockDetails{StockName = "BAJAJ",StockCount = 4},
                        new StockDetails{StockName = "TATASTEEL",StockCount = 6},
                        new StockDetails{StockName = "HDFC",StockCount = 8}
                    }
                }


            };

        public async Task<AssetSaleResponse> calculateNetWorthAsync(PortFolioDetails portFolioDetails)
        {

            Stock stock = new Stock() ;
            MutualFund mutualfund = new MutualFund();
            AssetSaleResponse networth = new AssetSaleResponse();
            
            try
            {
                if (portFolioDetails.StockList != null && portFolioDetails.StockList.Any() == true)
                {
                    foreach (StockDetails stockDetails in portFolioDetails.StockList)
                    {
                        if (stockDetails.StockName != null)
                        {
                            var result = new Stock();
                                                                
                            using(var client =new HttpClient())
                            {
                                var res = client.GetAsync("https://localhost:44385/api/DailySharePrice/stockname?stockname=" + stockDetails.StockName);
                                var data = res.Result.Content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<Stock>(data.Result);
                            }
                            networth.NetWorth += stockDetails.StockCount * result.StockValue;
                        }
                    }
                }
                if (portFolioDetails.MutualFundList != null && portFolioDetails.MutualFundList.Any() == true)
                {
                    foreach (MutualFundDetails mutualFundDetails in portFolioDetails.MutualFundList)
                    {
                        if (mutualFundDetails.MutualFundName != null)
                        {
                            var result = new MutualFund();
                            using (var client = new HttpClient())
                            {
                                var res = client.GetAsync("https://localhost:44366/api/MutualFundNAV/mutualfundname?mutualfundname=" + mutualFundDetails.MutualFundName);
                                var data = res.Result.Content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<MutualFund>(data.Result);
                            }
                            networth.NetWorth += mutualFundDetails.MutualFundUnits * result.MutualFundValue;
                        }
                    }
                }
                
                networth.NetWorth = Math.Round(networth.NetWorth, 2);
            }
            catch (Exception ex)
            {
            }
            return networth;
        }

        
        public PortFolioDetails GetPortFolioDetailsByID(int id)
        {
            PortFolioDetails portFolioDetails = new PortFolioDetails();
            
            portFolioDetails = _portFolioDetails.FirstOrDefault(e => e.PortFolioId == id);
            return portFolioDetails;
        }
    }
}
