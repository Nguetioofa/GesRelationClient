using GesRelationClient.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;



namespace GesRelationClient.Services
{
    public class ClientsServices : IRepositorie<Client>
    {
        private readonly DbConnectionManager _dbConnectionManager;

        public ClientsServices(DbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task<bool> Create(Client entity)
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {

                    var parameters = new
                    {
                        entity.NomClient,
                        entity.PrenomClient,
                        entity.Adresse,
                        entity.CodePostal,
                        entity.Pays,
                        entity.DateNaissance
                    };

                    var rowsAffected = await connection.ExecuteAsync("InsertClient", parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected > 0; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var parameters = new
                    {
                        ClientId = id
                    };

                    var rowsAffected = await connection.ExecuteAsync("DeleteClient", parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected > 0;
                }
            }
            catch 
            {
                return false;
            }
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var clients = await connection.QueryAsync<Client>("GetAllClients", commandType: CommandType.StoredProcedure);
                    return clients;
                }
            }
            catch
            {
                return Enumerable.Empty<Client>();
            }
        }

        public async Task<Client> GetById(int id)
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var parameters = new
                    {
                        ClientId = id
                    };

                    var client = await connection.QuerySingleOrDefaultAsync<Client>("GetClientById", parameters, commandType: CommandType.StoredProcedure);
                    return client;
                }
            }
            catch
            {
                
                return new Client();
            }
        }

        public async Task<IEnumerable<Client>> GetPaged(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var parameters = new
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    };

                    var clients = await connection.QueryAsync<Client>("GetPagedClients", parameters, commandType: CommandType.StoredProcedure);
                    return clients;
                }
            }
            catch
            {
                return Enumerable.Empty<Client>();
            }
        }

        public async Task<bool> Update(Client entity)
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var parameters = new
                    {
                        entity.ClientId,
                        entity.NomClient,
                        entity.PrenomClient,
                        entity.Adresse,
                        entity.CodePostal,
                        entity.Pays,
                        entity.DateNaissance
                    };

                    var rowsAffected = await connection.ExecuteAsync("UpdateClient", parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected > 0;
                }
            }
            catch 
            {
                return false;
            }
        }

        public async Task<int> GetTotal()
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var totalAppels = await connection.ExecuteScalarAsync<int>("GetTotalClients", commandType: CommandType.StoredProcedure);
                    return totalAppels;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
