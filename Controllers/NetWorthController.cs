using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculateNetWorthApi.Model;
using CalculateNetWorthApi.Provider;
using CalculateNetWorthApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace CalculateNetWorthApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NetWorthController : ControllerBase
    {
        private readonly INetWorthProvider _netWorthProvider;

        public NetWorthController(INetWorthProvider netWorthProvider)
        {
            _netWorthProvider = netWorthProvider;
        }
        [HttpGet("{portFolioId}")]
        public IActionResult GetPortFolioDetailsByID(int portFolioId)
        {
            PortFolioDetails portFolioDetails = new PortFolioDetails();
            try
            {
                if (portFolioId <= 0)
                {
                    return NotFound("ID can't be 0 or less than 0");
                }
                portFolioDetails = _netWorthProvider.GetPortFolioDetailsByID(portFolioId);
                if (portFolioDetails == null)
                {
                    return NotFound("Sorry, We don't have a portfolio with that ID");
                }
                else
                {                    
                    return Ok(portFolioDetails);
                }
            }
            catch (Exception)
            {               
                return new StatusCodeResult(500);
            }
        }
        [HttpPost]
        [Route("GetWorth")]
        public IActionResult GetNetWorth(PortFolioDetails portFolioDetails)
        {

            AssetSaleResponse netWorth = new AssetSaleResponse();
            
            try
            {
                if (portFolioDetails == null)
                {
                    return NotFound("The portfolio doesn't contain any data");
                }
                else if (portFolioDetails.PortFolioId == 0)
                {
                    return NotFound("The user with that id not found");
                }
                else
                {
                    
                    netWorth = _netWorthProvider.calculateNetWorthAsync(portFolioDetails).Result;
                    return Ok(netWorth);
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult SellAssets(PortFolioDetails portFolioDetails)
        {
            try
            {
                AssetSaleResponse assetSaleResponse = new AssetSaleResponse();
                if (portFolioDetails == null)
                {
                    return NotFound("The portfolio doesn't contain any data");
                }
                else if (portFolioDetails.PortFolioId == 0)
                {
                    return NotFound("The user with that id not found");
                }
                else
                {

                    assetSaleResponse = _netWorthProvider.SellAsset(portFolioDetails);
                    if (assetSaleResponse == null)
                    {
                        
                        return NotFound("Please provide a valid list of portfolios");
                    }
                    return Ok(assetSaleResponse);
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}