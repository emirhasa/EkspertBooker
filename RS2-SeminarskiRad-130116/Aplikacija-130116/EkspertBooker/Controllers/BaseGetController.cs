using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkspertBooker.WebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkspertBooker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseGetController<TModel, TSearch> : ControllerBase
    {
        private readonly IBaseGetService<TModel, TSearch> _service;

        public BaseGetController(IBaseGetService<TModel, TSearch> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual ActionResult<List<TModel>> Get([FromQuery]TSearch search)
        {
            return _service.Get(search);
        }

        [HttpGet("{id}")]
        public virtual ActionResult<TModel> GetById(int id)
        {
            return _service.GetById(id);
        }

    }
}