using Back_UniversoDicta.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back_UniversoDicta.Models.Users
{
    public class Users
    {
        public int idUsuario { get; set; }
        public String username { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String confirmarPassword { get; set; }
        public bool estado { get; set; }
        public int idRol { get; set; }

        public Users() { }

        public Users(int IdUsuario, String Username, String Email, String Password, bool Estado, int IdRol)
        {
            idUsuario = IdUsuario;
            username = Username;
            email = Email;
            password = Password;
            estado = Estado;
            idRol = IdRol;
        }
        public Users(String Username, String Email, String Password, bool Estado, int IdRol)
        {
            username = Username;
            email = Email;
            password = Password;
            estado = Estado;
            idRol = IdRol;
        }
        public Users( String Email, String Password)
        {
            email = Email;
            password = Password;
            
        }
        public Users(String Email)
        {
            email = Email;

        }

    }
}