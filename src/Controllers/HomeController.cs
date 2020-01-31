using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using elastic_kibana.Models;

namespace elastic_kibana.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("In the Index method at {timestamp} for {orderId}", DateTime.UtcNow, 123);
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("In the Privacy method at {timestamp} for {orderId}", DateTime.UtcNow, 123);
            return View();
        }

        public IActionResult WriteStructuredMessage()
        {
            var mymodel = new MyModel{ FirstField = "Structured", SecondField = 2020, ThirdField = Guid.NewGuid()};
            var mytimestamp = DateTime.UtcNow;
            var mypersonId = 5;
            var myorderId = 123;

            _logger.LogError(new ApplicationException("Something bad just happened"), 
                "Ooops, structured my bad @ {timestamp} for {personId} for {orderId} with model {@model}", mytimestamp, mypersonId, myorderId, mymodel);
            return View();
        }

        public IActionResult WriteUnstructuredMessage()
        {
            var mymodel = new MyModel{ FirstField = "Unstructured", SecondField = 1917, ThirdField = Guid.NewGuid()};
            var mytimestamp = DateTime.UtcNow;
            var mypersonId = 5;
            var myorderId = 123;

            _logger.LogError(new ApplicationException("Something bad just happened"), 
                $"Ooops, unstructured my bad @ {mytimestamp} for {mypersonId} for {myorderId} with model {mymodel}");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("In the Error method at {timestamp}", DateTime.UtcNow);

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
