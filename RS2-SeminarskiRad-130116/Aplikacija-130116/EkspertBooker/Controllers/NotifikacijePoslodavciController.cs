using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EkspertBooker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifikacijePoslodavciController : ControllerBase
    {
        IMapper _mapper;
        EkspertBookerContext _context;

        public NotifikacijePoslodavciController(EkspertBookerContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Model.NotifikacijaPoslodavac>> Get([FromQuery] NotifikacijePoslodavciSearchRequest search)
        {
            var query = _context.NotifikacijePoslodavci.AsQueryable();

            if (search.PoslodavacId.HasValue)
            {
                query = query.Where(ne => ne.PoslodavacId == search.PoslodavacId);
            }

            query = query.OrderByDescending(np => np.Vrijeme).Take(10);

            var result = query.ToList();
            return _mapper.Map<List<Model.NotifikacijaPoslodavac>>(result);
        }

    }
}