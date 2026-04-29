namespace quickassist.Model
{
    public record EmergencySignsModel
    {
        public int emergency_signs_id { get; init; }
        public long emergency_id { get; init; }

        // Mediciones y Signos (Strings con inicialización segura)
        public string hour { get; init; } = string.Empty;
        public string p_a { get; init; } = string.Empty; // Presión Arterial
        public string f_r { get; init; } = string.Empty; // Frecuencia Respiratoria
        public string f_c { get; init; } = string.Empty; // Frecuencia Cardíaca
        public string t_2 { get; init; } = string.Empty; // Temperatura / O2
        public string glycemia { get; init; } = string.Empty;
        public string oximetry { get; init; } = string.Empty;
        public string rhythm_def { get; init; } = string.Empty;
        public string results { get; init; } = string.Empty;

        // Auditoría
        public DateTime date_created { get; init; }
        public long created_by { get; init; }
    }
}
