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
    public class EkspertiController : BaseCRUDController<Model.Ekspert, EkspertiSearchRequest, EkspertUpsertRequest, EkspertUpsertRequest>
    {
        public EkspertiController(IBaseCRUDService<Model.Ekspert, EkspertiSearchRequest, EkspertUpsertRequest, EkspertUpsertRequest> service) : base(service)
        { 
        }
       
        [Authorize(Roles ="Administrator, Ekspert")]
        public override ActionResult<Model.Ekspert> Update(int id, EkspertUpsertRequest request)
        {
            return _service.Update(id, request);
        }
    }
}