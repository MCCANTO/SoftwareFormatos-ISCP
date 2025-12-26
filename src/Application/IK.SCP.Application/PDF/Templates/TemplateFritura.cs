

using iText.Kernel.Geom;

namespace IK.SCP.Application.PDF.Templates;
using iText.Kernel.Pdf;

using IK.SCP.Application.PDF.Fritura.Model;
using IK.SCP.Application.PDF.Helpers;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Newtonsoft.Json;
using Border = iText.Layout.Borders.Border;
using Cell = iText.Layout.Element.Cell;
using Table = iText.Layout.Element.Table;

public class TemplateFritura
{
    public static Document GetTemplateLineasFrituras( Document document, ArranqueManufacturaResponse arranque)
    {
        /************************************************************************************************************
         *  Input Data
         ************************************************************************************************************/
        List<Condicion> Condiciones = arranque.Condiciones;
        VerificacionEquipo verificaciones = arranque.Verificaciones;
        List<Observacion> Observaciones = arranque.Observaciones;
        List<Sensorial> sensoriales = arranque.Sensoriales;
        
        /************************************************************************************************************
         *  General Information  of Production
         ************************************************************************************************************/
        var columnsHead = new float[] { 12, 45, 15, 28 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "PRODUCTO: "), 
                                PDFBuilder.CreateCellFormat(1, 1, arranque.Articulo));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "ORDEN DE PRODUCCION: "), 
                                    PDFBuilder.CreateCellFormat(1, 1,  arranque.Orden));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "MAQUINISTA: "), 
                                    PDFBuilder.CreateCellFormat(1, 1,  arranque.Responsable));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "COORDINADOR/ENCARGADO: "), 
                                    PDFBuilder.CreateCellFormat(1, 1,  arranque.Encargado));
        PDFBuilder.RemoveInsideBorder(tableHead);
        tableHead.SetMarginBottom(10f);
        document.Add(tableHead);

        /************************************************************************************************************
         *  Data de control
         ************************************************************************************************************/
        var columnsDataControl = new float[] { 6, 23, 6, 23, 9, 23 };
        var tableDataControl = new Table(UnitValue.CreatePercentArray(columnsDataControl)).UseAllAvailableWidth();

        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "FECHA").SetBorder(Border.NO_BORDER),
                                       PDFBuilder.CreateCellFormat(1, 1, arranque.Fecha.ToString("yyyy-M-d")));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "TURNO").SetBorder(Border.NO_BORDER),
                                       PDFBuilder.CreateCellFormat(1, 1, arranque.Turno));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "HORA INICIO").SetBorder(Border.NO_BORDER),
                                       PDFBuilder.CreateCellFormat(1, 1, arranque.Fecha.ToString("HH:mm:ss")));
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
        PDFBuilder.GenerateTable(document, columnWidths, header, dataCondicionesArranque);

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
        PDFBuilder.GenerateTableCustom(document, columnsTable, data);
        
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
        tableRow1Title.AddCell(PDFBuilder.CreateCellFormat(1, 1, "III. Variables basicas para el arranque").SetTextAlignment(TextAlignment.LEFT));
        celdaRow1Title.Add(tableRow1Title);
        tableVariablesArranque.AddCell(celdaRow1Title);

        /************************************************************************************************************
         *  Caracteristicas del Aceite
         ************************************************************************************************************/
        var celdaRow2OC = new Cell(1, 1).SetPaddings(2,2,2,0);
        var columnsRow2OC = new float[] { 45, 25, 15, 15 };
        var tableRow2OilCharacteristics = new Table(UnitValue.CreatePercentArray(columnsRow2OC)).UseAllAvailableWidth();
        tableRow2OilCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 2, " "));
        tableRow2OilCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "% AGL"));
        tableRow2OilCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "% CP"));
        tableRow2OilCharacteristics.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Caracteristicas del Aceite"));
        tableRow2OilCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Valores"));
        tableRow2OilCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow2OilCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow2OilCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Condicion OK? (Si/No)"));
        tableRow2OilCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow2OilCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        celdaRow2OC.Add(tableRow2OilCharacteristics);
        tableVariablesArranque.AddCell(celdaRow2OC);

        /************************************************************************************************************
         *  Nivel de aceite
         ************************************************************************************************************/
        var celdaRow2OL = new Cell(1, 1).SetPaddings(2,0,2,2);
        var columnsRow2OL = new float[] { 40, 40, 20 };
        var tableRow2OilLevel = new Table(UnitValue.CreatePercentArray(columnsRow2OL)).UseAllAvailableWidth();
        tableRow2OilLevel.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Nivel de Aceite"));
        tableRow2OilLevel.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Valor"));
        tableRow2OilLevel.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow2OilLevel.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Conforme? (Si/No)"));
        tableRow2OilLevel.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        celdaRow2OL.Add(tableRow2OilLevel);
        tableVariablesArranque.AddCell(celdaRow2OL);

        /************************************************************************************************************
         *  Caracteristicas del producto
         ************************************************************************************************************/
        var celdaRow3PC = new Cell(1, 2).SetPaddings(2,0,2,0);
        var columnsRow3PC = new float[] { 29.25f, 16.25f, 9.75f, 9.75f, 9.75f, 25.25f };
        var tableRow3ProductCharacteristics = new Table(UnitValue.CreatePercentArray(columnsRow3PC)).UseAllAvailableWidth();
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 2, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "% Cloruro"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "% Humedad"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "% Grasa"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, ""));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Caracteristicas del producto frito"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Valores"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Condicion OK? (Si/No)"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        celdaRow3PC.Add(tableRow3ProductCharacteristics);
        tableVariablesArranque.AddCell(celdaRow3PC);

        /************************************************************************************************************
         *  Evaluacion sensorial de atributos de producto frito
         ************************************************************************************************************/
        var celdaRow4ES = new Cell(1, 2).SetPaddings(2,0,2,0);
        var columnsRow4ES = new float[] { 32.5f, 4.875f, 4.875f, 4.875f, 4.875f, 4.875f, 4.875f, 20.25f, 18 };
        var tableRow4EvaluacionSensorial = new Table(UnitValue.CreatePercentArray(columnsRow4ES)).UseAllAvailableWidth();
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 6, "Hora:"));
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 2, " "));

        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Evaluacion Sensorial de Atributos del Producto Frito \n" +
            "Calificacion Final: \n" +
            "1= Muy Malo 2=Malo 3=Regular 4=Bueno \n 5=Muy Bueno"));
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Apariencia General").SetRotationAngle(Math.PI / 2));
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateRotatedCell(1, 1, "Color").SetTextAlignment(TextAlignment.RIGHT));
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateRotatedCell(1, 1, "Olor"));
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateRotatedCell(1, 1, "Sabor"));
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateRotatedCell(1, 1, "Textura"));
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Calificacion Final").SetRotationAngle(Math.PI / 2));
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Panelista", TextAlignment.CENTER));
        tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Observaciones", TextAlignment.CENTER));

        foreach (var sensorial in sensoriales)
        {
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.AparienciaGeneral.ToString(), TextAlignment.CENTER));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Color.ToString(), TextAlignment.CENTER));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Olor.ToString(), TextAlignment.CENTER));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Sabor.ToString(), TextAlignment.CENTER));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Textura.ToString(), TextAlignment.CENTER));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.CalificacionFinal.ToString(), TextAlignment.CENTER));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Panelistas, TextAlignment.CENTER));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Observacion));
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
            if (observacion.Observaciones != null)
            {
                tableObservaciones.AddCell(new Cell()
                        .Add(new Paragraph(observacion.Observaciones)).SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(7)).SetBorder(Border.NO_BORDER);
            }
        }
        
        tableObservaciones.SetHeight(40f);
        celdaRow5OB.Add(tableObservaciones);
        tableVariablesArranque.AddCell(celdaRow5OB);
        PDFBuilder.RemoveAllBorder(tableVariablesArranque);
        document.Add(tableVariablesArranque);
        
        /************************************************************************************************************
         *  Signature (Firmas del Maquinista y del Coordinador)
         ************************************************************************************************************/ 
        PDFBuilder.SetSignature(document, "Maquinista", "Coordinador", arranque.Responsable);
        
        document.Close();

        return document;
    }
    public static Document GetTemplateAttributeEvaluation(Document document, EvaluacionAtributoResponse headEvaluacion )
    {
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<LineasEvaluacionAtributo> evaluaciones = headEvaluacion.EvaluacionAtributos;
        
        /************************************************************************************************************
         *  General Information  of Production
         ************************************************************************************************************/
        var columnsHead = new float[] { 6, 42, 20, 32 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "FECHA: "), 
            PDFBuilder.CreateCellFormat(1, 1, headEvaluacion.Fecha.ToString("dd-MM-yy")));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "NOMBRE DEL PRODUCTO: "), 
            PDFBuilder.CreateCellFormat(1, 1,  headEvaluacion.Descripcion));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "TURNO: "), 
            PDFBuilder.CreateCellFormat(1, 1,  headEvaluacion.Turno));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "O/FR: "), 
            PDFBuilder.CreateCellFormat(1, 1,  headEvaluacion.Orden));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "LINEA: "), 
            PDFBuilder.CreateCellFormat(1, 1,  headEvaluacion.Linea.ToString()));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "MAQUINISTA: "), 
            PDFBuilder.CreateCellFormat(1, 1,  headEvaluacion.Maquinista));
        PDFBuilder.RemoveAllBorder(tableHead);
        tableHead.SetMarginBottom(10f);
        document.Add(tableHead);
        
        var columnWidths = new float[] { 10, 20, 6, 6, 6, 6, 6, 6, 34 };
        var table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();
        table.AddCell(PDFBuilder.CreateCellFormat(2, 1, "HORA").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(2, 1, "PANELISTAS").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 6, "PRODUCTO A GRANEL").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(2, 1, "OBSERVACION").SetBold().SetTextAlignment(TextAlignment.CENTER));
        
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "APARIENCIA GENERAL").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "COLOR").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "OLOR").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "SABOR").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "TEXTURA").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CALIFICACION FINAL*").SetBold().SetTextAlignment(TextAlignment.CENTER));
        
        
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
                    table.AddCell(PDFBuilder.CreateCellFormat(cantidadPanelistas, 1, evaluacion.HoraMinutoSegundo).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(PDFBuilder.CreateCellFormat(1, 1, panelistas[i].Trim()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(PDFBuilder.CreateCellFormat(cantidadPanelistas, 1, evaluacion.AparienciaGeneral.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(PDFBuilder.CreateCellFormat(cantidadPanelistas, 1, evaluacion.Color.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(PDFBuilder.CreateCellFormat(cantidadPanelistas, 1, evaluacion.Olor.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(PDFBuilder.CreateCellFormat(cantidadPanelistas, 1, evaluacion.sabor.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(PDFBuilder.CreateCellFormat(cantidadPanelistas, 1, evaluacion.Textura.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(PDFBuilder.CreateCellFormat(cantidadPanelistas, 1, evaluacion.CalificacionFinal.ToString()).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(PDFBuilder.CreateCellFormat(cantidadPanelistas, 1, PDFBuilder.getEvaluateString(evaluacion.Observacion)).SetTextAlignment(TextAlignment.CENTER));
                }
                else
                {
                    table.AddCell(PDFBuilder.CreateCellFormat(1, 1, panelistas[i].Trim()).SetTextAlignment(TextAlignment.CENTER));
                }
            }
        }
        
        document.Add(table);
        document.Add(new Paragraph("*CALIFICACION FINAL").SetBold().SetFontSize(7));
        document.Add(new Paragraph("1:Muy Malo  2:Malo  3:Regular  4:Bueno  5:Muy Bueno").SetFontSize(7));
        
        /************************************************************************************************************
         *  Signature (Coordinador de Produccion)
         ************************************************************************************************************/ 
        PDFBuilder.SetSignature(document, "", "Coordinador de Produccion");
        
        document.Close();
        
        return document;
    }

    public static Document GetTemplateControlAceites(Document document, List<ControlParametroCalidadAceiteResponse> calidadAceites)
    {
      
        /************************************************************************************************************
         *  CONTROL DE PARÁMETROS DE CALIDAD DE ACEITE
         ************************************************************************************************************/
        var columnsVerif = new float[] { 7,6,6,6,6,7,6,6,6,14, 6,6,6,6,6};
        var tableVerif = new Table(UnitValue.CreatePercentArray(columnsVerif)).UseAllAvailableWidth();
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Fecha", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Orden de Fritura", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Producto Frito", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Sal / Sabor", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Etapa del Proceso", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Aceite", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Inicio de Fuente", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Relleno de Fritura", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Observaciones", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "AGL", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CP", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Color", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Olor", TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Ejecuta", TextAlignment.CENTER).SetBold());
        
        foreach (var rowIns in calidadAceites)
        {
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.fechaHora.ToString("dd/MM/yyyy"))).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.fechaHora.ToString("HH:mm:ss"))).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.ordenId)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.producto)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.sabor)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.etapa)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.aceite)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.inicioFuente)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.rellenoFuente)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.observacion)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.agl)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.cp)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.color)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.olor)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.usuario)).SetTextAlignment(TextAlignment.CENTER));
        }
        tableVerif.SetMarginBottom(10f);
        document.Add(tableVerif);
        
        return document;
    }
    public static Document GetTemplateCaracterizacionProductosTerminados(Document document, IEnumerable<dynamic> productoTerminado)
    {
        // Obtener las claves de la primera fila para determinar el número de columnas
        var firstItem = productoTerminado.FirstOrDefault() as IDictionary<string, object>;
        if (firstItem == null)
        {
            // No hay datos para mostrar
            return document;
        }

        // Crear una tabla con un número de columnas igual al número de claves
        int numColumns = firstItem.Count;
        Table table = new Table(numColumns);
        table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
        
        table.AddCell(PDFBuilder.CreateCellFormatHead(4, 1, "FECHA").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(4, 1, "N\u00b0 DE ORDEN DE FRITURA").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(4, 1, "LINEA").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(4, 1, "PRODUCTO FRITO").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(4, 1, "Peso de muestra  (200g)").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(1, numColumns-9, "DEFECTOS DEL PRODUCTO FRITO").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(4, 1, "ETAPA DEL PROCESO").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(4, 1, "OBSERVACIONES").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(4, 1, "PUNTA ESTRELLA").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(4, 1, "INSPECTOR").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(1, numColumns-9, "PRODUCTO DE LINEA").SetBold());
        table.AddCell(PDFBuilder.CreateCellFormatHead(1, numColumns-9, "Parámetros variables según la Cartilla de Caracterización de PT , clasificado por base.").SetBold());
 
        // Agregar las claves como encabezados de columna
        foreach (var key in firstItem.Keys)
        {
            // fechaHora, ordenId, linea, producto, peso
            // columnas dinamicas
            // etapa, observacion, usuario, inspector

            if (!(key == "fechaHora" || key == "ordenId" || key == "linea" || key == "producto" || key == "peso"
                || key == "etapa" || key == "observacion" || key == "usuario" || key == "inspector"))
            {
                table.AddCell(PDFBuilder.CreateCellFormatHead(1, 1, key).SetBold());
            }
        }

        // Agregar los valores como celdas en la tabla
        foreach (var item in productoTerminado)
        {
            IDictionary<string, object> expando = (IDictionary<string, object>)item;

            foreach (var kvp in expando)
            {
                table.AddCell(PDFBuilder.CreateCellFormatHead(1, 1, kvp.Value.ToString()));
            }
        }
        
        // Agregar la tabla al documento
        document.Add(table);
        document.Add(new Paragraph("Leyenda: \n *ETAPA DEL PROCESO: Inicio, Intermedio y Final \n **Colocar iniciales").SetBold().SetFontSize(7));
        
        /************************************************************************************************************
         *  Signature (Coordinador de Produccion)
         ************************************************************************************************************/ 
        PDFBuilder.SetSignature(document, "", "Coordinador de Control de Calidad");
        
        return document;
    }


    
}