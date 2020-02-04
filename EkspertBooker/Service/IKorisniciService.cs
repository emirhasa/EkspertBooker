using AutoMapper;
using EkspertBooker.WebAPI.Database;
using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkspertBooker.Model.Requests;

namespace EkspertBooker.WebAPI.Service
{
    public interface IKorisniciService : IBaseCRUDService<Model.Korisnik, KorisniciSearchRequest, KorisnikUpsertRequest, KorisnikUpsertRequest>
    {
        new Model.Korisnik Insert(KorisnikUpsertRequest request);
        Model.Korisnik Authenticate(string username, string password);
    }
}
