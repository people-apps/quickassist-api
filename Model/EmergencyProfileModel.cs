namespace quickassist.Model
{
    public record RightAndLeftDto
    {
        public string key { get; set; }
        public bool right { get; set; }
        public bool left { get; set; }
    }

    public record EmergencyProfileModel
    {
        // Identificadores
        public int emergency_id { get; init; }
        public Guid emergency_uuid { get; init; }
        public long profile_id { get; init; }
        public string blood_type { get; init; } = string.Empty;
        public string address { get; init; } = string.Empty;
        public string responsible_name { get; init; } = string.Empty;
        public string responsible_phone { get; init; } = string.Empty;

        // Evento y Tiempos
        public long event_id { get; init; }
        public string time_at_scene { get; init; } = "00:00";
        public string time_out_scene { get; init; } = "00:00";
        public string is_translated { get; init; } = "NO";
        public string hospital_translated { get; init; } = string.Empty;
        public string time_at_hospital { get; init; } = "00:00";
        public string time_out_hospital { get; init; } = "00:00";

        // Personal
        public string pilots { get; init; } = string.Empty;
        public string main_medicals { get; init; } = string.Empty;

        // Estados Físicos (Booleanos)
        public bool is_conscious { get; init; }
        public bool is_unconscious { get; init; }
        public bool is_disoriented { get; init; }
        public bool is_seizing { get; init; }
        public bool is_skin_dry { get; init; }
        public bool is_skin_diaphoretic { get; init; }
        public bool is_skin_color_not_applied { get; init; }
        public bool is_skin_color_rash { get; init; }
        public bool is_skin_color_cyanotic { get; init; }
        public bool is_skin_color_pale { get; init; }
        public bool temperature_not_applied { get; init; }
        public bool temperature_cold { get; init; }
        public bool temperature_hot { get; init; }

        // Antecedentes
        public bool history_asthma { get; init; }
        public bool history_bronchitis { get; init; }
        public bool history_overweight { get; init; }
        public bool history_diabetes { get; init; }
        public bool history_cardiac { get; init; }
        public bool history_hta { get; init; }
        public bool history_others { get; init; }

        // Pulmones
        public bool long_right_clear { get; init; }
        public bool long_right_absent { get; init; }
        public bool long_right_crackling { get; init; }
        public bool long_right_ronchi { get; init; }
        public bool long_right_wheezing { get; init; }
        public bool long_left_clear { get; init; }
        public bool long_left_absent { get; init; }
        public bool long_left_crackling { get; init; }
        public bool long_left_ronchi { get; init; }
        public bool long_left_wheezing { get; init; }

        // Pupilas
        public bool pupils_right_pupiliform { get; init; }
        public bool pupils_right_medium { get; init; }
        public bool pupils_right_dilated { get; init; }
        public bool pupils_right_react_light { get; init; }
        public bool pupils_right_not_responing { get; init; }
        public bool pupils_right_not_anisocoria { get; init; }
        public bool pupils_left_pupiliform { get; init; }
        public bool pupils_left_medium { get; init; }
        public bool pupils_left_dilated { get; init; }
        public bool pupils_left_react_light { get; init; }
        public bool pupils_left_not_responing { get; init; }
        public bool pupils_left_not_anisocoria { get; init; }

        // Otros estados
        public bool eyes_uneven { get; init; }
        public bool eyes_constricted { get; init; }
        public bool eyes_dilated { get; init; }
        public bool eyes_normal { get; init; }
        public bool patient_contractures { get; init; }
        public bool patient_bleeding { get; init; }
        public bool patient_amputated { get; init; }
        public bool patient_oxygen { get; init; }
        public bool patient_paralysis { get; init; }
        public bool patient_vomiting { get; init; }

        // Tratamientos
        public bool treatment_rccp { get; init; }
        public bool treatment_monitor { get; init; }
        public bool treatment_defibrillator { get; init; }
        public bool treatment_cannulation { get; init; }
        public bool treatment_dressings { get; init; }
        public bool treatment_ext_fixation { get; init; }
        public bool treatment_irrigation { get; init; }
        public bool treatment_spinal_inmobi { get; init; }
        public bool treatment_child_birth { get; init; }
        public bool treatment_breath_oxygen { get; init; }
        public bool treatment_breath_aspiration { get; init; }
        public bool treatment_breath_intubation { get; init; }
        public bool treatment_breath_anbu { get; init; }
        public bool treatment_breath_failed { get; init; }
        public bool treatment_breath_ventilator { get; init; }

        // Escalas y Notas
        public int glascow_eyes_scala { get; init; }
        public int glascow_motor_scala { get; init; }
        public int glascow_verbal_scala { get; init; }
        public string clinical_history { get; init; } = string.Empty;
        public string signs_and_symptopms { get; init; } = string.Empty;
        public string allergies { get; init; } = string.Empty;
        public string physical_exam { get; init; } = string.Empty;
        public string treatment { get; init; } = string.Empty;
        public string url_sign { get; init; } = string.Empty;

        // Auditoría
        public int created_by { get; init; }
        public DateTime date_created { get; init; }
        public DateTime date_modified { get; init; }
        public long event_location { get; init; }
        public long emergency_status { get; init; }

        // Perfil (Relacionados)
        public Guid profile_uuid { get; init; }
        public string birthday { get; init; } = string.Empty;
        public string country { get; init; } = string.Empty;
       
        public string document_type { get; init; } = string.Empty;
        public string document { get; init; } = string.Empty;
        public string name { get; init; } = string.Empty;
        public string surname { get; init; } = string.Empty;
        public long profile_status { get; init; }
    }
}
