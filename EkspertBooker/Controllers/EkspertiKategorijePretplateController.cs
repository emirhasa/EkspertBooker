using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkspertBooker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EkspertiKategorijePretplateController : ControllerBase
    {
        protected readonly IEkspertiKategorijePretplateService _service = null;
        public EkspertiKategorijePretplateController(IEkspertiKategorijePretplateService service) 
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Model.EkspertKategorijaPretplata>> Get([FromQuery]EkspertKategorijeSearchRequest request)
        {
            return _service.Get(request);
        }

        [HttpGet("{id}")]
        public ActionResult<Model.EkspertKategorijaPretplata> GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPut("{id}")]
        public ActionResult<List<Model.EkspertKategorijaPretplata>> Update(int id, [FromBody]EkspertKategorijeUploadRequest request)
        { 
            return _service.Update(id, request);
        }

        [HttpPost]
        public ActionResult<Model.EkspertKategorijaPretplata> Insert([FromBody]EkspertKategorijaInsertRequest request)
        {
            return _service.Insert(request);
        }

        [HttpDelete]
        public ActionResult<bool> Delete(int id)
        {
            return _service.Delete(id);
        }
        
    }
}