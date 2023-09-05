using System.Data;
using System.Data.SqlClient;
using Dapper;
using Domain.Model;

namespace Repository
{
    public class BancoFrameworkOperacoes
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BancoFramework;Integrated Security=True;Connect Timeout=30;Encrypt=False;";


        public Cliente GetById(int clientId)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT * FROM Clientes WHERE Id = @ClientId";
                return dbConnection.QueryFirstOrDefault<Cliente>(sqlQuery, new { ClientId = clientId });
            }
        }

        public void Insert(Cliente cliente)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Clientes (Id,Nome, Cpf, Saldo) VALUES (@Id, @Nome, @Cpf, @Saldo)";
                dbConnection.Execute(sqlQuery, cliente);
            }
        }

        public void UpdateSaldo(int clientId, float novoSaldo)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Clientes SET Saldo = @NovoSaldo WHERE Id = @ClientId";
                dbConnection.Execute(sqlQuery, new { NovoSaldo = novoSaldo, ClientId = clientId });
            }
        }

    }
}