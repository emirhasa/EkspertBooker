using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Service
{
    public interface IStanjaService
    {
        List<Model.Stanje> Get();
        Model.Stanje GetById(string stanjeId);
    }
}
