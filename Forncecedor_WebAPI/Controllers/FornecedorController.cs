using Forncecedor_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Forncecedor_WebAPI.Controllers
{
    public class FornecedorController : ApiController
    {
        private FornecedoresEntities ctx = new FornecedoresEntities();

        // GET api/<controller>
        [HttpGet]
        [ActionName("listagem")]
        public IEnumerable<FORNECEDOR> Get()
        {
            return ctx.FORNECEDOR.AsEnumerable();
        }

        // GET api/<controller>/5
        [HttpGet]
        [ActionName("busca")]
        public FORNECEDOR Get(int id)
        {
            var objFornecedor = ctx.FORNECEDOR.Where(f => f.ID_FORNECEDOR == id).FirstOrDefault();

            return objFornecedor;
        }

        // POST api/<controller>
        [HttpPost]
        [ActionName("incluir")]
        public void Post(FORNECEDOR objFornecedor)
        {
            ctx.FORNECEDOR.Add(objFornecedor);
            ctx.SaveChanges();
        }

        // PUT api/<controller>/5
        [HttpPost]
        [ActionName("alterar")]
        public void Put(FORNECEDOR objFornecedorAlterar)
        {
            var buscarFornecedor = ctx.FORNECEDOR.Where(f => f.ID_FORNECEDOR == objFornecedorAlterar.ID_FORNECEDOR).First();
            ctx.Entry(buscarFornecedor).CurrentValues.SetValues(objFornecedorAlterar);
            ctx.SaveChanges();
        }

        // DELETE api/<controller>/5
        [HttpGet]
        [ActionName("excluir")]
        public void Delete(int id)
        {
            var deletarFornecedor = ctx.FORNECEDOR.Where(f => f.ID_FORNECEDOR == id).First();
            ctx.FORNECEDOR.Remove(deletarFornecedor);
            ctx.SaveChanges();
        }
    }
}