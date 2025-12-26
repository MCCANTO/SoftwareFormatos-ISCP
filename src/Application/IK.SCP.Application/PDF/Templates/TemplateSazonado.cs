using IK.SCP.Application.PDF.Helpers;
using IK.SCP.Application.PDF.Sazonado.Model;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Newtonsoft.Json;
using Border = iText.Layout.Borders.Border;
using Cell = iText.Layout.Element.Cell;
using Table = iText.Layout.Element.Table;

namespace IK.SCP.Application.PDF.Templates;

public class TemplateSazonado
{
    public static Document GetTemplateArranqueSazonado(Document document, ChecklistArranqueSazonadoResponse arranque )
    {
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        ArranqueProducto arranqueProductosSazonados = arranque.arranque;
        List<CondicionesPreviasSazonado> condicionesPreviasSazonados = arranque.listaCondicionesPrevias;
        List<VerificacionEquipoSazonado> equipoSazonados = arranque.listaVerificacionEquipo;
        
        
        /************************************************************************************************************
         *  General Information  of Production
         ************************************************************************************************************/
        var columnsHead = new float[] { 12, 21, 15, 20, 15,  15  };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "PRODUCTO: "), 
                                PDFBuilder.CreateCellFormat(1, 3, arranqueProductosSazonados.Producto));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "SABOR: "), 
                                    PDFBuilder.CreateCellFormat(1, 1,  arranque.Sabor));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "Linea"), 
                                    PDFBuilder.CreateCellFormat(1, 1,  arranqueProductosSazonados.Linea));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "RESPONSABLE: "), 
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
                                       PDFBuilder.CreateCellFormat(1, 1, arranque.FechaCreacion.ToString("yyyy-M-d")));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "TURNO").SetBorder(Border.NO_BORDER),
                                       PDFBuilder.CreateCellFormat(1, 1, arranque.Turno));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "HORA INICIO").SetBorder(Border.NO_BORDER),
                                       PDFBuilder.CreateCellFormat(1, 1, arranque.FechaCreacion.ToString("HH:mm:ss")));
        tableDataControl.SetMarginBottom(10f);
        document.Add(tableDataControl);

        /************************************************************************************************************
         *  Condiciones Previas de Arranque
         ************************************************************************************************************/
        var columnWidths = new float[] { 55, 10, 10, 35 };
        var header = new string[] { "Condiciones previas al arranque", "Si", "No", "Observaciones" };
        
        string[][] dataCondicionesArranque = condicionesPreviasSazonados
            .Select(condicion => new string[] { condicion.Nombre.ToString(), 
                                                condicion.Valor!=null?"X":"",
                                                condicion.Valor!=null?"":"X",
                                                condicion.Observacion==null?"":condicion.Observacion.ToString()
            })
            .ToArray();

        // Generar la tabla en el documento PDF
        PDFBuilder.GenerateTable(document, columnWidths, header, dataCondicionesArranque);

        /************************************************************************************************************
         *  Verificacion del equipo previa  al arranque
         ************************************************************************************************************/
        var columnsTable = new float[] { 4, 30, 25, 5, 5, 31 };
        var listVerificaciones = equipoSazonados
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
         *  Caracteristicas del producto
         ************************************************************************************************************/
        var celdaRow2PC = new Cell(1, 2).SetPaddings(2,0,2,0);
        var columnsRow2PC = new float[] { 35,15, 25,25};
        var tableRow2ProductCharacteristics = new Table(UnitValue.CreatePercentArray(columnsRow2PC)).UseAllAvailableWidth();
        tableRow2ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 2, " "));
        tableRow2ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "% Cloruro"));
        tableRow2ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Observaciones"));
        tableRow2ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Caracteristicas del producto frito"));
        tableRow2ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Valores"));
        tableRow2ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow2ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow2ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Condicion OK? (Si/No)"));
        tableRow2ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow2ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        celdaRow2PC.Add(tableRow2ProductCharacteristics);
        tableVariablesArranque.AddCell(celdaRow2PC);

        /************************************************************************************************************
         *  Caracteristicas del producto
         ************************************************************************************************************/
        var celdaRow3PC = new Cell(1, 2).SetPaddings(2,0,2,0);
        var columnsRow3PC = new float[] { 35,15, 25,25};
        var tableRow3ProductCharacteristics = new Table(UnitValue.CreatePercentArray(columnsRow3PC)).UseAllAvailableWidth();
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 2, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Peso(Kg)"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Observaciones"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Peso del sabor"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Inicio"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Final"));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableRow3ProductCharacteristics.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        celdaRow3PC.Add(tableRow3ProductCharacteristics);
        tableVariablesArranque.AddCell(celdaRow3PC);
        
        PDFBuilder.RemoveAllBorder(tableVariablesArranque);
        document.Add(tableVariablesArranque);
        
        /************************************************************************************************************
         *  Signature (Firmas del Maquinista y del Coordinador)
         ************************************************************************************************************/ 
        PDFBuilder.SetSignature(document, "Responsable (Nombre y firma)", "", arranque.Responsable);
        
        document.Close();
        
        return document;
    }
}