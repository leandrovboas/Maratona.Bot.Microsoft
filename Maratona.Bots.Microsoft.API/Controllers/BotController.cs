using Maratona.Bots.Microsoft.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Maratona.Bots.Microsoft.API.Controllers
{

    [Produces("application/json")]
    [Route("api/bot")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet("getLivrosBot")]
        public IEnumerable<Livros> Get()
        {
            return Livros.GetListaLivros();
        }

        // POST api/values
        [HttpPost("getLivrosCategoria")]
        public void Post([FromBody]string value)
        {
        }

    }
}
