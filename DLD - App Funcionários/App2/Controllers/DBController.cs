using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;
using App2.Models;

namespace App2.Controllers
{
    public class DBController
    {
        List<Funcionarios> funcionarios = new List<Funcionarios>();
        List<Encomendas> encomendas = new List<Encomendas>();
        public List<Funcionarios> GetFuncionarios()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "10.0.2.2",
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
                funcionario.nome = reader["nome"].ToString();
                funcionario.email = reader["email"].ToString();
                funcionario.password = reader["password"].ToString();
                funcionario.dispositivo = reader["dispositivo"].ToString();
                funcionario.expira = reader["expira"].ToString();

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
                Server = "10.0.2.2",
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
                encomenda.nencomenda = reader["nencomenda"].ToString();
                encomenda.nfunc = Convert.ToInt32(reader["nfunc"]);
                encomenda.estado = reader["estado"].ToString();
                encomenda.destino = reader["destino"].ToString();

                encomendas.Add(encomenda);
            }
            reader.Close();

            conn.Close();
            return encomendas;
        }

        public void SetDevice(int nfunc)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "10.0.2.2",
                UserID = "root",
                Password = "",
                Database = "dbdld",
            };

            using var conn = new MySqlConnection(builder.ConnectionString);

            conn.Open();

            MySqlCommand cmd = new MySqlCommand($"update funcionarios set dispositivo = '{Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId)}', expira = '{DateTimeOffset.Now.AddDays(10).ToUnixTimeSeconds()}' where nfunc = {nfunc};", conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Logout(int nfunc)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "10.0.2.2",
                UserID = "root",
                Password = "",
                Database = "dbdld",
            };

            using var conn = new MySqlConnection(builder.ConnectionString);

            conn.Open();

            MySqlCommand cmd = new MySqlCommand($"update funcionarios set expira = '0' where nfunc = {nfunc};", conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void AddEncomenda(int nfunc, string nencomenda, string estado, string destino)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "10.0.2.2",
                UserID = "root",
                Password = "",
                Database = "dbdld",
            };

            using var conn = new MySqlConnection(builder.ConnectionString);

            conn.Open();

            MySqlCommand cmd = new MySqlCommand($"insert into encomendas (nencomenda,nfunc,estado,destino) values ('{nencomenda}','{nfunc}','{estado}','{destino}');", conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void ChangePassword(int nfunc, string pass)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "10.0.2.2",
                UserID = "root",
                Password = "",
                Database = "dbdld",
            };

            using var conn = new MySqlConnection(builder.ConnectionString);

            conn.Open();

            MySqlCommand cmd = new MySqlCommand($"update funcionarios set password = '{pass}' where nfunc = {nfunc};", conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void ChangeEncomenda(string nencomenda, string address)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "10.0.2.2",
                UserID = "root",
                Password = "",
                Database = "dbdld",
            };

            using var conn = new MySqlConnection(builder.ConnectionString);

            conn.Open();

            MySqlCommand cmd = new MySqlCommand($"update encomendas set destino = '{address}' where nencomenda = '{nencomenda}';", conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void DeliverEncomenda(string nencomenda)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "10.0.2.2",
                UserID = "root",
                Password = "",
                Database = "dbdld",
            };

            using var conn = new MySqlConnection(builder.ConnectionString);

            conn.Open();

            MySqlCommand cmd = new MySqlCommand($"update encomendas set estado = 'Entregue' where nencomenda = '{nencomenda}';", conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
