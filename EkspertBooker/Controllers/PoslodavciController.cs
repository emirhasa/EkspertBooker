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
    public class PoslodavciController : BaseCRUDController<Model.Poslodavac, PoslodavciSearchRequest, PoslodavacUpsertRequest, PoslodavacUpsertRequest>
    {
        public PoslodavciController(IBaseCRUDService<Model.Poslodavac, PoslodavciSearchRequest, PoslodavacUpsertRequest, PoslodavacUpsertRequest> service) : base(service)
        { 
        }
       
        [Authorize(Roles ="Administrator, Poslodavac")]
        public override ActionResult<Model.Poslodavac> Update(int id, PoslodavacUpsertRequest request)
        {
            return _service.Update(id, request);
        }
    }
}