using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkspertBooker.WebAPI.Controllers
{
    [Authorize]
    public class ProjektDetaljiController : BaseCRUDController<Model.ProjektDetalji, ProjektDetaljiSearchRequest, ProjektDetaljiUpsertRequest, ProjektDetaljiUpsertRequest> 
    {
        public ProjektDetaljiController(IBaseCRUDService<Model.ProjektDetalji, ProjektDetaljiSearchRequest, ProjektDetaljiUpsertRequest, ProjektDetaljiUpsertRequest> service) : base(service)
        {
        }

        [Authorize(Roles = "Poslodavac")]
        public override ActionResult<ProjektDetalji> Insert(ProjektDetaljiUpsertRequest request)
        {
            return base.Insert(request);
        }

        [Authorize(Roles = "Administrator, Poslodavac")]
        public override ActionResult<ProjektDetalji> Update(int id, [FromBody] ProjektDetaljiUpsertRequest request)
        {
            return base.Update(id, request);
        }

        [Authorize(Roles = "None")]
        public override ActionResult<bool> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}