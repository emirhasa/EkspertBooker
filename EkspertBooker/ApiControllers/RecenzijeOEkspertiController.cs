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
    public class RecenzijeOEkspertiController : BaseCRUDController<Model.RecenzijaOEkspert, RecenzijeOEkspertiSearchRequest, RecenzijaOEkspertUpsertRequest, RecenzijaOEkspertUpsertRequest>
    {
        public RecenzijeOEkspertiController(IBaseCRUDService<Model.RecenzijaOEkspert, RecenzijeOEkspertiSearchRequest, RecenzijaOEkspertUpsertRequest, RecenzijaOEkspertUpsertRequest> service) : base(service)
        {
        }

        public override ActionResult<RecenzijaOEkspert> Insert(RecenzijaOEkspertUpsertRequest request)
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