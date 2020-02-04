using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkspertBooker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisniciKategorijeController : ControllerBase
    {
        protected readonly IKorisniciKategorijeService _service;
        public KorisniciKategorijeController(IKorisniciKategorijeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Model.KorisnikKategorija>> Get([FromQuery]KorisniciKategorijeSearchRequest search)
        {
            return _service.Get(search);
        }

        [HttpGet("{id}")]
        public ActionResult<Model.KorisnikKategorija> GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPut("{id}")]
        public virtual ActionResult<List<Model.KorisnikKategorija>> Update(int id, [FromBody]KorisnikKategorijeUpsertRequest request)
        {
            return _service.Update(id, request);
        }

        [HttpDelete("{id}")]
        public virtual ActionResult<bool> Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}