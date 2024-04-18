using Back_UniversoDicta.Infraestructure.Hash;
using Back_UniversoDicta.Infraestructure.Responsive;
using Back_UniversoDicta.Models.Users;
using Back_UniversoDicta.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back_UniversoDicta.Application.auth
{
    public class LoginUserUseCase
    {
        public Users LoginUssuario(Users users)
        {
            UsuarioReposiroty usuarioReposiroty = new UsuarioReposiroty();
            Users validateusuario = usuarioReposiroty.LoginUsuario(users.email);
            if (validateusuario == null) return null;
            Hash hash = new Hash();

            String hashPassword = hash.DecryptStringFromBytes_Aes(validateusuario.password);
            if (users.password != hashPassword) return null;
            return validateusuario;
        }

        
    }
}