using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectionsEFCore.Models;
using System.Diagnostics;
using System.Linq;

namespace ProjectionsEFCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
            
        //    using (AdventureWorksLT2019Context db = new AdventureWorksLT2019Context())
        //    {
        //        var salesOrder = db.SalesOrderHeader
        //                        .Include(soh => soh.Customer)
        //                        .Include(soh => soh.ShipToAddress)
        //                        .Take(10).ToList();

        //        return View(salesOrder);
        //    }

        //    //KeyVaultModel keyVaultModel = new KeyVaultModel();

        //    return View();
        //}

        public IActionResult Index()
        {

            using (AdventureWorksLT2019Context db = new AdventureWorksLT2019Context())
            {
                var salesOrder = db.SalesOrderHeader
                                .Include(soh => soh.Customer)
                                .Include(soh => soh.ShipToAddress)
                                .Select(soh =>  new SalesOrderHeader()
                                {
                                    SalesOrderId = soh.SalesOrderId,
                                    SalesOrderNumber = soh.SalesOrderNumber,
                                    PurchaseOrderNumber = soh.PurchaseOrderNumber,
                                    TotalDue = soh.TotalDue,
                                    Customer = new Customer()
                                    {
                                        FirstName = soh.Customer.FirstName,
                                        LastName = soh.Customer.LastName,
                                        EmailAddress = soh.Customer.EmailAddress,
                                    },
                                    ShipToAddress = new Address()
                                    {
                                        AddressLine1 = soh.ShipToAddress.AddressLine1
                                    },
                                    BillToAddress = new Address()
                                    {
                                        AddressLine1 = soh.BillToAddress.AddressLine1
                                    }
                                })
                                .Take(10)
                                .ToList();

                return View(salesOrder);
            }

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
