using EmpresaEntregas.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EmpresaEntregas.Controllers
{
    public class DBController
    {
        List<Funcionarios> funcionarios = new List<Funcionarios>();
        List<Encomendas> encomendas = new List<Encomendas>();
        public List<Funcionarios> GetFuncionarios()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "root",
                Password = "",
                Database = "dbdld",
            };

            using var conn = new MySqlConnection(builder.ConnectionString);

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from funcionarios", conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Funcionarios funcionario = new Funcionarios();
                funcionario.nfunc = Convert.ToInt32(reader["nfunc"]);
                funcionario.nome = reader["nome"].ToString()!;
                funcionario.email = reader["email"].ToString()!;
                funcionario.password = reader["password"].ToString()!;
                funcionario.dispositivo = reader["dispositivo"].ToString()!;
                funcionario.expira = reader["expira"].ToString()!;

                funcionarios.Add(funcionario);
            }
            reader.Close();

            conn.Close();
            return funcionarios;
        }

        public List<Encomendas> GetEncomendas()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "root",
                Password = "",
                Database = "dbdld",
            };

            using var conn = new MySqlConnection(builder.ConnectionString);

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from encomendas", conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Encomendas encomenda = new Encomendas();
                encomenda.nencomenda = reader["nencomenda"].ToString()!;
                encomenda.nfunc = Convert.ToInt32(reader["nfunc"]);
                encomenda.estado = reader["estado"].ToString()!;
                encomenda.destino = reader["destino"].ToString()!;

                encomendas.Add(encomenda);
            }
            reader.Close();

            conn.Close();
            return encomendas;
        }
    }
}
