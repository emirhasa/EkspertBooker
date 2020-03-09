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
    public class PonudeController : BaseCRUDController<Model.Ponuda, PonudeSearchRequest, PonudaUpsertRequest, PonudaUpsertRequest>
    {
        public PonudeController(IBaseCRUDService<Model.Ponuda, PonudeSearchRequest, PonudaUpsertRequest, PonudaUpsertRequest> service) : base(service)
        {
        }

        public override ActionResult<Model.Ponuda> Update(int id, [FromBody] PonudaUpsertRequest request)
        {
            try
            {
                return base.Update(id, request);
            }
            catch
            {
                return Conflict();
            }
        }
    }
}