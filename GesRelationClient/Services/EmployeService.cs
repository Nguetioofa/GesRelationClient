using GesRelationClient.Models;
using System.Data;
using Dapper;

namespace GesRelationClient.Services
{
    public class EmployeService
    {
        private readonly DbConnectionManager _dbConnectionManager;

        public EmployeService(DbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public async Task<Employe> Authenticate(string username, string password)
        {
            try
            {
                using (var connection = _dbConnectionManager.GetOpenConnection())
                {
                    var parameters = new
                    {
                        NomUtilisateur = username,
                        MotDePasse = password
                    };

                    var result = await connection.QueryFirstOrDefaultAsync<Employe>("AuthenticateEmployee", parameters, commandType: CommandType.StoredProcedure);
                    
                    return result;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new();
            }
        }
    }
}
