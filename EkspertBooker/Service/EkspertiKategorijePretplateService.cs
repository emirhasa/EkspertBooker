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
    public class EkspertiKategorijePretplateService : IEkspertiKategorijePretplateService
    {
        protected readonly EkspertBookerContext _context;
        protected readonly IMapper _mapper;

        public EkspertiKategorijePretplateService(EkspertBookerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool Delete(int id)
        {
            try
            {
                var entity = _context.EkspertiKategorijePretplate.Find(id);
                _context.EkspertiKategorijePretplate.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Model.EkspertKategorijaPretplata> Get(EkspertKategorijeSearchRequest request)
        {
            //try
            //{
                var pretplate = _context.EkspertiKategorijePretplate.AsQueryable();

                if (request.EkspertId.HasValue)
                {
                    pretplate = pretplate.Where(ekp => ekp.EkspertId == request.EkspertId);
                }

                if (request.KategorijaId.HasValue)
                {
                    pretplate = pretplate.Where(ekp => ekp.KategorijaId == request.KategorijaId);
                }

                var result = pretplate.ToList();
                return _mapper.Map<List<Model.EkspertKategorijaPretplata>>(pretplate);
            //} 
            //catch
            //{
            //    throw;
            //}
        }

        public Model.EkspertKategorijaPretplata GetById(int id)
        {
            var pretplata = _context.EkspertiKategorijePretplate.Find(id);
            return _mapper.Map<Model.EkspertKategorijaPretplata>(pretplata);
        }

        public List<Model.EkspertKategorijaPretplata> Update(int id, EkspertKategorijeUploadRequest request)
        {
            //only has update as technically every insert is updating(replacing) the current state
            //still, if there is a need to have a list of users subscribe to a category through a request, this one does not implement it so it should be
            //done differently, however currently it's not deemed necessary
            var trenutne_pretplate = _context.EkspertiKategorijePretplate.Where(ekp=>ekp.EkspertId == id).ToList();
            foreach(var pretplata in trenutne_pretplate)
            {
                //izbrisi trenutne
                _context.EkspertiKategorijePretplate.Remove(pretplata);
            }

            foreach(var kategorijaId in request.EkspertKategorije)
            {
                _context.EkspertiKategorijePretplate.Add(new Database.EkspertKategorijaPretplata
                {
                    //dodaj nove
                    EkspertId = id,
                    KategorijaId = kategorijaId
                }); 
            }
            _context.SaveChanges();
            var lista_pretplata = _context.EkspertiKategorijePretplate.Where(ekp => ekp.EkspertId == id).ToList();
            return _mapper.Map<List<Model.EkspertKategorijaPretplata>>(lista_pretplata);
        }

        public Model.EkspertKategorijaPretplata Insert(EkspertKategorijaInsertRequest request)
        {
            var entity = _mapper.Map<Database.EkspertKategorijaPretplata>(request);
            _context.EkspertiKategorijePretplate.Add(entity);
            var result = _mapper.Map<Model.EkspertKategorijaPretplata>(entity);
            return result;
        }
    }
}
