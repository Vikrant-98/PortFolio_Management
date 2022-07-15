using Business.Interface;
using CommonServices.ModelServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortFolioAPI.Controllers
{
    public class PortFolioController : Controller
    {
        private readonly IStockBusiness _stockBusiness;

        public PortFolioController(IStockBusiness stockBusiness) 
        {
            _stockBusiness = stockBusiness;
        }

        [HttpGet("GetAllPortFolioDetails")]
        public PortfolioDetails GetAllPortFolioDetails(int CustomerId)
        {
            var Result = _stockBusiness.GetAllPortFolioDetails(CustomerId);
            return Result;
        }
    }
}
