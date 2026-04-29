using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using quickassist.Model;
using quickassist.Repository.DataReport;
using quickassist.Services.QuestPDF;
using quickassist.Utils;
using System.Reflection;

namespace quickassist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataReportController : ControllerBase
    {
        private readonly IDataReportRepository _dataReportRepository;

        public DataReportController(IDataReportRepository dataReportRepository)
        {
            _dataReportRepository = dataReportRepository;
        }


        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetEmergencyByEventPDF(
            [FromRoute] int eventId,
            [FromQuery] string eventName,
            [FromQuery] int limit, 
            [FromQuery] int offset
            )
        {
            try
            {
                IEnumerable<EmergencyProfileModel> models = await _dataReportRepository.GetEmergencyDataByEventIdAsync(eventId, limit, offset);

                if (models is null)
                    return NotFound("No data found for the specified emergency ID.");



                // Configuramos para usar, por ejemplo, 4 procesos simultáneos 
                // (Depende de los núcleos de tu servidor)
                var options = new ParallelOptions
                {
                    MaxDegreeOfParallelism = 2
                };

                await Parallel.ForEachAsync(models, options, async (reportData, token) =>
                {

                    reportData.drugs = await _dataReportRepository.GetDrugsDataByEmergencyIdAsync(reportData.emergency_id);

                    // 1. Generar el PDF (usando QuestPDF, iText, etc.)
                    //byte[] pdfBytes = await _pdfService.GenerateSinglePdfAsync(reportData);
                    var pdfBytes = new FormEmergency1(reportData).GeneratePdf();

                    // 2. Guardar o Subir (Ej: Azure Blob Storage o FileSystem)
                    string fileName = $"{eventName}-{reportData.emergency_id}-{reportData.document}-{reportData.date_created:yyyy-MM-dd}.pdf";
                    fileName = CsnFunctions.SanitizeFileName(fileName);
                    
                    await SavePdfToFileSystemAsync(pdfBytes, fileName);

                });

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message} | {ex?.InnerException?.Message}");
            }
        }



        [HttpGet("{emergencyId}/pdf")]
        public async Task<IActionResult> GetEmergencyAndProfilePDF([FromRoute] int emergencyId)
        {
            try
            {
                IEnumerable<EmergencyProfileModel> models = await _dataReportRepository.GetEmergencyAndProfileDataAsync(emergencyId);

                EmergencyProfileModel? model = models.FirstOrDefault();

                if(model is null)
                    return NotFound("No data found for the specified emergency ID.");

                model.drugs = await _dataReportRepository.GetDrugsDataByEmergencyIdAsync(model.emergency_id);

                IDocument document = new FormEmergency1(model);

                document.GeneratePdfAndShow();
                return Ok();
                //using var stream = new MemoryStream();

                //document.GeneratePdf(stream);

                //return File(stream.ToArray(), "application/pdf", $"emergency-report-{emergencyId}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message} | {ex?.InnerException?.Message}");
            }
        }



        [HttpGet("{emergencyId}/json")]
        public async Task<IActionResult> GetEmergencyAndProfileData([FromRoute] int emergencyId)
        {
            try
            {
                var data = await _dataReportRepository.GetEmergencyAndProfileDataAsync(emergencyId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving data.");
            }
        }



        private async Task SavePdfToFileSystemAsync(byte[] pdfBytes, string eventName, string fileName)
        {
            // 1. Definir la ruta base (Ej: una carpeta "Reports" en la raíz del proyecto)
            // Path.Combine se encarga de manejar los separadores / o \ según el SO (Windows/Linux)
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "EmergencyReports", CsnFunctions.SanitizeFileName(eventName));

            // 2. Asegurarse de que el directorio existe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // 3. Combinar la carpeta con el nombre del archivo
            string filePath = Path.Combine(folderPath, fileName);

            // 4. Escritura asíncrona de los bytes en el disco
            await System.IO.File.WriteAllBytesAsync(filePath, pdfBytes);
        }
    }
}
