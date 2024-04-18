using Back_UniversoDicta.Models.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Back_UniversoDicta.Repositories.User
{
    public class UsuarioReposiroty
    {
        string conexion = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
        public bool RegistarUsuario(Users users)
        {
            using (SqlConnection connection = new SqlConnection(conexion))
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlCommand.CommandText = "registrarUsuario";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@username", users.username);
                sqlCommand.Parameters.AddWithValue("@email", users.email);
                sqlCommand.Parameters.AddWithValue("@password", users.password);
                sqlCommand.Parameters.AddWithValue("@fechaDecreacion", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@estado", users.estado);
                sqlCommand.Parameters.AddWithValue("@idRol", users.idRol);
                try
                {
                    if (sqlCommand.ExecuteNonQuery() == 1)
                    {
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    sqlCommand.Parameters.Clear();
                    connection.Close();
                }

            }

            return false;
        }

        public Users LoginUsuario(string email)
        {
            using (SqlConnection connection = new SqlConnection(conexion))
            {
                SqlCommand sqlCommand = connection.CreateCommand();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlCommand.CommandText = "login_usuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@email", email);
                try
                {
                    connection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.Read())
                        {

                            return new Users
                            {
                                idUsuario = Convert.ToInt32(sqlDataReader["idUsuario"]),
                                username = sqlDataReader["username"].ToString(),
                                email = sqlDataReader["email"].ToString(),
                                password = sqlDataReader["password"].ToString(),
                                estado = Convert.ToBoolean(sqlDataReader["estado"]),
                                idRol = Convert.ToInt32(sqlDataReader["idRol"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    sqlCommand.Parameters.Clear();
                    connection.Close();
                }

            }
            return null;
        }
        public List<Users> visualizarUsuarios()
        {
            List<Users> usuarios = new List<Users>();
            using (SqlConnection connection = new SqlConnection(conexion))
            {
                SqlCommand sqlCommand = connection.CreateCommand();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlCommand.CommandText = "VisualizarUsuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            Users users = new Users
                            {
                                idUsuario = Convert.ToInt32(sqlDataReader["idUsuario"]),
                                email = sqlDataReader["email"].ToString(),
                                username = sqlDataReader["username"].ToString(),
                                password = sqlDataReader["password"].ToString(),
                                estado = Convert.ToBoolean(sqlDataReader["estado"]),
                                idRol = Convert.ToInt32(sqlDataReader["idRol"]),
                            };
                            usuarios.Add(users);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    sqlCommand.Parameters.Clear();
                    connection.Close();
                }
            }

            return usuarios;
        }
    }
}