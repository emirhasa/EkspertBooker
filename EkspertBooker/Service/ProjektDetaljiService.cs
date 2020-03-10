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
            List<Model.ProjektDetalji> list = _mapper.Map<List<Model.ProjektDetalji>>(_context.ProjektDetalji.ToList());
            return list; 
        }

        public override Model.ProjektDetalji GetById(int id)
        {
            return base.GetById(id);
        }

        public override Model.ProjektDetalji Insert(ProjektDetaljiUpsertRequest request)
        {
            //projekt detalji se automatski generiraju cim projekt postane aktivan. ne postoji insert, samo update
            throw new NotImplementedException();
        }


    }
}
