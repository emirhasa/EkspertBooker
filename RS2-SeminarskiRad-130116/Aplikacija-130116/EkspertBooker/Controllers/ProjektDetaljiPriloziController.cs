using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Database;
using EkspertBooker.WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace EkspertBooker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProjektDetaljiPriloziController : ControllerBase
    {
        EkspertBookerContext _context;
        IMapper _mapper;
        public ProjektDetaljiPriloziController(IBaseCRUDService<Model.ProjektDetaljiPrilog, object, ProjektDetaljiPrilogUpsertRequest, ProjektDetaljiPrilogUpsertRequest> service, EkspertBookerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ProjektDetaljiPrilogSearchRequest request)
        {
            //todo: try catch blockovi, return helper metode u slucaju exceptiona
            if (request.GetOnlyMetaData)
            {
                if (request.ProjektDetaljiId.HasValue)
                {
                    var prilog = _context.ProjektDetaljiPrilozi.Select(p => new PrilogMetaDataDTO
                    {
                        ProjektDetaljiId = p.ProjektDetaljiId,
                        PrilogEkstenzija = p.PrilogEkstenzija,
                        PrilogNaziv = p.PrilogNaziv,
                        UploadVrijeme = p.UploadVrijeme
                    }).Where(p => p.ProjektDetaljiId == request.ProjektDetaljiId).SingleOrDefault();
                    PrilogMetaDataDTO prilog_single = prilog;
                    return Ok(prilog_single);
                } else
                {
                    var prilozi = _context.ProjektDetaljiPrilozi
                    .Select(p => new PrilogMetaDataDTO
                    {
                        PrilogEkstenzija = p.PrilogEkstenzija,
                        PrilogNaziv = p.PrilogNaziv,
                        ProjektDetaljiId = p.ProjektDetaljiId,
                        UploadVrijeme = p.UploadVrijeme
                    }).ToList();
                    List<PrilogMetaDataDTO> prilozi_list = prilozi;
                    return Ok(prilozi_list);
                }
            }
            else
            {
                if (request.ProjektDetaljiId == null) return BadRequest();
                var entity = _context.ProjektDetaljiPrilozi.Include(pdp => pdp.ProjektDetalji).ThenInclude(pd => pd.Projekt).Where(pd => pd.ProjektDetaljiId == request.ProjektDetaljiId).SingleOrDefault();
                if (entity == null) return NotFound();
                var dataStream = new MemoryStream(entity.Prilog);
                string fileName = entity.PrilogNaziv;
                string fileExtension = entity.PrilogEkstenzija;
                return File(dataStream, "application/octet-stream", fileName + fileExtension);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Model.ProjektDetaljiPrilog> Update(int id, [FromBody]ProjektDetaljiPrilogUpsertRequest request)
        {
            var provjera = _context.ProjektDetalji.Find(id);
            if (provjera == null) return NotFound();

            var temp_entity = _mapper.Map<Database.ProjektDetaljiPrilog>(request);
            if (string.IsNullOrWhiteSpace(temp_entity.PrilogNaziv))
            temp_entity.PrilogNaziv = "EkspertBookerPrilog_" + DateTime.Now.ToString("ddMMyyyy-HH:mm:ss");
            temp_entity.UploadVrijeme = DateTime.Now;

            var entity = _context.ProjektDetaljiPrilozi.Find(id);
            if (entity != null)
            {
                _context.ProjektDetaljiPrilozi.Remove(entity);
                _context.SaveChanges();
                _context.ProjektDetaljiPrilozi.Add(temp_entity);
                _context.SaveChanges();
                temp_entity.Prilog = null;
                return _mapper.Map<Model.ProjektDetaljiPrilog>(entity);
            }
            else
            {
                _context.ProjektDetaljiPrilozi.Add(temp_entity);
                _context.SaveChanges();
                temp_entity.Prilog = null;
                return _mapper.Map<Model.ProjektDetaljiPrilog>(temp_entity);
            }
        }
    }
}