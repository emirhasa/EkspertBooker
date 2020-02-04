﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkspertBooker.WebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkspertBooker.WebAPI.Controllers
{
    public class BaseCRUDController<TModel, TSearch, TInsert, TUpdate> : BaseGetController<TModel, TSearch>
    {
        protected readonly IBaseCRUDService<TModel, TSearch, TInsert, TUpdate> _service = null;

        public BaseCRUDController(IBaseCRUDService<TModel, TSearch, TInsert, TUpdate> service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual ActionResult<TModel> Insert(TInsert request)
        {
            return _service.Insert(request);
        }

        [HttpPut("{id}")]
        public virtual ActionResult<TModel> Update(int id, [FromBody]TUpdate request)
        {
            return _service.Update(id, request);
        }

        [HttpDelete("{id}")]
        public virtual ActionResult<bool> Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}