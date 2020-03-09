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
    public class KorisniciSlikeController : BaseCRUDController<Model.KorisnikSlika, object, KorisnikSlikaUpsertRequest, KorisnikSlikaUpsertRequest>
    {
        public KorisniciSlikeController(IBaseCRUDService<Model.KorisnikSlika, object, KorisnikSlikaUpsertRequest, KorisnikSlikaUpsertRequest> service): base(service)
        {
        }
    }
}