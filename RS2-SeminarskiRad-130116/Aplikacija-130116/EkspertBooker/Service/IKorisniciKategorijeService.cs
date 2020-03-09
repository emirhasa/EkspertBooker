using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using System.Collections.Generic;

namespace EkspertBooker.WebAPI.Service
{
    public interface IKorisniciKategorijeService
    {
        List<Model.KorisnikKategorija> Get(KorisniciKategorijeSearchRequest search);
        Model.KorisnikKategorija GetById(int id);
        List<Model.KorisnikKategorija> Update(int id, KorisnikKategorijeUpsertRequest request);
        bool Delete(int id);
    }
}