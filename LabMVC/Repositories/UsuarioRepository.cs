using LabMVC.Services;
using LabWebForms.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace LabMVC.Repositories
{
    public class UsuarioRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexao"].ConnectionString;

        public List<Usuario> ListaUsuarios()
        {
            var usuarios = new List<Usuario>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nome, Email FROM Usuarios";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = reader.GetInt32(0);
                    usuario.Nome = reader.GetString(1);
                    usuario.Email = reader.GetString(2);
                    usuario.Login = reader.GetString(3);


                    usuarios.Add(usuario);
                }

                return usuarios;
            }
        }

        public Usuario BuscaLogin(string login, string senha)
        {
            var usuario = new Usuario();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nome, Email FROM Usuarios where login = @login and senha = @senha";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@senha", senha);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    usuario.Id = (int)reader["Id"];
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    //usuario.Login = reader["Login"].ToString();
                    usuario.Senha = reader["Senha"].ToString();    
                }

            }

            return usuario;
        }

        public void Salvar(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Usuarios (Nome, Email, Login, Senha) VALUES (@Nome, @Email, @Login, @Senha)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Login", usuario.Login);
                command.Parameters.AddWithValue("@Senha", usuario.Senha);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
