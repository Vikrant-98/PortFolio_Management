using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortFolio_Management.Business;
using PortFolio_Management.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PortFolio_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserBusiness _business;

        public HomeController(ILogger<HomeController> logger, IUserBusiness business)
        {
            _logger = logger;
            _business = business;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult StockDetails(int id)
        {
            User user = new User();
            user.Id = id;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UserRegistration(CustomerRegister userRegister)
        {
            var Result = await _business.RegisterUser(userRegister).ConfigureAwait(false);
            if (Result)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [Route("/createStocks")]
        [HttpGet]
        public IActionResult CreateStocks()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(CustomerLogin logindata)
        {
            try
            {
                (int, bool) data = _business.LoginVerification(logindata);
                if (data.Item2)
                {
                    return RedirectToAction("StockDetails",new { id = data.Item1 });    
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
            
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
