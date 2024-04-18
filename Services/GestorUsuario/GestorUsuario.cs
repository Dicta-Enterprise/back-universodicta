using Back_UniversoDicta.Application.auth;
using Back_UniversoDicta.Infraestructure.Responsive;
using Back_UniversoDicta.Models.Users;
using Back_UniversoDicta.Repositories.User;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Text;

namespace Back_UniversoDicta.Services.GestorUsuario
{
    public class GestorUsuario
    {
        private Responsive mensaje(dynamic mensaje, bool ok)
        {
            Responsive responsive = new Responsive();
            responsive.ok = ok;
            responsive.mensaje = mensaje;

            return responsive;
        }
        public Responsive RegistarUsuario(Users users)
        {
            CreateUserUseCase createUserUse = new CreateUserUseCase();

            bool usuarioExistente = createUserUse.verificarUsuarioExistente(users.email);
            if(usuarioExistente)
            {
                return mensaje("el usuario que esta registrando ya existe", false);
            }

            if (users.username.Contains(" ")||users.email.Contains(" ") || users.password.Contains(" "))

                return mensaje("El correo electrónico y la contraseña no deben contener espacios en blanco", false);

            if (users == null || string.IsNullOrEmpty(users.email)||string.IsNullOrWhiteSpace(users.email) || 
                string.IsNullOrEmpty(users.username) || string.IsNullOrWhiteSpace(users.username) ||
                 string.IsNullOrEmpty(users.password) || string.IsNullOrWhiteSpace(users.password)) return mensaje("Usuario vacío o con espacios en blanco", false);



            if (users.password != users.confirmarPassword) return mensaje("Las contraseñas no son iguales", false);



            string[] dominiosPermitidos = { "@gmail.com", "@outlook.com", "@hotmail.com" };
            if (!dominiosPermitidos.Any(dominio => users.email.EndsWith(dominio, StringComparison.OrdinalIgnoreCase)))
            {
                return mensaje("El correo electrónico debe ser de dominio @gmail.com, @outlook.com o @hotmail.com al registrarse", false);
            }
            users.idRol = 1;
            users.estado = true;



            bool dispararEjecucion = createUserUse.EspecificValidation(users);

            if (!dispararEjecucion) return mensaje("!Ups, contacte con el administrador encargado", false);

            string token = GenerarToken(users);

            return mensaje(token, true);
        }
        public Responsive LoginUsuario(Users users)
        {
            LoginUserUseCase loginUserUseCase = new LoginUserUseCase();
            if (users == null || string.IsNullOrEmpty(users.email)) return mensaje("Usuario vacio", false);

            string[] dominiosPermitidos = { "@gmail.com", "@outlook.com", "@hotmail.com" };

            if (!dominiosPermitidos.Any(dominio => users.email.EndsWith(dominio, StringComparison.OrdinalIgnoreCase)))
            {
                return mensaje("El correo electrónico debe ser de dominio @gmail.com, @outlook.com o @hotmail.com al iniciar sesión", false);
            }


            if (users.email.Contains(" ") || users.password.Contains(" "))

                return mensaje("El correo electrónico y la contraseña no deben contener espacios en blanco", false);

            Users usuarioRegistrado = loginUserUseCase.LoginUssuario(users);

            if (usuarioRegistrado == null)
                return mensaje("El usuario no existe o contraseña incorrecta", false);

            users.idRol = 3;
            users.estado = true;

            string token = GenerarToken(users);

            return mensaje(token, true);
        }

        public List<Users> VisualizarUsuarios()
        {
            UsuarioReposiroty usuarioReposiroty = new UsuarioReposiroty();

            try
            {
                List<Users> users = usuarioReposiroty.visualizarUsuarios();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // o maneja el error según tu lógica de aplicación
            }
        }

        private string GenerarToken(Users users)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, users.email),
                // Puedes agregar más claims según necesites
            };

            var keyString = "TuClaveSecretaSuperSecreta";
            var keyBytes = Encoding.UTF8.GetBytes(keyString.PadRight(32)); // Rellena la cadena si es necesario
            var key = new SymmetricSecurityKey(keyBytes);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "TuEmpresa",
                audience: "Clientes",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60), // Tiempo de expiración del token
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}