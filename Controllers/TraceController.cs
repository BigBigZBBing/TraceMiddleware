using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraceMiddleware.Controllers
{
    public class TraceController : Controller
    {
        public IActionResult Index()
        {
            return View(TraceService.SearchLog());
        }
    }
}
