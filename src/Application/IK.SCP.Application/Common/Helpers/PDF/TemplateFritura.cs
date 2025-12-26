using IK.SCP.Application.ACO.General.ViewModels;
using IK.SCP.Application.FR.ViewModels;
using Irony.Parsing.Construction;
using iText.Layout.Borders;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Border = iText.Layout.Borders.Border;
using Cell = iText.Layout.Element.Cell;
using Table = iText.Layout.Element.Table;

namespace IK.SCP.Application.Common.Helpers.PDF;
public class TemplateFritura
{
    private static Cell CreateRotatedCell(int rowSpan, int colSpan, string content, TextAlignment textAlignment = TextAlignment.CENTER)
    {
        Cell outerCell = new Cell(rowSpan, colSpan)
            .SetPadding(0)
            .SetMargin(0)
            .SetVerticalAlignment(VerticalAlignment.MIDDLE);

        Paragraph paragraph = new Paragraph(content == null ? "" : content)
            .SetTextAlignment(textAlignment)
            .SetRotationAngle(Math.PI / 2);

        outerCell.Add(paragraph).SetFontSize(7);

        return outerCell;
    }
    private static void RemoveAllBorder(Table table)
    {
        foreach (var cell in table.GetChildren())
        {
            ((Cell)cell).SetBorder(null);
        }
    }
    private static void RemoveInsideBorder(Table table)
    {
        table.SetBorder(Border.NO_BORDER);
        table.SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f));
        table.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5f));
        table.SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f));
        table.SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));

        // Quita los bordes interiores de las celdas
        foreach (var cell in table.GetChildren())
        {
            if (cell is Cell)
            {
                ((Cell)cell).SetBorder(Border.NO_BORDER);
            }
        }
    }
    private static void SetLabelValue(Table table, Cell label, Cell value)
    {
        table.AddCell(label);
        table.AddCell(value);
    }
    private static void GenerateTable(Document document, float[] columnWidths, string[] header, string[][] data)
    {
        if (columnWidths.Length != header.Length)
        {
            throw new ArgumentException("El número de elementos en columnWidths debe coincidir con el número de columnas en el encabezado.");
        }

        var table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();

        foreach (var columnHeader in header)
        {
            table.AddCell(CreateCellFormat(1, 1, columnHeader).SetTextAlignment(TextAlignment.CENTER).SetBold());
        }

        foreach (var rowData in data)
        {
            foreach (var cellData in rowData)
            {
                table.AddCell(CreateCellFormat(1, 1, cellData).SetTextAlignment(TextAlignment.CENTER));
            }
        }

        table.SetMarginBottom(10f);
        document.Add(table);
    }
    private static void GenerateTableCustom(Document document, float[] columnWidths, string data) {

        // Parsea el JSON-ARRAY;
        var mArrayData    = JArray.Parse(data);

        /************************************************************************************************************
         *  HEAD OF TABLE
         ************************************************************************************************************/
        var table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();
            table.AddCell(CreateCellFormat(2, 3, "II. Verificación de Equipo previa al arranque").SetBold());
            table.AddCell(CreateCellFormat(1, 2, " Si: Check(✓), No: (X), No Aplica: (N.A.)").SetBold());
            table.AddCell(CreateCellFormat(2, 1, "Observaciones / Causa aparente / Acciones inmediatas").SetBold());
            table.AddCell(CreateCellFormat(1, 1, "Operativo").SetBold());
            table.AddCell(CreateCellFormat(1, 1, "Limpio").SetBold());

        /************************************************************************************************************
         *  DETAIL OF TABLE
         ************************************************************************************************************/
        foreach (var item in mArrayData) 
        {
            var padre = item["padre"].ToString();
            var detalleArray = item["detalle"].ToObject<JArray>();

            // Añade la celda de Padre
            table.AddCell(CreateRotatedCell(detalleArray.Count, 1, padre));
            
            // Añade las celdas de Detalle
            foreach (var detalle in detalleArray) 
            {
                var nombre = detalle["nombre"].ToString();
                var detalleTexto = detalle["detalle"].ToString();
                
                if (detalleTexto.Trim() == "0.-") {

                    if (detalle["rowspan"].ToString() != "0")
                    {
                        // Si el detalle es ".-", oculta la celda de detalle y fusiona la celda de nombre
                        var nombreCell = new Cell(Int16.Parse(detalle["rowspan"].ToString()), 2)
                            .Add(new Paragraph(nombre))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(7);
                        table.AddCell(nombreCell);
                    }
                    
                } else {
                    // Si el detalle no es ".-", muestra ambas celdas de nombre y detalle
                    if (detalle["rowspan"].ToString() != "0")
                    {
                        var nombreCell = new Cell(Int16.Parse(detalle["rowspan"].ToString()), 1)
                            .Add(new Paragraph(nombre))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(7);
                        table.AddCell(nombreCell);
                    }

                    var detalleCell = new Cell(1, 1)
                        .Add(new Paragraph(detalleTexto))
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(7);
                    
                    table.AddCell(detalleCell);
                }

                // Añade las celdas de Operativo, Limpio y Observación
                var operativo = detalle["operativo"].ToString();
                var limpio = detalle["limpio"].ToString();
                var observacion = detalle["observacion"].ToString();
                
                var operativoCell = new Cell(1, 1)
                    .Add(new Paragraph(operativo))
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(7);

                var limpioCell = new Cell(1, 1)
                    .Add(new Paragraph(limpio))
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(7);
                var observacionCell = new Cell(1, 1)
                    .Add(new Paragraph(observacion))
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(7);

                table.AddCell(operativoCell);
                table.AddCell(limpioCell);
                table.AddCell(observacionCell);
            }
        }
        document.Add(table);
    }
    private static Cell CreateCellFormat(int rowSpan, int colSpan, string text)
    {
        return new Cell(rowSpan, colSpan)
            .SetTextAlignment(TextAlignment.LEFT)
            .Add(new Paragraph(text))
            .SetFontSize(7)
            .SetVerticalAlignment(VerticalAlignment.MIDDLE); ;
    }
    public static Document GetTemplateLineasFrituras( Document document,
                                                            List<GetByIdArranqueMaquinaCondicionResponse> Condiciones,
                                                            GetByIdMaquinaVerificacionEquipoResponse verificaciones,
                                                            List<GetByIdArranqueMaquinaObservacionResponse> Observaciones,
                                                            List<GetByIdArranqueMaquinaEvaluacionSensorial> sensoriales,
                                                            Dictionary<string, string> DataGeneral)
    {
        
        /************************************************************************************************************
         *  General Information  of Production
         ************************************************************************************************************/
        var columnsHead = new float[] { 12, 38, 15, 35 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        SetLabelValue(tableHead, CreateCellFormat(1, 1, "PRODUCTO: "), 
                                CreateCellFormat(1, 1, DataGeneral["Articulo"]));
        SetLabelValue(tableHead, CreateCellFormat(1, 1, "ORDEN DE PRODUCCION: "), 
                                    CreateCellFormat(1, 1,  DataGeneral["Orden"]));
        SetLabelValue(tableHead, CreateCellFormat(1, 1, "MAQUINISTA: "), 
                                    CreateCellFormat(1, 1,  verificaciones.Usuario));
        SetLabelValue(tableHead, CreateCellFormat(1, 1, "COORDINADOR/ENCARGADO: "), 
                                    CreateCellFormat(1, 1,  DataGeneral["Coordinador"]));
        RemoveInsideBorder(tableHead);
        tableHead.SetMarginBottom(10f);
        document.Add(tableHead);

        /************************************************************************************************************
         *  Data de control
         ************************************************************************************************************/
        var columnsDataControl = new float[] { 6, 23, 6, 23, 9, 23 };
        var tableDataControl = new Table(UnitValue.CreatePercentArray(columnsDataControl)).UseAllAvailableWidth();

        SetLabelValue(tableDataControl, CreateCellFormat(1, 1, "FECHA").SetBorder(Border.NO_BORDER),
                                       CreateCellFormat(1, 1, verificaciones.Fecha.ToString("yyyy-M-d")));
        SetLabelValue(tableDataControl, CreateCellFormat(1, 1, "TURNO").SetBorder(Border.NO_BORDER),
                                       CreateCellFormat(1, 1, DataGeneral["Turno"]));
        SetLabelValue(tableDataControl, CreateCellFormat(1, 1, "HORA INICIO").SetBorder(Border.NO_BORDER),
                                       CreateCellFormat(1, 1, verificaciones.Fecha.ToString("HH:mm:ss")));
        tableDataControl.SetMarginBottom(10f);
        document.Add(tableDataControl);

        /************************************************************************************************************
         *  Condiciones Previas de Arranque
         ************************************************************************************************************/
        var columnWidths = new float[] { 55, 10, 10, 35 };
        var header = new string[] { "Condiciones previas al arranque", "Si", "No", "Observaciones" };
        
        string[][] dataCondicionesArranque = Condiciones
            .Select(condicion => new string[] { condicion.Nombre.ToString(), 
                                                condicion.Valor?"X":"",
                                                condicion.Valor?"":"X",
                                                condicion.Comentario==null?"":condicion.Comentario.ToString()
            })
            .ToArray();

        // Generar la tabla en el documento PDF
        GenerateTable(document, columnWidths, header, dataCondicionesArranque);

        /************************************************************************************************************
         *  Verificacion del equipo previa  al arranque
         ************************************************************************************************************/
        var columnsTable = new float[] { 4, 30, 25, 5, 5, 31 };
        var listVerificaciones = verificaciones
            .VerificacionesArranque
            .GroupBy(g => new { g.Orden_1, g.Nombre_1 })
            .Select(x => new
            {
                padre = x.Key.Orden_1.ToString() + ". " + x.Key.Nombre_1,
                detalle = x.Select(y => new
                {
                    id = y.Id,
                    verificacionEquipoId = y.VerificacionEquipoId,
                    nombre = $"{y.Orden_2}.- {y.Nombre_2}",
                    detalle = $"{y.Orden_3}.- {y.Nombre_3}",
                    operativo = y.Operativo,
                    limpio = y.Limpio,
                    observacion = y.Observacion,
                    orden = y.Orden_2,
                    cerrado = y.Cerrado,
                    rowspan = y.rowSpan
                }).ToList()
            }).ToList();
        
        string data = @JsonConvert.SerializeObject(listVerificaciones);
        
        // Generar la tabla en el documento PDF
        GenerateTableCustom(document, columnsTable, data);
        
        // Comment information aditional of table 'Verificacion de Equipo previa al arranque'
        document.Add(new Paragraph("* Materiales duro/quebradizo e instrumento afilado            **Solo aplica después de una limpieza intensiva ó limpieza de la freidora").SetFontSize(7));
        
        /************************************************************************************************************
         *  Variables basicas de arranque
         ************************************************************************************************************/
        var columnsVariablesArranque = new float[] { 65, 35 };
        var tableVariablesArranque = new Table(UnitValue.CreatePercentArray(columnsVariablesArranque)).UseAllAvailableWidth();

        var celdaRow1Title = new Cell(1, 2).SetPaddings(2,0,2,0);
        var tableRow1Title = new Table(UnitValue.CreatePercentArray(1))
            .UseAllAvailableWidth();
        tableRow1Title.AddCell(CreateCellFormat(1, 1, "III. Variables basicas para el arranque").SetTextAlignment(TextAlignment.LEFT));
        celdaRow1Title.Add(tableRow1Title);
        tableVariablesArranque.AddCell(celdaRow1Title);

        /************************************************************************************************************
         *  Caracteristicas del Aceite
         ************************************************************************************************************/
        var celdaRow2OC = new Cell(1, 1).SetPaddings(2,2,2,0);
        var columnsRow2OC = new float[] { 45, 25, 15, 15 };
        var tableRow2OilCharacteristics = new Table(UnitValue.CreatePercentArray(columnsRow2OC)).UseAllAvailableWidth();
        tableRow2OilCharacteristics.AddCell(CreateCellFormat(1, 2, ""));
        tableRow2OilCharacteristics.AddCell(CreateCellFormat(1, 1, "% AGL"));
        tableRow2OilCharacteristics.AddCell(CreateCellFormat(1, 1, "% CP"));
        tableRow2OilCharacteristics.AddCell(CreateCellFormat(2, 1, "Caracteristicas del Aceite"));
        tableRow2OilCharacteristics.AddCell(CreateCellFormat(1, 1, "Valores"));
        tableRow2OilCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        tableRow2OilCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        tableRow2OilCharacteristics.AddCell(CreateCellFormat(1, 1, "Condicion OK? (Si/No)"));
        tableRow2OilCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        tableRow2OilCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        celdaRow2OC.Add(tableRow2OilCharacteristics);
        tableVariablesArranque.AddCell(celdaRow2OC);

        /************************************************************************************************************
         *  Nivel de aceite
         ************************************************************************************************************/
        var celdaRow2OL = new Cell(1, 1).SetPaddings(2,0,2,2);
        var columnsRow2OL = new float[] { 40, 40, 20 };
        var tableRow2OilLevel = new Table(UnitValue.CreatePercentArray(columnsRow2OL)).UseAllAvailableWidth();
        tableRow2OilLevel.AddCell(CreateCellFormat(2, 1, "Nivel de Aceite"));
        tableRow2OilLevel.AddCell(CreateCellFormat(1, 1, "Valor"));
        tableRow2OilLevel.AddCell(CreateCellFormat(1, 1, " - "));
        tableRow2OilLevel.AddCell(CreateCellFormat(1, 1, "Conforme? (Si/No)"));
        tableRow2OilLevel.AddCell(CreateCellFormat(1, 1, " - "));
        celdaRow2OL.Add(tableRow2OilLevel);
        tableVariablesArranque.AddCell(celdaRow2OL);

        /************************************************************************************************************
         *  Caracteristicas del producto
         ************************************************************************************************************/
        var celdaRow3PC = new Cell(1, 2).SetPaddings(2,0,2,0);
        var columnsRow3PC = new float[] { 29.25f, 16.25f, 9.75f, 9.75f, 9.75f, 25.25f };
        var tableRow3ProductCharacteristics = new Table(UnitValue.CreatePercentArray(columnsRow3PC)).UseAllAvailableWidth();
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 2, ""));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "% Cloruro"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "% Humedad"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "% Grasa"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, ""));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(2, 1, "Caracteristicas del producto frito"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "Valores"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "Condicion OK? (Si/No)"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        tableRow3ProductCharacteristics.AddCell(CreateCellFormat(1, 1, "-"));
        celdaRow3PC.Add(tableRow3ProductCharacteristics);
        tableVariablesArranque.AddCell(celdaRow3PC);

        /************************************************************************************************************
         *  Evaluacion sensorial de atributos de producto frito
         ************************************************************************************************************/
        var celdaRow4ES = new Cell(1, 2).SetPaddings(2,0,2,0);
        var columnsRow4ES = new float[] { 32.5f, 4.875f, 4.875f, 4.875f, 4.875f, 4.875f, 4.875f, 20.25f, 18 };
        var tableRow4EvaluacionSensorial = new Table(UnitValue.CreatePercentArray(columnsRow4ES)).UseAllAvailableWidth();
        tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, ""));
        tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 6, "Hora:"));
        tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 2, ""));

        tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(2, 1, "Evaluacion Sensorial de Atributos del Producto Frito \n" +
            "Calificacion Final: \n" +
            "1= Muy Malo 2=Malo 3=Regular 4=Bueno \n 5=Muy Bueno"));
        tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, "Apariencia General").SetRotationAngle(Math.PI / 2));
        tableRow4EvaluacionSensorial.AddCell(CreateRotatedCell(1, 1, "Color"));
        tableRow4EvaluacionSensorial.AddCell(CreateRotatedCell(1, 1, "Olor"));
        tableRow4EvaluacionSensorial.AddCell(CreateRotatedCell(1, 1, "Sabor"));
        tableRow4EvaluacionSensorial.AddCell(CreateRotatedCell(1, 1, "Textura"));
        tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, "Calificacion Final").SetRotationAngle(Math.PI / 2));
        tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, "Panelista"));
        tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, "Observaciones"));

        foreach (var sensorial in sensoriales)
        {
            tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, sensorial.AparienciaGeneral.ToString()));
            tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, sensorial.Color.ToString()));
            tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, sensorial.Olor.ToString()));
            tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, sensorial.Sabor.ToString()));
            tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, sensorial.Textura.ToString()));
            tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, sensorial.CalificacionFinal.ToString()));
            tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, sensorial.Panelistas));
            tableRow4EvaluacionSensorial.AddCell(CreateCellFormat(1, 1, sensorial.Observacion));
        }
        celdaRow4ES.Add(tableRow4EvaluacionSensorial);
        tableVariablesArranque.AddCell(celdaRow4ES);

        /************************************************************************************************************
         *  Observaciones
         ************************************************************************************************************/
        var celdaRow5OB = new Cell(1, 2).SetPaddings(2,0,2,0);
        var tableObservaciones = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();

        foreach (var observacion in Observaciones)
        {
            tableObservaciones.AddCell(new Cell()
                .Add(new Paragraph(observacion.Observacion))
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(7))
                .SetBorder(Border.NO_BORDER);
        }
        
        tableObservaciones.SetHeight(40f);
        celdaRow5OB.Add(tableObservaciones);
        tableVariablesArranque.AddCell(celdaRow5OB);
        RemoveAllBorder(tableVariablesArranque);
        document.Add(tableVariablesArranque);
        
        /************************************************************************************************************
         *  Signature (Firmas del Maquinista y del Coordinador)
         ************************************************************************************************************/ 
        var columnsSignature = new float[] { 25, 25, 25, 25 };
        var tableSignature = new Table(UnitValue.CreatePercentArray(columnsSignature)).UseAllAvailableWidth();
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, "___________________________").SetBorder(Border.NO_BORDER), 
                                      CreateCellFormat(1, 1, ""));
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, ""), 
                                     CreateCellFormat(1, 1,  "___________________________"));
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, "Maquinista").SetBorder(Border.NO_BORDER), 
                                      CreateCellFormat(1, 1, ""));
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, ""), 
                                     CreateCellFormat(1, 1,  "Coordinador"));
        tableSignature.SetMarginTop(40f);
        RemoveAllBorder(tableSignature);
        document.Add(tableSignature);
        
        document.Close();

        return document;
    }

    public static Document GetTemplateAttributeEvaluation(Document document, GetByIdEvaluacionAtributoPDFResponse orden )
    {
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<GetByOrdenEvaluacionAtributo> evaluaciones = orden.EvaluacionAtributos;
        
        /************************************************************************************************************
         *  General Information  of Production
         ************************************************************************************************************/
        var columnsHead = new float[] { 6, 42, 20, 32 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        SetLabelValue(tableHead, CreateCellFormat(1, 1, "FECHA: "), 
            CreateCellFormat(1, 1, orden.Fecha.ToString("dd-MM-yy")));
        SetLabelValue(tableHead, CreateCellFormat(1, 1, "NOMBRE DEL PRODUCTO: "), 
            CreateCellFormat(1, 1,  orden.Descripcion));
        SetLabelValue(tableHead, CreateCellFormat(1, 1, "TURNO: "), 
            CreateCellFormat(1, 1,  orden.Turno));
        SetLabelValue(tableHead, CreateCellFormat(1, 1, "O/FR: "), 
            CreateCellFormat(1, 1,  orden.Orden));
        SetLabelValue(tableHead, CreateCellFormat(1, 1, "LINEA: "), 
            CreateCellFormat(1, 1,  orden.Linea.ToString()));
        SetLabelValue(tableHead, CreateCellFormat(1, 1, "MAQUINISTA: "), 
            CreateCellFormat(1, 1,  orden.Maquinista));
        RemoveAllBorder(tableHead);
        tableHead.SetMarginBottom(10f);
        document.Add(tableHead);
        
        var columnWidths = new float[] { 10, 20, 6, 6, 6, 6, 6, 6, 34 };
        var table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();
        table.AddCell(CreateCellFormat(2, 1, "HORA").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(2, 1, " PANELISTAS").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 6, "PRODUCTO A GRANEL").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(2, 1, "OBSERVACION").SetBold().SetTextAlignment(TextAlignment.CENTER));
        
        table.AddCell(CreateCellFormat(1, 1, "APARIENCIA GENERAL").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 1, "COLOR").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 1, "OLOR").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 1, "SABOR").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 1, "TEXTURA").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 1, "CALIFICACION FINAL*").SetBold().SetTextAlignment(TextAlignment.CENTER));
        
        
        foreach (var evaluacion in evaluaciones)
        {
            var panelistas = evaluacion.Panelistas.Split(',');
            
            // Determinar la cantidad de panelistas
            int cantidadPanelistas = panelistas.Length;
            
            // Añadir las celdas con Rowspan según la cantidad de panelistas
            for (int i = 0; i < cantidadPanelistas; i++)
            {

                if (i == 0)
                {
                    // Añadir las celdas con Rowspan para los campos comunes
                    table.AddCell(CreateCellFormat(cantidadPanelistas, 1, evaluacion.HoraMinutoSegundo).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(CreateCellFormat(1, 1, panelistas[i].Trim()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(CreateCellFormat(cantidadPanelistas, 1, evaluacion.AparienciaGeneral.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(CreateCellFormat(cantidadPanelistas, 1, evaluacion.Color.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(CreateCellFormat(cantidadPanelistas, 1, evaluacion.Olor.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(CreateCellFormat(cantidadPanelistas, 1, evaluacion.sabor.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(CreateCellFormat(cantidadPanelistas, 1, evaluacion.Textura.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(CreateCellFormat(cantidadPanelistas, 1, evaluacion.CalificacionFinal.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(CreateCellFormat(cantidadPanelistas, 1, evaluacion.Observacion).SetTextAlignment(TextAlignment.CENTER));
                }
                else
                {
                    table.AddCell(CreateCellFormat(1, 1, panelistas[i].Trim()).SetTextAlignment(TextAlignment.CENTER));
                }
            }
        }
        
        document.Add(table);
        document.Add(new Paragraph("*CALIFICACION FINAL").SetBold().SetFontSize(7));
        document.Add(new Paragraph("1:Muy Malo  2:Malo  3:Regular  4:Bueno  5:Muy Bueno").SetFontSize(7));
        
        /************************************************************************************************************
         *  Signature (Coordinador de Produccion)
         ************************************************************************************************************/ 
        var columnsSignature = new float[] { 25, 25, 25, 25 };
        var tableSignature = new Table(UnitValue.CreatePercentArray(columnsSignature)).UseAllAvailableWidth();
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, "").SetBorder(Border.NO_BORDER), 
            CreateCellFormat(1, 1, ""));
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, ""), 
            CreateCellFormat(1, 1,  "__________________________________"));
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, "").SetBorder(Border.NO_BORDER), 
            CreateCellFormat(1, 1, ""));
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, ""), 
            CreateCellFormat(1, 1,  "Coordinador de Produccion").SetTextAlignment(TextAlignment.CENTER));
        tableSignature.SetMarginTop(40f);
        RemoveAllBorder(tableSignature);
        document.Add(tableSignature);
        
        document.Close();
        
        return document;
    }

    public static Document GetTemplateAcondicionamientoMaizHabas(Document document, GetByIdReposoMaizPDFResponse sancochado)
    {
        
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<GetByOrdenReposoMaiz> remojoHabasMaiz = sancochado.getByOrdenReposoMaiz;
        
        /************************************************************************************************************
         *  General Information  of Control de remojo
         ************************************************************************************************************/
        var columnsControl = new float[] { 10, 30, 60 };
        var tableControl = new Table(UnitValue.CreatePercentArray(columnsControl)).UseAllAvailableWidth();
        tableControl.SetMargins(5f, 0, 5f, 0);
            
        tableControl.AddCell(CreateCellFormat(1,1,"CONTROL DE: ").SetPaddings(0,0,0,0));

        /************************************************************************************************************
         *  Remojo de habas
         ************************************************************************************************************/
        var cellHabas = new Cell(1, 1).SetPaddings(2,0,2,0);
        var columnsHabas = new float[] { 26, 12, 62 };
        var tableHabas = new Table(UnitValue.CreatePercentArray(columnsHabas)).UseAllAvailableWidth();
        tableHabas.AddCell(CreateCellFormat(1, 1, "Remojo de Habas").SetBorder(Border.NO_BORDER));
        var cellCaja1 = new Cell(1,1);
        var columnsCaja1 = new float[] { 1 };
        var tableCaja1 = new Table(UnitValue.CreatePercentArray(columnsCaja1)).UseAllAvailableWidth();
        tableCaja1.AddCell(CreateCellFormat(1, 1, "Si").SetHeight(12));
        cellCaja1.Add(tableCaja1);
        tableHabas.AddCell(cellCaja1.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER));
            
        var cellMaquinista = new Cell(1, 1).SetBorder(Border.NO_BORDER);
        var columnsMaquinista = new float[] { 1 };
        var tableMaquinista = new Table(UnitValue.CreatePercentArray(columnsMaquinista)).UseAllAvailableWidth();
        tableMaquinista.AddCell(CreateCellFormat(1, 1, "Maquinista Responsable del Remojo de Habas"));
        tableMaquinista.AddCell(CreateCellFormat(1, 1, "Maquinista.FR"));
        cellMaquinista.Add(tableMaquinista);
        
        tableHabas.AddCell(cellMaquinista);
        cellHabas.Add(tableHabas);
        
        tableControl.AddCell(cellHabas);
        /************************************************************************************************************
         *  Remojo de maiz
         ************************************************************************************************************/
        var cellMaiz = new Cell(1, 1).SetPaddings(2,0,2,0);
        var columnsMaiz = new float[] { 14, 6, 80 };
        var tableMaiz = new Table(UnitValue.CreatePercentArray(columnsMaiz)).UseAllAvailableWidth();
        tableMaiz.AddCell(CreateCellFormat(1, 1, "Remojo de Maiz").SetBorder(Border.NO_BORDER));
        var cellCaja2 = new Cell(1,1);
        var columnsCaja2 = new float[] { 1 };
        var tableCaja2 = new Table(UnitValue.CreatePercentArray(columnsCaja2)).UseAllAvailableWidth();
        tableCaja2.AddCell(CreateCellFormat(1, 1, "Si").SetHeight(12));
        cellCaja2.Add(tableCaja2);
        tableMaiz.AddCell(cellCaja2.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER));
            
        var cellSancochado = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetPaddings(2,0,2,2);
        var columnsSancochado = new float[] { 37,38,25 };
        var tableSancochado = new Table(UnitValue.CreatePercentArray(columnsSancochado)).UseAllAvailableWidth();
        tableSancochado.AddCell(CreateCellFormat(1, 1, "N\u00b0 Tanque de remojo del que proviene el maíz"));
        tableSancochado.AddCell(CreateCellFormat(1, 1, "N\u00b0 de Batch\nsancochados por tanque"));
        tableSancochado.AddCell(CreateCellFormat(1, 1, "Maquinista del Sancochado"));
        tableSancochado.AddCell(CreateCellFormat(1, 1, "1"));
        tableSancochado.AddCell(CreateCellFormat(1, 1, "11"));
        tableSancochado.AddCell(CreateCellFormat(1, 1, "Sancochador.FR"));
        cellSancochado.Add(tableSancochado);
        
        tableMaiz.AddCell(cellSancochado);
        cellMaiz.Add(tableMaiz);
        
        tableControl.AddCell(cellMaiz);
        RemoveAllBorder(tableControl);
        
        
        document.Add(tableControl);
        
        /************************************************************************************************************
         *  Detail of Control de remojo
         ************************************************************************************************************/
        var columnWidths = new float[] { 10, 10, 10, 10, 10, 10, 10, 20 };
        var table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();
        table.AddCell(CreateCellFormat(2, 1, "Nro Batch").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(2, 1, "Cantidad de Producto x Batch").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 2, "REMOJO DE HABAS / REPOSO DE MAIZ").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 3, "FRITURA").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(2, 1, "OBSERVACION").SetBold().SetTextAlignment(TextAlignment.CENTER));
        
        table.AddCell(CreateCellFormat(1, 1, "Fecha de inicio de remojo de habas / reposo de maiz").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 1, "Hora de inicio de remojo de habas / reposo de maiz").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 1, "Fecha de inicio de fritura").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 1, "Hora de inicio de fritura").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 1, "Maquinista de Fritura").SetBold().SetTextAlignment(TextAlignment.CENTER));

        double sumarCantidadBatch = 0;
        foreach (var remojo in remojoHabasMaiz)
        {
            table.AddCell(CreateCellFormat(1, 1, remojo.numeroBatch.ToString()).SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(CreateCellFormat(1, 1, remojo.cantidadBatch.ToString()).SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(CreateCellFormat(1, 1, remojo.fechaHoraInicioReposo.ToString("dd/MM/yyyy")).SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(CreateCellFormat(1, 1, remojo.fechaHoraInicioReposo.ToString("HH:mm:ss")).SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(CreateCellFormat(1, 1, remojo.fechaHoraInicioFritura.ToString("dd/MM/yyyy")).SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(CreateCellFormat(1, 1, remojo.fechaHoraInicioFritura.ToString("HH:mm:ss")).SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(CreateCellFormat(1, 1, remojo.usuario).SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(CreateCellFormat(1, 1, remojo.observacion).SetTextAlignment(TextAlignment.CENTER));
            
            sumarCantidadBatch = sumarCantidadBatch + remojo.cantidadBatch;
        }
        table.AddCell(CreateCellFormat(1, 1, "Total(Kg)").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(CreateCellFormat(1, 1, sumarCantidadBatch.ToString()).SetBold().SetTextAlignment(TextAlignment.CENTER));
        
        document.Add(table);
        document.Add(new Paragraph("* Considerar como inicio de reposo para maíz, la fecha y hora de fin de sancochado").SetBold().SetFontSize(7));
        
        /************************************************************************************************************
         *  Signature (Firmas del Maquinista y del Coordinador)
         ************************************************************************************************************/ 
        var columnsSignature = new float[] { 25, 25, 25, 25 };
        var tableSignature = new Table(UnitValue.CreatePercentArray(columnsSignature)).UseAllAvailableWidth();
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, "_______________________________").SetBorder(Border.NO_BORDER), 
            CreateCellFormat(1, 1, ""));
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, ""), 
            CreateCellFormat(1, 1,  "_______________________________"));
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, "Maquinista").SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER), 
            CreateCellFormat(1, 1, ""));
        SetLabelValue(tableSignature, CreateCellFormat(1, 1, ""), 
            CreateCellFormat(1, 1,  "Coordinador").SetTextAlignment(TextAlignment.CENTER));
        tableSignature.SetMarginTop(40f);
        RemoveAllBorder(tableSignature);
        document.Add(tableSignature);
        
        document.Close();
        return document;
    }
}