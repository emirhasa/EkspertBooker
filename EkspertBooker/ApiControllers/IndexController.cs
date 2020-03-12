using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EkspertBooker.WebAPI.ApiControllers
{
    public class IndexController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/")]
        [Route("/docs")]
        public IActionResult Home()
        {
            return new RedirectResult("~/swagger/index.html");
        }
    }
}