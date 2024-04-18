using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Back_UniversoDicta.Infraestructure.Responsive;
using Back_UniversoDicta.Models.Users;
using Back_UniversoDicta.Services.GestorUsuario;

namespace Back_UniversoDicta.Controllers.Users
{
    public class IniciarsionController : ApiController
    {
        // GET: api/Iniciarsion
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Iniciarsion/5
        public string Get(int id)
        {
            return "value";
        }
        [HttpPost]
        [Route("api/Users/IniciarSesion")]
            public IHttpActionResult Post([FromBody] Back_UniversoDicta.Models.Users.Users users)
            {
                GestorUsuario gestorUsuario = new GestorUsuario();
                Responsive res = gestorUsuario.LoginUsuario(users);

                if (!res.ok)
                {
                    return BadRequest(res.mensaje);
                }
                return Ok(res);
            }
        

        // PUT: api/Iniciarsion/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Iniciarsion/5
        public void Delete(int id)
        {
        }
    }
}
