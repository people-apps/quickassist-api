namespace quickassist.Model
{
    public class DrugsModel
    {
        public int drug_id { get; init; }
        public long emergency_id { get; init; }

        // Detalles del Medicamento
        public string description { get; init; } = string.Empty;
        public string dose { get; init; } = string.Empty;
        public string via { get; init; } = string.Empty;
        public string results { get; init; } = string.Empty;
        public string medication_time { get; init; } = string.Empty;

        // Auditoría
        public DateTime date_created { get; init; }
        public long created_by { get; init; }
    }
}
