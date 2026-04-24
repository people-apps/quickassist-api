using quickassist.Model;

namespace quickassist.Repository.DataReport
{
    public interface IDataReportRepository
    {
        /// <summary>
        /// Método encargado de recuperar la información de emergencia y perfil de un usuario a través de una consulta SQL utilizando Dapper.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="emergencyId"></param>
        /// <returns></returns>
        Task<IEnumerable<EmergencyProfileModel>> GetEmergencyAndProfileDataAsync(int emergencyId);
    }
}
