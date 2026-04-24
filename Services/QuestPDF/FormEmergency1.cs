using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using quickassist.Model;

namespace quickassist.Services.QuestPDF
{
    public class FormEmergency1 : IDocument
    {

        public readonly EmergencyProfileModel Model;

        public FormEmergency1(EmergencyProfileModel model)
        {
            Model = model;
        }


        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                //page.Size(PageSizes.A4);
                page.Margin(0.2f, Unit.Centimetre);
                page.PageColor(Colors.White);
                //page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Verdana));

                // El Header ahora queda minimalista (solo si necesitas algo que SÍ se repita)
                // Si no necesitas nada repetido, puedes comentar o borrar page.Header()
                //page.Header().AlignRight().Text(x =>
                //{
                //    x.Span("No. 000001").FontColor("#7393B3").FontSize(8).SemiBold();
                //});

                page.Content().Column(col =>
                {
                    // --- AQUÍ MOVEMOS EL HEADER ANTERIOR ---
                    col.Item().Element(ComposeHeaderInformation);

                    // --- SECCIONES ANTERIORES DEL CONTENT ---
                    col.Item().PaddingTop(5).Element(ComposeContent);
                    //col.Item().PaddingTop(10).Element(ComposeCheckboxesSiNo);
                    //col.Item().PaddingTop(10).Element(ComposeSignosYGlasgow);
                    //col.Item().PaddingTop(10).Element(ComposeAutorizacionYFirmas);
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                });
            });
        }


        public void ComposeHeaderInformation(IContainer container)
        {
            container.Row(row =>
            {
                //// 1. Columna del Logo (Izquierda)
                row.RelativeItem(1).Border(0).AlignCenter().AlignMiddle().Column(col =>
                {
                    // Placeholder para el logo
                    col.Item().Width(120).Height(80).Placeholder();
                });

                // 2. Columna Central: INFORMACIÓN DEL PACIENTE
                row.RelativeItem(1.5f).Border(0).PaddingHorizontal(10).Column(col =>
                {
                    // Título con fondo negro y texto blanco
                    col.Item().Background(Colors.Black).Padding(2).AlignCenter().Text("INFORMACIÓN DEL PACIENTE")
                        .FontColor(Colors.White).FontSize(12).SemiBold();

                    // Contenedor de la tabla de datos
                    col.Item().Border(0.5f).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(0.8f); // Fecha Nac / Apellidos / etc
                            columns.RelativeColumn(0.4f); // Edad / Religión
                            columns.RelativeColumn(0.6f); // Género
                        });

                        // Fila 1: Fecha Nac, Edad, Género
                        table.Cell().BorderRight(0.5f).BorderBottom(0.5f).Padding(2).Column(c =>
                        {
                            c.Item().Text("FECHA NAC.:").FontSize(5).SemiBold();
                            c.Item().Text(Model.birthday).FontSize(7);
                        });
                        table.Cell().BorderRight(0.5f).BorderBottom(0.5f).Padding(2).Column(c =>
                        {
                            c.Item().Text("EDAD:").FontSize(5).SemiBold();
                            c.Item().Text($"").FontSize(7);

                        });
                        table.Cell().BorderBottom(0.5f).Padding(2).Column(c =>
                        {
                            c.Item().Text("GÉNERO: ").FontSize(5).SemiBold();
                            c.Item().Text("").FontSize(7);
                        });


                        //        // Filas de ancho completo (Spanning)
                        table.Cell().RowSpan(1).ColumnSpan(3).BorderBottom(0.5f).Padding(2).Column(c =>
                        {
                            c.Item().Text("APELLIDOS:").FontSize(5).SemiBold();
                            c.Item().Text(Model.surname).FontSize(7); // Espacio para escribir
                        });

                        table.Cell().RowSpan(1).ColumnSpan(3).BorderBottom(0.5f).Padding(2).Column(c =>
                        {
                            c.Item().Text("NOMBRES:").FontSize(5).Bold();
                            c.Item().Text(Model.name).FontSize(7);
                            //c.Item().Height(10);
                        });

                        table.Cell().RowSpan(1).ColumnSpan(3).BorderBottom(0.5f).Padding(2).Column(c =>
                        {
                            c.Item().Text($"NACIONALIDAD: {Model.country}").FontSize(6).SemiBold();
                            //c.Item().Text("Guatemalteco").FontSize(7);
                        });

                        table.Cell().RowSpan(1).ColumnSpan(3).BorderBottom(0.5f).Padding(2).Column(c =>
                        {
                            c.Item().Text($"Documento identificación: {Model.document}").FontSize(6).SemiBold();
                        });

                        table.Cell().RowSpan(1).ColumnSpan(1).Border(0.5f).Padding(2).Column(c =>
                        {
                            c.Item().Text("TIPO DE SANGRE:").FontSize(5).SemiBold();
                            c.Item().Text(Model.blood_type).FontSize(7);
                        });

                        table.Cell().RowSpan(1).ColumnSpan(2).Border(0.5f).Padding(2).Column(c =>
                        {
                            c.Item().Text("RELIGIÓN:").FontSize(5).SemiBold();
                            c.Item().Text("").FontSize(7);
                        });

                        table.Cell().RowSpan(1).ColumnSpan(3).Border(0.5f).Padding(2).Column(c =>
                        {
                            c.Item().Text("DIRECCIÓN:").FontSize(5).SemiBold();
                            c.Item().Text(Model.address).FontSize(7);
                        });
                    });
                });

                // 3. Columna Derecha: Tiempos, Traslado y Personal
                row.RelativeItem(1.5f).Border(0).PaddingLeft(5).Column(col =>
                {
                    col.Item().PaddingTop(5); // Alineación con el título central

                    // Función auxiliar para filas con líneas
                    void DrawLineField(string label)
                    {
                        col.Item().PaddingBottom(2).Row(r =>
                        {
                            r.AutoItem().Text(label).FontSize(7);
                            r.RelativeItem().BorderBottom(0.5f).Height(5);
                        });
                    }

                    DrawLineField("FECHA:  ");
                    DrawLineField($"HORA DE LLAMADA: ");
                    DrawLineField($"HORA DE LLEGADA A LA ESCENA:  {Model.time_at_scene}");
                    DrawLineField($"HORA DE SALIDA DE LA ESCENA:  {Model.time_out_scene}");
                    DrawLineField($"LUGAR/DIRECCIÓN:  ");

                    col.Item().PaddingTop(5).Text("TRASLADO").FontSize(8).SemiBold();
                    DrawLineField($"HOSPITAL DESTINO: {Model.hospital_translated}");
                    DrawLineField($"HORA DE LLEGADA AL HOSPITAL: {Model.time_at_hospital}");
                    DrawLineField($"HORA DE SALIDA DEL HOSPITAL: {Model.time_out_hospital}");

                    col.Item().Text("PERSONAL").FontSize(8).SemiBold();
                    DrawLineField($"PILOTO: {Model.pilots}");
                    DrawLineField("PARAMÉDICO:");
                });
            });
        }


        public void ComposeContent(IContainer container)
        {
            container.PaddingVertical(5).Column(col =>
            {

                // Título de la sección
                col.Item().Background(Colors.Black).Padding(1).AlignCenter().Text("RECORD DE TRATAMIENTO")
                    .FontColor(Colors.White).FontSize(9).ExtraBold();

                // Fila principal que contiene las 6 columnas de datos
                col.Item().PaddingTop(3).Border(0).Row(row =>
                {
                    // 1. ESTADO MENTAL Y COLOR DE PIEL
                    row.RelativeItem(0.9f).Border(0).Column(innerCol =>
                    {
                        //DrawCheckboxGroup(innerCol, "ESTADO MENTAL", new[] { "CONCIENTE", "DESORIENTADO", "INCONSCIENTE", "CONVULSIONANDO" });
                        DrawCheckboxGroupV2(innerCol, "ESTADO MENTAL", GetEstadoMentalOptions(Model));
                        innerCol.Item().PaddingTop(5);
                        //DrawCheckboxGroup(innerCol, "COLOR DE PIEL", new[] { "N.L", "CIANÓTICA", "PÁLIDO", "RASH" });
                        DrawCheckboxGroupV2(innerCol, "COLOR DE PIEL", GetColorPielOptions(Model));

                    });

                    // 2. PIEL Y TEMPERATURA
                    row.RelativeItem(0.8f).Border(0).Column(innerCol =>
                    {
                        //DrawCheckboxGroup(innerCol, "PIEL", new[] { "N.L", "SECA", "DIAFORÉTICA" });
                        DrawCheckboxGroupV2(innerCol, "PIEL", GetPielOptions(Model));
                        innerCol.Item().PaddingTop(5);
                        //DrawCheckboxGroup(innerCol, "TEMPERATURA PIEL", new[] { "N.L", "CALIENTE", "FRIO" });
                        DrawCheckboxGroupV2(innerCol, "TEMPERATURA PIEL", GetTemperaturaPielOptions(Model));
                    });

                    // 3. HISTORIA
                    row.RelativeItem(0.8f).Border(0).Column(innerCol =>
                    {
                        //DrawCheckboxGroup(innerCol, "HISTORIA", new[] { "ASMA", "BRONQUITIS", "ENFISEMA", "PESO EN LB.", "CARDIACA", "DIABETES", "H.T.A.", "OTRAS" });
                        DrawCheckboxGroupV2(innerCol, "HISTORIA", GetHistoriaOptions(Model));
                    });

                    // 4. SONIDOS PULMONARES (Doble Checkbox D e I)
                    row.RelativeItem(1).Border(0).Column(innerCol =>
                    {
                        innerCol.Item().AlignCenter().Text("SONIDOS PULMONARES").FontSize(7).SemiBold();
                        innerCol.Item().Row(r =>
                        {
                            r.RelativeItem().PaddingLeft(5).Text("D.").FontSize(7).Bold();
                            r.RelativeItem().Text("I.").FontSize(7).Bold();
                            r.RelativeItem(3); // Espacio para el texto
                        });


                        DrawYesNoCheckbox(innerCol, GetSonidosPulmonaresOptions(Model));

                        //var sonidos = new[] { "CLAROS", "AUSENTES", "CREPITANTES", "RONCUS", "SIBILANCIAS" };
                        //for (int i = 0; i < sonidos.Length; i++)
                        //{
                        //    innerCol.Item().Row(r =>
                        //    {

                        //        if (i == 3)
                        //            r.RelativeItem().PaddingVertical(1).Border(0.5f).Height(8).Width(8);
                        //        else
                        //            r.RelativeItem().PaddingVertical(1).Border(0.5f).Height(8).Width(8).Background("#000");
                        //        // D
                        //        r.RelativeItem().PaddingVertical(1).Border(0.5f).Height(8).Width(8); // I
                        //        r.RelativeItem(3).PaddingLeft(2).Text(sonidos[i]).FontSize(7);
                        //    });
                        //}
                    });

                    // 5. PUPILAS
                    row.RelativeItem(1.1f).Border(0).Column(innerCol =>
                    {
                        innerCol.Item().AlignCenter().Text("PUPILAS").FontSize(7).SemiBold();
                        innerCol.Item().Row(r =>
                        {
                            r.RelativeItem().PaddingLeft(5).Text("D.").FontSize(7).Bold();
                            r.RelativeItem().Text("I.").FontSize(7).Bold();
                            r.RelativeItem(3);
                        });


                        DrawYesNoCheckbox(innerCol, GetPupilasOptions(Model));

                        //var pupilas = new[] { "PUNTIFORMES", "MEDIAS", "DILATADAS", "RESPONDE A LA LUZ", "NO RESPONDE", "ANISOCORIA" };
                        //foreach (var p in pupilas)
                        //{
                        //    innerCol.Item().Row(r =>
                        //    {
                        //        r.RelativeItem().PaddingVertical(1).Border(0.5f).Height(8).Width(8);
                        //        r.RelativeItem().PaddingVertical(1).Border(0.5f).Height(8).Width(8);
                        //        r.RelativeItem(3).PaddingLeft(2).Text(p).FontSize(7);
                        //    });
                        //}
                    });

                    // 6. IMÁGENES/DIAGRAMA (Placeholder)
                    row.RelativeItem(1.2f).Column(innerCol =>
                    {
                        innerCol.Item().Height(80).Placeholder(); // Espacio para el diagrama de ojos
                        innerCol.Item().AlignCenter().Text("DIAGRAMA OCULAR").FontSize(7).Italic();
                    });


                    // 2 parte Content
                    col.Item().PaddingTop(5).Border(0).Row(row =>
                    {
                        // 1. MARQUE SI O NO
                        row.RelativeItem(1).Border(0).Column(c =>
                        {
                            c.Item().Text("MARQUE SI O NO").FontSize(7).SemiBold();
                            DrawYesNoHeader(c);

                            var opciones = new[] { "CAMA", "CONTRACTURAS", "SANGRADO", "INMOBILIZADO", "SOLUCIÓN IV", "MIEMBROS (S) AMPUTADOS (S)", "LUCES / SIRENA", "OXÍGENO", "PARÁLISIS", "VOMITADO" };
                            foreach (var opt in opciones) DrawYesNoRow(c, opt);
                        });

                        // 2. TX ESCENA
                        row.RelativeItem(1).PaddingLeft(5).Column(c =>
                        {
                            c.Item().Text("TX ESCENA").FontSize(7).SemiBold();
                            DrawYesNoHeader(c);

                            var opciones = new[] { "R.C.C.P.", "MONITOR", "DEFIB", "CANALIZACIÓN", "VENDAJES", "FIJACIÓN EXTERNA", "IRRIGACIÓN", "INMOBILIZACIÓN COLUMNA", "PARTO", "AYUDA PSIQUIÁTRICA" };
                            foreach (var opt in opciones) DrawYesNoRow(c, opt);
                        });

                        // 3. TRATAMIENTO RESPIRATORIO
                        row.RelativeItem(1).PaddingLeft(5).Border(0).Column(c =>
                        {
                            c.Item().Text("TRATAMIENTO RESPIRATORIO").FontSize(7).SemiBold();
                            DrawYesNoHeader(c);

                            var opciones = new[] { "OXÍGENO", "MASCARILLA", "CÁNULA NASAL", "ASPIRACIÓN", "CÁNULA DE GUEDEL", "AMBU", "INTUBACIÓN", "INTUBACIÓN FALLIDA", "VENTILADOR", "OXIMETRÍA" };
                            foreach (var opt in opciones) DrawYesNoRow(c, opt);
                        });

                        // 4. HALLAZGOS NOTABLES (Placeholder para los cuerpos)
                        row.RelativeItem(1.5f).PaddingLeft(10).Column(c =>
                        {
                            c.Item().AlignCenter().Text("HALLAZGOS NOTABLES").FontSize(7).SemiBold();
                            c.Item().Height(100).Placeholder(); // Aquí iría la imagen anatómica

                            // Lista de hallazgos en dos columnas pequeñas o una lista simple
                            c.Item().PaddingTop(5).Text("CONTUSIÓN, DECÚBITO, DISLOCACIÓN, DOLOR, EDEMA, FRACTURA, FOLEY, HEMORRAGIA, LACERACIÓN, PARÁLISIS, QUEMADURA, TUBOS")
                                .FontSize(6).LineHeight(1.2f);
                        });
                    });

                    // --- MÉTODOS AUXILIARES PARA MANTENER EL CÓDIGO LIMPIO ---

                    void DrawYesNoHeader(ColumnDescriptor col)
                    {
                        col.Item().Row(r =>
                        {
                            r.RelativeItem(3); // Espacio para el texto
                            r.RelativeItem(1).AlignCenter().Text("SI").FontSize(7).Bold();
                            r.RelativeItem(1).AlignCenter().Text("NO").FontSize(7).Bold();
                        });
                    }

                    void DrawYesNoRow(ColumnDescriptor col, string label)
                    {
                        col.Item().PaddingVertical(1).Row(r =>
                        {
                            r.RelativeItem(3).AlignLeft().Text(label).FontSize(7);
                            // Cuadro SI
                            r.RelativeItem(1).AlignCenter().Border(0.5f).Height(9).Width(9);
                            // Cuadro NO
                            r.RelativeItem(1).AlignCenter().Border(0.5f).Height(9).Width(9);
                        });
                    }



                    col.Item().PaddingTop(5).Row(row =>
                    {
                        // 1. TABLA DE SIGNOS VITALES Y DROGAS (Izquierda/Centro)
                        row.RelativeItem(4).Border(0).Column(c =>
                        {
                            c.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    // Signos Vitales
                                    columns.RelativeColumn(1.5f); // SIGNOS VITALES
                                    columns.RelativeColumn(0.6f); // F/R
                                    columns.RelativeColumn(0.6f); // F/C
                                    columns.RelativeColumn(0.6f); // T°
                                    columns.RelativeColumn(0.8f); // Glicemia
                                    columns.RelativeColumn(0.8f); // Oximetría
                                    //columns.RelativeColumn(0.8f); 

                                    //// Monitor y Drogas
                                    columns.RelativeColumn(1.3f); // Ecomonitor
                                    columns.RelativeColumn(0.8f); // Resultado
                                    columns.RelativeColumn(1.8f); // Drogas/Dosis/Vía
                                    columns.RelativeColumn(0.8f); // Resultado Drogas
                                });

                                // Cabeceras complejas
                                table.Header(header =>
                                {
                                    header.Cell().Border(0.2f).AlignCenter().Text("SIGNOS VITALES\nHORA    P/A").FontSize(6).SemiBold();
                                    header.Cell().Border(0.2f).AlignCenter().AlignMiddle().Text("F/R").FontSize(7).SemiBold();
                                    header.Cell().Border(0.2f).AlignCenter().AlignMiddle().Text("F/C").FontSize(7).SemiBold();
                                    header.Cell().Border(0.2f).AlignCenter().AlignMiddle().Text("T°").FontSize(7).SemiBold();
                                    header.Cell().Border(0.2f).AlignCenter().AlignMiddle().Text("GLICEMIA").FontSize(6).SemiBold();
                                    header.Cell().Border(0.2f).AlignCenter().AlignMiddle().Text("OXIMETRÍA").FontSize(6).SemiBold();

                                    header.Cell().Border(0.2f).AlignCenter().AlignMiddle().Text("ECOMONITOR\nRITMO/DESFIB.").FontSize(6).SemiBold();
                                    header.Cell().Border(0.2f).AlignCenter().AlignMiddle().Text("RESULT.").FontSize(6).SemiBold();
                                    header.Cell().Border(0.2f).AlignCenter().AlignMiddle().Text("DROGAS\nHORA | DOSIS | VIA").FontSize(6).SemiBold();
                                    header.Cell().Border(0.2f).AlignCenter().AlignMiddle().Text("RESULT.").FontSize(6).SemiBold();
                                });

                                // Filas vacías para llenado manual (ejemplo de 4 filas)
                                for (int i = 0; i < 4; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                        table.Cell().Border(0.2f).Height(15).Text("").FontSize(6);
                                }
                            });




                            //2.BLOQUE DE TEXTO(Historia, Alergias, etc.)
                            c.Item().PaddingTop(10).BorderTop(0.2f).Padding(2).Column(textCol =>
                            {
                                string[] campos = { "1) HISTORIA:", "2) IC:", "3) SIGNOS Y SINTOMAS:", "4) ALERGIAS:", "5) EXAMEN FÍSICO:", "6) TRATAMIENTO:" };
                                foreach (var campo in campos)
                                {
                                    textCol.Item().PaddingBottom(2).BorderBottom(0.2f).Row(r =>
                                    {
                                        r.AutoItem().Text(campo).FontSize(6).SemiBold();
                                        r.RelativeItem().Height(13);
                                    });
                                }
                            });
                        });



                        col.Item().PaddingTop(5).Row(row =>
                        {
                            // COLUMNA IZQUIERDA: Textos Legales y Personal
                            row.RelativeItem(2).Column(leftCol =>
                            {
                                // 1. AUTORIZACIÓN DE TRASLADO (Con bordes redondeados)
                                leftCol.Item().Border(0.3f).Padding(8).Column(inner =>
                                {
                                    inner.Item().Text("AUTORIZACIÓN DE TRASLADO:").FontSize(8).ExtraBold();

                                    inner.Item().PaddingTop(2).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(7).LineHeight(1.2f));
                                        text.Span("Entiendo que ");
                                        text.Span("__________________________________________").Underline();
                                        text.Span(" se encuentra en condición crítica y será trasladado a mi solicitud al Hospital ");
                                        text.Span("____________________________").Underline();
                                        text.Span(" para atención de emergencia. YO, como pariente más cercano en la escena, autorizo al personal de ");
                                        text.Span("QUICKASSIST").Bold();
                                        text.Span(", a realizar los procedimientos que consideren necesarios. Estoy consciente del riesgo que conlleva el traslado del paciente");
                                        text.Span(" al lugar ya referido. Me doy cuenta que el paciente está en condición crítica y puede no sobrevivir el traslado al hospital,");
                                        text.Span("o, cuando arribe la condición médica sea tal que se deba realizar una cirugía de emergencia, por consiguiente se libera a la empresa");
                                        text.Span(" QUICK ASSIST").Bold();
                                        text.Span(", personal médico y paramédico de cualquier responsabilidad. ");
                                        text.Span(" QUICK ASSIST").Bold();
                                        text.Span(" no se hace responsable por objetos olvidados dentro de la ambulancia durante la atención o el traslado.");
                                    });

                                    inner.Item().AlignRight().PaddingTop(10).Column(c =>
                                    {
                                        c.Item().Width(150).BorderBottom(0.5f);
                                        c.Item().Width(150).AlignCenter().Text("Testigo").FontSize(7);
                                    });
                                });

                                // 2. ATENDIDO POR
                                leftCol.Item().PaddingTop(5).Border(0.3f).Padding(8).Column(inner =>
                                {
                                    void DrawStaffLine(string label)
                                    {
                                        inner.Item().PaddingVertical(2).Row(r =>
                                        {
                                            r.AutoItem().Text(label).FontSize(7).Bold();
                                            r.RelativeItem().PaddingLeft(5).BorderBottom(0.5f).Height(10);
                                        });
                                    }
                                    DrawStaffLine("ATENDIDO POR EL PERSONAL PARAMÉDICO");
                                    DrawStaffLine("MÉDICO");
                                });
                            });

                            // COLUMNA DERECHA: Recuadros de Firma
                            row.RelativeItem(1).PaddingLeft(5).Column(rightCol =>
                            {
                                //rightCol.Item().Border(0.5f).Height(65)
                                //    .AlignBottom() // Esto empuja todo el contenido de este Item al fondo
                                //    .Column(inner =>
                                //    {
                                //        inner.Item().BorderTop(0.5f).Background(Colors.Grey.Lighten4).AlignCenter().PaddingVertical(2)
                                //            .Text("TOTAL DE GLASGOW: 00").FontSize(7).Bold();
                                //    });

                                rightCol.Item().Border(0.3f).Column(inner =>
                                {
                                    inner.Item().Background(Colors.Grey.Lighten3).AlignCenter().Text("ESCALA DE GLASGOW").FontSize(7).Bold();

                                    // Sección Ojos
                                    inner.Item().PaddingHorizontal(3).AlignCenter().Text("OJOS").FontSize(6).Bold().Underline();
                                    inner.Item().PaddingHorizontal(3).Row(r =>
                                    {
                                        r.RelativeItem().AlignCenter().Text("4\n(ESPONT.)").FontSize(4).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("3\n(VERBAL)").FontSize(4).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("2\n(DOLOR)").FontSize(4).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("1\n(NINGUNO)").FontSize(4).SemiBold();
                                    });

                                    inner.Item().PaddingVertical(2).LineHorizontal(0.5f);

                                    // Sección Motor
                                    inner.Item().PaddingHorizontal(3).AlignCenter().Text("MOTOR").FontSize(6).Bold().Underline();
                                    inner.Item().PaddingHorizontal(3).Row(r =>
                                    {
                                        r.RelativeItem().AlignCenter().Text("6\n(OBEDECE)").FontSize(3.3f).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("5\n(LOCAL)").FontSize(3.5f).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("4\n(RETIRA)").FontSize(3.5f).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("3\n(FLEX)").FontSize(3.5f).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("2\n(EXT)").FontSize(3.5f).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("1\n(N.R)").FontSize(3.5f).SemiBold();
                                    });

                                    inner.Item().PaddingVertical(2).LineHorizontal(0.5f);

                                    // Sección Verbal
                                    inner.Item().PaddingHorizontal(3).AlignCenter().Text("VERBAL").FontSize(6).Bold().Underline();
                                    inner.Item().PaddingHorizontal(3).Row(r =>
                                    {
                                        r.RelativeItem().AlignCenter().Text("5\n(ORIENTADO)").FontSize(3.2f).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("4\n(CONFUSO)").FontSize(3.5f).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("3\n(INAPROPIADO)").FontSize(2.83f).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("2\n(INCO)").FontSize(3.5f).SemiBold();
                                        r.RelativeItem().AlignCenter().Text("1\n(N.R)").FontSize(3.5f).SemiBold();
                                    });
                                });


                                // Firma Paciente
                                rightCol.Item().PaddingTop(3).Border(0.3f).Height(45)
                                    .AlignBottom() // Esto empuja todo el contenido de este Item al fondo
                                    .Column(inner =>
                                    {
                                        inner.Item().BorderTop(0.5f).Background(Colors.Grey.Lighten4).AlignCenter().PaddingVertical(2)
                                            .Text("FIRMA DEL PACIENTE O TESTIGO").FontSize(7).Bold();
                                    });

                                // Firma Personal
                                rightCol.Item().Border(0.3f).Height(45)
                                    .AlignBottom() // Alinea el contenido al borde inferior del contenedor de 65pt
                                    .Column(inner =>
                                    {
                                        inner.Item().BorderTop(0.3f).Background(Colors.Grey.Lighten4).AlignCenter().PaddingVertical(2)
                                            .Text("FIRMA DEL PERSONAL DE LA AMBULANCIA").FontSize(7).Bold();
                                    });
                            });
                        });



                    });

                });
            });
        }


        private void DrawCheckboxGroup(ColumnDescriptor col, string title, string[] options)
        {
            col.Item().Text(title).FontSize(7).SemiBold();
            for (int i = 0; i < options.Length; i++)
            {
                col.Item().Row(row =>
                {
                    row.AutoItem().Text($"{i + 1} ").FontSize(7);

                    if (i == 2)
                        row.AutoItem().PaddingVertical(1).Border(0.5f).Height(8).Width(8).Background("#000");
                    else
                        row.AutoItem().PaddingVertical(1).Border(0.5f).Height(8).Width(8);

                    row.RelativeItem().PaddingLeft(2).Text(options[i]).FontSize(7);
                });
            }
        }



        private void DrawYesNoCheckbox(ColumnDescriptor col, Dictionary<string, RightAndLeftDto> options)
        {
            foreach(var opt in options)
            {
                col.Item().Row(r =>
                {
                    r.RelativeItem()
                     .PaddingVertical(1)
                     .Border(0.5f)
                     .BorderColor(opt.Value.right ? "#fff" : "#000")
                     .Background(opt.Value.right ? "#000" : "#fff")
                     .Height(8).Width(8); //D
                    
                    r.RelativeItem()
                     .PaddingVertical(1)
                     .Border(0.5f)
                     .BorderColor(opt.Value.right ? "#fff" : "#000")
                     .Background(opt.Value.left ? "#000" : "fff")
                     .Height(8).Width(8); //I

                    r.RelativeItem(3).PaddingLeft(2).Text(opt.Key).FontSize(7);
                });
            }
        }


        private void DrawCheckboxGroupV2(ColumnDescriptor col, string title, Dictionary<string, bool> options)
        {
            col.Item().Text(title).FontSize(7).SemiBold();
            
            int i = 0;
            foreach (var opt in options)
            {
                col.Item().Row(row =>
                {
                    row.AutoItem().Text($"{i + 1} ").FontSize(7);

                    row.AutoItem()
                        .PaddingVertical(1)
                        .PaddingLeft(2)
                        .Border(0.5f)
                        .Height(8)
                        .Width(8)
                        .Background((opt.Value) ? "#000" : "fff");

                    row.RelativeItem().PaddingLeft(2).Text(opt.Key).FontSize(7);
                });

                i++;
            }
        }

        private Dictionary<string, bool> GetEstadoMentalOptions(EmergencyProfileModel model)
            => new Dictionary<string, bool>
                {
                    { "CONCIENTE", model.is_conscious },
                    { "DESORIENTADO", model.is_disoriented },
                    { "INCONSCIENTE", model.is_unconscious },
                    { "CONVULSIONANDO", model.is_seizing }
                };
        

        private Dictionary<string, bool> GetColorPielOptions(EmergencyProfileModel model)
            => new Dictionary<string, bool>
                {
                    { "N.L", model.is_skin_color_not_applied },
                    { "CIANÓTICA", model.is_skin_color_cyanotic },
                    { "PÁLIDO", model.is_skin_color_pale },
                    { "RASH", model.is_skin_color_rash }
                };

        private Dictionary<string, bool> GetPielOptions(EmergencyProfileModel model)
            => new Dictionary<string, bool>
                {
                    { "N.L", false },
                    { "SECA", model.is_skin_dry },
                    { "DIAFORÉTICA", model.is_skin_diaphoretic }

                };


        private Dictionary<string, bool> GetTemperaturaPielOptions(EmergencyProfileModel model)
            => new Dictionary<string, bool>
                {
                    { "N.L", model.temperature_not_applied },
                    { "FRÍA", model.temperature_hot },
                    { "CALIENTE", model.temperature_cold }
                };


        private Dictionary<string, bool> GetHistoriaOptions(EmergencyProfileModel model)
            => new Dictionary<string, bool>
                {
                    { "ASMA", model.history_asthma },
                    { "BRONQUITIS", model.history_bronchitis },
                    { "ENFISEMA", false },
                    { "PESO EN LB.", model.history_overweight },
                    { "CARDIACA", model.history_cardiac },
                    { "DIABETES", model.history_diabetes },
                    { "H.T.A.", model.history_hta },
                    { "OTRAS", model.history_others }
                };


        public Dictionary<string, RightAndLeftDto> GetSonidosPulmonaresOptions(EmergencyProfileModel model)
            => new Dictionary<string, RightAndLeftDto>
                {
                    { "CLAROS", new RightAndLeftDto { right = model.long_right_clear, left = model.long_left_clear } },
                    { "AUSENTES", new RightAndLeftDto { right = model.long_right_absent, left = model.long_left_absent } },
                    { "CREPITANTES", new RightAndLeftDto { right = model.long_right_crackling, left = model.long_left_crackling } },
                    { "RONCUS", new RightAndLeftDto { right = model.long_right_ronchi, left = model.long_left_ronchi } },
                    { "SIBILANCIAS", new RightAndLeftDto { right = model.long_right_wheezing, left = model.long_left_wheezing    } }
                };


        public Dictionary<string, RightAndLeftDto> GetPupilasOptions(EmergencyProfileModel model)
            => new Dictionary<string, RightAndLeftDto>
                {
                    { "PUNTIFORMES", new RightAndLeftDto { right = model.pupils_right_pupiliform, left = model.pupils_left_pupiliform } },
                    { "MEDIAS", new RightAndLeftDto { right = model.pupils_right_medium, left = model.pupils_left_medium } },
                    { "DILATADAS", new RightAndLeftDto { right = model.pupils_right_dilated, left = model.pupils_left_dilated } },
                    { "RESPONDE A LA LUZ", new RightAndLeftDto { right = model.pupils_right_react_light, left = model.pupils_left_react_light } },
                    { "NO RESPONDE", new RightAndLeftDto { right = model.pupils_right_not_responing, left = model.pupils_left_not_responing } },
                    { "ANISOCORIA", new RightAndLeftDto { right = model.pupils_right_not_anisocoria, left = model.pupils_left_not_anisocoria } }
                };


        //private Dictionary<string, bool> GetYesNoOptions(EmergencyProfileModel model)
        //    => new Dictionary<string, bool>
        //        {
        //            { "CAMA", model.yes_no_cama },
        //            { "CONTRACTURAS", model.yes_no_contracturas },
        //            { "SANGRADO", model.yes_no_sangrado },
        //            { "INMOBILIZADO", model.yes_no_inmobilizado },
        //            { "SOLUCIÓN IV", model.yes_no_solucion_iv },
        //            { "MIEMBROS (S) AMPUTADOS (S)", model.yes_no_miembros_amputados },
        //            { "LUCES / SIRENA", model.yes_no_luces_sirena },
        //            { "OXÍGENO", model.yes_no_oxigeno },
        //            { "PARÁLISIS", model.yes_no_paralisis },
        //            { "VOMITADO", model.yes_no_vomitado }
        //        };  
    }
}
