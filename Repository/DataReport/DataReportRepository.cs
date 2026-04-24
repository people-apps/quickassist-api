using Dapper;
using quickassist.Model;
using quickassist.Services.Dapper;

namespace quickassist.Repository.DataReport
{
    public class DataReportRepository : IDataReportRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public DataReportRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }


        /// <summary>
        /// Método encargado de recuperar la información de emergencia y perfil de un usuario a través de una consulta SQL utilizando Dapper.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="emergencyId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EmergencyProfileModel>> GetEmergencyAndProfileDataAsync(int emergencyId)
        {
            using var connection = _sqlConnectionFactory.Create();
            
            var sql = string.Format(SqlQuery.EmergencyAndProfileQuery, emergencyId);

            return await connection.QueryAsync<EmergencyProfileModel>(sql);
        }


    }
}
