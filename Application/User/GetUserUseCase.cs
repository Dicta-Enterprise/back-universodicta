using Back_UniversoDicta.Models.Users;
using Back_UniversoDicta.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back_UniversoDicta.Application.User
{
    public class GetUserUseCase
    {
        public List<Users> visualizarUsuarios()
        {
            UsuarioReposiroty usuarioReposiroty = new UsuarioReposiroty();
            List<Users> usuarios = usuarioReposiroty.visualizarUsuarios();

            return usuarios;
        }
    }
}