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
    public class ProjektDetaljiPriloziController : BaseCRUDController<Model.ProjektDetaljiPrilog, object, ProjektDetaljiPrilogUpsertRequest, ProjektDetaljiPrilogUpsertRequest>
    {
        public ProjektDetaljiPriloziController(IBaseCRUDService<Model.ProjektDetaljiPrilog, object, ProjektDetaljiPrilogUpsertRequest, ProjektDetaljiPrilogUpsertRequest> service) : base(service)
        {
        }

        [Authorize(Roles ="Administrator, Poslodavac")]
        public override ActionResult<ProjektDetaljiPrilog> Insert(ProjektDetaljiPrilogUpsertRequest request)
        {
            return base.Insert(request);
        }

        [Authorize(Roles = "Administrator, Poslodavac")]
        public override ActionResult<ProjektDetaljiPrilog> Update(int id, [FromBody] ProjektDetaljiPrilogUpsertRequest request)
        {
            return base.Update(id, request);
        }

        [Authorize(Roles = "Administrator, Poslodavac")]
        public override ActionResult<bool> Delete(int id)
        {
            return base.Delete(id);
        }

    }
}