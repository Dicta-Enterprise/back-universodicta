using Back_UniversoDicta.Infraestructure.Hash;
using Back_UniversoDicta.Models.Users;
using Back_UniversoDicta.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back_UniversoDicta.Application.auth
{
    public class CreateUserUseCase
    {
        public bool EspecificValidation(Users users)
        {
            bool isUsuario = verificarUsuarioExistente(users.email);
            if (!isUsuario)
            {
                Hash hash = new Hash();
                String newPassword = hash.EncryptStringToBytes_Aes(users.password);

                users.password = newPassword;

                UsuarioReposiroty usuarioReposiroty = new UsuarioReposiroty();
                if (usuarioReposiroty.RegistarUsuario(users))
                {
                    return true;
                }
            }
            return false;
        }

        public bool verificarUsuarioExistente(string email)
        {
            UsuarioReposiroty usuarioReposiroty = new UsuarioReposiroty();
            Users users = usuarioReposiroty.LoginUsuario(email);
            if (users == null) return false;
            return true;

        }
    }
}