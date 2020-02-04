using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkspertBooker.WebAPI.Controllers
{
    [Authorize]
    public class KorisniciController : BaseCRUDController<Model.Korisnik, KorisniciSearchRequest, KorisnikUpsertRequest, KorisnikUpsertRequest>
    {
        public KorisniciController(IKorisniciService service) : base(service)
        {
        }

        [Authorize(Roles = "Administrator")]
        public override ActionResult<Model.Korisnik> Insert(KorisnikUpsertRequest request)
        {
            try
            {
                return _service.Insert(request);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        public override ActionResult<Model.Korisnik> Update(int id, KorisnikUpsertRequest request)
        {
            return _service.Update(id, request);
        }

        [Authorize(Roles = "Administrator")]
        public override ActionResult<bool> Delete(int id)
        {
            return _service.Delete(id);
        }

    }
}