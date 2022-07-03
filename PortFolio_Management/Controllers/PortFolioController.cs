using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortFolio_Management.Business;
using PortFolio_Management.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PortFolio_Management.Controllers
{
    public class PortFolioController : Controller
    {
        private readonly IStockBusiness _stockBusiness;
        private readonly IMutualFundBusiness _mutualFundBusiness;

        public class Root
        {
            public string company { get; set; }
            public string description { get; set; }
            public double initial_price { get; set; }
            public double price_2002 { get; set; }
            public double price_2007 { get; set; }
            public string symbol { get; set; }
        }

        public PortFolioController(IStockBusiness stockBusiness, IMutualFundBusiness mutualFundBusiness)
        {
            _stockBusiness = stockBusiness;
            _mutualFundBusiness = mutualFundBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> AddStocks(AddStocks userStocks)
        {
            //var rrr = LoadJson();

            //foreach (var item in rrr)
            //{
            //    var tt = new AddStocks()
            //    {
            //        StockName = item.company,
            //        StockPrice = item.initial_price
            //    };
            //    await _stockBusiness.AddStocks(tt).ConfigureAwait(false);
            //}

            var Result = await _stockBusiness.AddStocks(userStocks).ConfigureAwait(false);
            if (Result)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //private List<Root> LoadJson()
        //{
        //    try
        //    {
        //        string strFilePath = Directory.GetCurrentDirectory() + @"/" + "stocks.json";
        //        List<Root> items = new List<Root>();
        //        using (StreamReader r = new StreamReader(strFilePath))
        //        {
        //            string json = r.ReadToEnd();
        //            items = JsonConvert.DeserializeObject<List<Root>>(json);
        //        }
        //        return items;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        [HttpGet]
        public IActionResult GetAllStocks()
        {
            var Result = _stockBusiness.GetAllStocks();
            return Json(new {data = Result });
        }

        [HttpPost]
        public async Task<IActionResult> AddMutualFunds(AddMutualFunds MutualFund)
        {
            var Result = await _mutualFundBusiness.AddMutualFunds(MutualFund).ConfigureAwait(false);
            if (Result)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetAllPortFolioDetails(int CustomerId)
        {
            var Result = _stockBusiness.GetAllPortFolioDetails(CustomerId);
            return Json(new { data = Result });
        }

        [HttpPost]
        public IActionResult GetAllCustomerStocks(int CustomerId)
        {
            var Result = _stockBusiness.GetAllCustomerStocks(CustomerId);
            return Json(new { data = Result });
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerStocks(AddCustomerStocks stocks)
        {
            var Result = await _stockBusiness.AddCustomerStocks(stocks).ConfigureAwait(false);
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
            var Result = await _stockBusiness.AddCustomerStocks(stocks).ConfigureAwait(false);
            if (Result)
            {
                return Json(new { data = Result });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerMutualFunds(AddCustomerMutualFunds mutualFunds)
        {
            var Result = await _mutualFundBusiness.AddCustomerMutualFunds(mutualFunds).ConfigureAwait(false);
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
            var Result = await _mutualFundBusiness.AddCustomerMutualFunds(mutualFunds).ConfigureAwait(false);
            if (Result)
            {
                return Json(new { data = Result });
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetAllMutualFunds()
        {
            var Result = _mutualFundBusiness.GetAllMutualFunds();
            return Json(new { data = Result });
        }

        [HttpGet]
        public IActionResult GetAllCustomerMutualFunds()
        {
            var Result = _mutualFundBusiness.GetAllMutualFunds();
            return View();
        }

        [HttpGet]
        public IActionResult GetAllCustomerStocks()
        {
            var Result = _mutualFundBusiness.GetAllMutualFunds();
            //if (Result)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPortFolio()
        {
            var Result = _mutualFundBusiness.GetAllMutualFunds();
            //if (Result)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }
    }
}
