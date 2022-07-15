using CommonServices.ModelServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortFolio_Management.ProxyClientService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PortFolio_Management.Controllers
{
    public class PortFolioController : Controller
    {
        private readonly PortFolioClient _PortFolioClient;
        private readonly StocksClient _StocksClient;
        private readonly MutualFundClient _MutualFundClient;

        public class Root
        {
            public string company { get; set; }
            public string description { get; set; }
            public double initial_price { get; set; }
            public double price_2002 { get; set; }
            public double price_2007 { get; set; }
            public string symbol { get; set; }
        }

        public PortFolioController(PortFolioClient PortFolioClient, StocksClient StocksClient, MutualFundClient MutualFundClient)
        {
            _PortFolioClient = PortFolioClient;
            _StocksClient = StocksClient;
            _MutualFundClient = MutualFundClient;
        }


        //[HttpGet]
        //public async Task<IActionResult> GetAllStocks()
        //{
        //    var rrr = LoadStocksJson();

        //    foreach (var item in rrr)
        //    {
        //        var tt = new AddStocks()
        //        {
        //            StockName = item.company,
        //            StockPrice = item.initial_price
        //        };
        //        await _stockBusiness.AddStocks(tt).ConfigureAwait(false);
        //    }
        //    var ttt = LoadMutualJson();

        //    foreach (var item in ttt)
        //    {
        //        var tt = new AddStocks()
        //        {
        //            StockName = item.company,
        //            StockPrice = item.initial_price
        //        };
        //        await _stockBusiness.AddStocks(tt).ConfigureAwait(false);
        //    }
        //    return Json(new { data = "" });
        //}


        private List<Root> LoadStocksJson()
        {
            try
            {
                string strFilePath = Directory.GetCurrentDirectory() + @"/" + "stocks.json";
                List<Root> items = new List<Root>();
                using (StreamReader r = new StreamReader(strFilePath))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Root>>(json);
                }
                return items;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private List<Root> LoadMutualJson()
        {
            try
            {
                string strFilePath = Directory.GetCurrentDirectory() + @"/" + "mutual.json";
                List<Root> items = new List<Root>();
                using (StreamReader r = new StreamReader(strFilePath))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Root>>(json);
                }
                return items;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet]
        public IActionResult GetAllMutualFunds()
        {
            var Result = _MutualFundClient.GetAllMutualFunds();
            return Json(new { data = Result });
        }
        [HttpPost]
        public async Task<IActionResult> AddMutualFunds(AddMutualFunds MutualFund)
        {
            var Result = await _MutualFundClient.AddMutualFunds(MutualFund).ConfigureAwait(false);
            if (Result)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerMutualFunds(AddCustomerMutualFunds mutualFunds)
        {
            var Result = await _MutualFundClient.AddCustomerMutualFunds(mutualFunds).ConfigureAwait(false);
            if (Result)
            {
                return Json(new { data = Result });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCustomerMutualFunds(AddCustomerMutualFunds mutualFunds)
        {
            mutualFunds.MutualFundQuantity = 0 - mutualFunds.MutualFundQuantity;
            var Result = await _MutualFundClient.AddCustomerMutualFunds(mutualFunds).ConfigureAwait(false);
            if (Result)
            {
                return Json(new { data = Result });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStocks(AddStocks userStocks)
        {
            var Result = await _StocksClient.AddStocks(userStocks).ConfigureAwait(false);
            if (Result)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetAllStocks()
        {
            var Result = _StocksClient.GetAllStocks();
            return Json(new { data = Result });
        }


        [HttpPost]
        public async Task<IActionResult> AddCustomerStocks(AddCustomerStocks stocks)
        {
            var Result = await _StocksClient.AddCustomerStocks(stocks).ConfigureAwait(false);
            if (Result)
            {
                return Json(new { data = Result });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCustomerStocks(AddCustomerStocks stocks)
        {
            stocks.StocksQuantity = 0 - stocks.StocksQuantity;
            var Result = await _StocksClient.AddCustomerStocks(stocks).ConfigureAwait(false);
            if (Result)
            {
                return Json(new { data = Result });
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetAllPortFolioDetails(int CustomerId)
        {
            var Result = _PortFolioClient.GetAllPortFolioDetails(CustomerId);
            return Json(new { data = Result });
        }

        [HttpGet]
        public IActionResult GetAllCustomerMutualFunds()
        {
            var Result = _MutualFundClient.GetAllMutualFunds();
            return View();
        }
    }
}
