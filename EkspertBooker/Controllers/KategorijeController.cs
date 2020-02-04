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
    public class KategorijeController : BaseCRUDController<Model.Kategorija, object, KategorijaUpsertRequest, KategorijaUpsertRequest>
    {
        public KategorijeController(IBaseCRUDService<Model.Kategorija, object, KategorijaUpsertRequest, KategorijaUpsertRequest> service): base(service)
        {
        }

        [Authorize(Roles = "Administrator")]
        public override ActionResult<Kategorija> Insert(KategorijaUpsertRequest request)
        {
            return base.Insert(request);
        }

        [Authorize(Roles = "Administrator")]
        public override ActionResult<Kategorija> Update(int id, [FromBody] KategorijaUpsertRequest request)
        {
            return base.Update(id, request);
        }

        [Authorize(Roles = "Administrator")]
        public override ActionResult<bool> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}