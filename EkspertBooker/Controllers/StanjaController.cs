using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkspertBooker.WebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkspertBooker.WebAPI.Controllers
{
    public class StanjaController : BaseGetController<Model.Stanje, object>
    {
        public StanjaController(IBaseGetService<Model.Stanje, object> service):base(service)
        {
        }
    }
}