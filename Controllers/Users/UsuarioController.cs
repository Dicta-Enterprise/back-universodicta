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
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("api/Users/VisualizarUsuarios")]
        public IHttpActionResult Get()
        {
            GestorUsuario gestorUsuario = new GestorUsuario();
            List<Back_UniversoDicta.Models.Users.Users> users = gestorUsuario.VisualizarUsuarios();

            return Ok(users);

        }

        // GET: api/Usuario/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Usuario
        public void Post([FromBody]string value)
        {
        }

        [HttpPost]
        [Route("api/Users/RegistrarUsuarios")]
        public IHttpActionResult Post([FromBody] Back_UniversoDicta.Models.Users.Users users)
        {
            GestorUsuario gestorUsuario = new GestorUsuario();
            Responsive res = gestorUsuario.RegistarUsuario(users);

            if (!res.ok)
            {
                return BadRequest(res.mensaje);
            }

            return Ok(res);
        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
        }
    }
}
