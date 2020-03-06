using AutoMapper;
using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Service
{
    public class ProjektDetaljiService : BaseCRUDService<Model.ProjektDetalji, ProjektDetaljiSearchRequest, ProjektDetaljiUpsertRequest, ProjektDetaljiUpsertRequest, Database.ProjektDetalji>
    {
        public ProjektDetaljiService(EkspertBookerContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.ProjektDetalji> Get(ProjektDetaljiSearchRequest search)
        {
            if(search.ProjektId.HasValue)
            {
                var projekt_detalj = _context.ProjektDetalji.Where(pd => pd.ProjektId == search.ProjektId).SingleOrDefault();
                List<Model.ProjektDetalji> list = new List<Model.ProjektDetalji>();
                var model_projekt_detalj = _mapper.Map<Model.ProjektDetalji>(projekt_detalj);
                list.Add(model_projekt_detalj);
                return list;
            } else
            {
                List<Model.ProjektDetalji> list = _mapper.Map<List<Model.ProjektDetalji>>(_context.ProjektDetalji.ToList());
                return list;
            }
        }

        public override Model.ProjektDetalji GetById(int id)
        {
            return base.GetById(id);
        }

    }
}
