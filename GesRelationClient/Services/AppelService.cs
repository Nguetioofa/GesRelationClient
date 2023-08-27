using GesRelationClient.Models;
using System.Data;
using Dapper;
using static Dapper.SqlMapper;


namespace GesRelationClient.Services
{
    public class AppelService : IRepositorie<Appel>
    {
        private readonly DbConnectionManager _dbConnectionManager;

        public AppelService(DbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task<bool> Create(Appel entity)
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var parameters = new
                    {
                        entity.ClientId,
                        entity.DateAppel,
                        entity.Objet,
                        entity.Description
                    };

                    var rowsAffected = await connection.ExecuteAsync("InsertAppelClient", parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected > 0;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
                        AppelId = id
                    };

                    var rowsAffected = await connection.ExecuteAsync("DeleteAppel", parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected > 0;
                }
            }
            catch 
            {
                return false;
            }

        }

        public async Task<IEnumerable<Appel>> GetAll()
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var appels = await connection.QueryAsync<Appel>("GetAllAppels", commandType: CommandType.StoredProcedure);
                    return appels;
                }
            }
            catch
            {
                return Enumerable.Empty<Appel>();
            }
        }

        public async Task<Appel> GetById(int id)
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var parameters = new
                    {
                        AppelId = id
                    };

                    var appel = await connection.QuerySingleOrDefaultAsync<Appel>("GetAppelById", parameters, commandType: CommandType.StoredProcedure);
                    return appel;
                }
            }
            catch 
            {
                return new Appel();
            }
        }

        public async Task<IEnumerable<Appel>> GetPaged(int pageNumber = 1, int pageSize = 10)
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

                    var appels = await connection.QueryAsync<Appel>("GetPagedAppels", parameters, commandType: CommandType.StoredProcedure);
                    return appels;
                }
            }
            catch
            {
                return  Enumerable.Empty<Appel>();
            }

        }

        public async Task<int> GetTotal()
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var totalClients = await connection.ExecuteScalarAsync<int>("GetTotalAppelsClients", commandType: CommandType.StoredProcedure);
                    return totalClients;
                }
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> Update(Appel entity)
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var parameters = new
                    {
                        entity.AppelId,
                        entity.ClientId,
                        entity.DateAppel,
                        entity.Objet,
                        entity.Description
                    };

                    var rowsAffected = await connection.ExecuteAsync("UpdateAppelClient", parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
