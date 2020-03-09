using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkspertBooker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifikacijeEkspertiController : ControllerBase
    {
        IMapper _mapper;
        EkspertBookerContext _context;

        public NotifikacijeEkspertiController(EkspertBookerContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Model.NotifikacijaEkspert>> Get([FromQuery] NotifikacijeEkspertiSearchRequest search)
        {
            var query = _context.NotifikacijeEksperti.AsQueryable();

            if(search.EkspertId.HasValue)
            {
                query = query.Where(ne => ne.EkspertId == search.EkspertId);
            }

            query = query.OrderByDescending(ne => ne.Vrijeme).Take(10);

            var result = query.ToList();
            return _mapper.Map<List<Model.NotifikacijaEkspert>>(result);
        }

        [HttpPost]
        public ActionResult<Model.NotifikacijaEkspert> Insert([FromBody] NotifikacijaEkspertUpsertRequest request)
        {
            var entity = _mapper.Map<Database.NotifikacijaEkspert>(request);
            if(entity != null)
            {
                entity.Vrijeme = DateTime.Now;
                _context.NotifikacijeEksperti.Add(entity);
                _context.SaveChanges();
                return _mapper.Map<Model.NotifikacijaEkspert>(entity);
            } else
            {
                return BadRequest();
            }
        }
    }
}