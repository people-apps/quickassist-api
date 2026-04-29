using quickassist.Model;

namespace quickassist.Repository.DataReport
{
    public interface IDataReportRepository
    {
        /// <summary>
        /// Método encargado de recuperar las drogas administradas en una emergencia.
        /// </summary>
        /// <param name="emergencyId"></param>
        /// <returns></returns>
        Task<IEnumerable<DrugsModel>> GetDrugsDataByEmergencyIdAsync(int emergencyId);



        /// <summary>
        /// Método encargado de recuperar la información de emergencia por evento.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Task<IEnumerable<EmergencyProfileModel>> GetEmergencyDataByEventIdAsync(int eventId, int limit, int offset);


        /// <summary>
        /// Método encargado de recuperar la información de emergencia y perfil de un usuario a través de una consulta SQL utilizando Dapper.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="emergencyId"></param>
        /// <returns></returns>
        Task<IEnumerable<EmergencyProfileModel>> GetEmergencyAndProfileDataAsync(int emergencyId);
    }
}
