using EkspertBooker.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Service
{
    public interface IEkspertiKategorijePretplateService
    {
        List<Model.EkspertKategorijaPretplata> Get(EkspertKategorijeSearchRequest request);
        Model.EkspertKategorijaPretplata GetById(int id);
        Model.EkspertKategorijaPretplata Insert(EkspertKategorijaInsertRequest request);
        List<Model.EkspertKategorijaPretplata> Update(int id, EkspertKategorijeUploadRequest request);
        bool Delete(int id);
    }
}
