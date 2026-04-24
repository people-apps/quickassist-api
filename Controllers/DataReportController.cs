using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using quickassist.Model;
using quickassist.Repository.DataReport;
using quickassist.Services.QuestPDF;
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


        [HttpGet("{emergencyId}/pdf")]
        public async Task<IActionResult> GetEmergencyAndProfilePDF([FromRoute] int emergencyId)
        {
            try
            {
                IEnumerable<EmergencyProfileModel> models = await _dataReportRepository.GetEmergencyAndProfileDataAsync(emergencyId);

                EmergencyProfileModel? model = models.FirstOrDefault();

                if(model is null)
                    return NotFound("No data found for the specified emergency ID.");

                
                IDocument document = new FormEmergency1(model);
                
                //document.GeneratePdfAndShow();
               
                using var stream = new MemoryStream();

                document.GeneratePdf(stream);

                return File(stream.ToArray(), "application/pdf", $"emergency-report-{emergencyId}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving data.");
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

    }
}
