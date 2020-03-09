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
    public class RecenzijeOPoslodavciController : BaseCRUDController<Model.RecenzijaOPoslodavac, RecenzijeOPoslodavciSearchRequest, RecenzijaOPoslodavacUpsertRequest, RecenzijaOPoslodavacUpsertRequest>
    {
        public RecenzijeOPoslodavciController(IBaseCRUDService<Model.RecenzijaOPoslodavac, RecenzijeOPoslodavciSearchRequest, RecenzijaOPoslodavacUpsertRequest, RecenzijaOPoslodavacUpsertRequest> service) : base(service)
        {
        }

        public override ActionResult<RecenzijaOPoslodavac> Insert(RecenzijaOPoslodavacUpsertRequest request)
        {
            try
            {
                return base.Insert(request);
            }
            catch
            {
                return Conflict();
            }
        }
    }
}