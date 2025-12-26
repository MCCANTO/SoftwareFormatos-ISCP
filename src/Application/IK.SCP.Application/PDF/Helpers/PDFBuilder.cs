using System.Text.RegularExpressions;
using iText.IO.Image;

namespace IK.SCP.Application.PDF.Helpers;
using iText.Layout.Borders;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Border = iText.Layout.Borders.Border;
using Cell = iText.Layout.Element.Cell;
using Table = iText.Layout.Element.Table;

public class PDFBuilder
{
    public static Cell CreateRotatedCell(int rowSpan, int colSpan, string content, TextAlignment textAlignment = TextAlignment.CENTER)
    {
        Cell outerCell = new Cell(rowSpan, colSpan)
            .SetPadding(0)
            .SetMargin(0)
            .SetVerticalAlignment(VerticalAlignment.MIDDLE);

        Paragraph paragraph = new Paragraph(content == null ? "" : content)
            .SetHorizontalAlignment(HorizontalAlignment.CENTER)
            .SetRotationAngle(Math.PI / 2);

        outerCell.Add(paragraph).SetFontSize(7);

        return outerCell;
    }
    public static void RemoveAllBorder(Table table)
    {
        foreach (var cell in table.GetChildren())
        {
            ((Cell)cell).SetBorder(null);
        }
    }
    public static void RemoveInsideBorder(Table table)
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
    public static void GenerateTable(Document document, float[] columnWidths, string[] header, string[][] data)
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
                if (cellData.Equals("X"))
                {
                    table.AddCell(CreateCellFormat(1, 1, cellData).SetTextAlignment(TextAlignment.CENTER));
                }
                else
                {
                    table.AddCell(CreateCellFormat(1, 1, cellData).SetTextAlignment(TextAlignment.LEFT));
                }
            }
        }

        table.SetMarginBottom(10f);
        document.Add(table);
    }
    public static void GenerateTableCustom(Document document, float[] columnWidths, string data) {

        // Parsea el JSON-ARRAY;
        var mArrayData    = JArray.Parse(data);

        /************************************************************************************************************
         *  HEAD OF TABLE
         ************************************************************************************************************/
        var alignmentHead = TextAlignment.CENTER;
        var table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();
            table.AddCell(CreateCellFormat(2, 3, "II. Verificación de Equipo previa al arranque", alignmentHead).SetBold());
            table.AddCell(CreateCellFormat(1, 2, " Si: Check(✓), No: (X), No Aplica: (N.A.)", alignmentHead).SetBold());
            table.AddCell(CreateCellFormat(2, 1, "Observaciones / Causa aparente / Acciones inmediatas", alignmentHead).SetBold());
            table.AddCell(CreateCellFormat(1, 1, "Operativo", alignmentHead).SetBold());
            table.AddCell(CreateCellFormat(1, 1, "Limpio", alignmentHead).SetBold());

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
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(7);
                        table.AddCell(nombreCell);
                    }
                    
                } else {
                    // Si el detalle no es ".-", muestra ambas celdas de nombre y detalle
                    if (detalle["rowspan"].ToString() != "0")
                    {
                        var nombreCell = new Cell(Int16.Parse(detalle["rowspan"].ToString()), 1)
                            .Add(new Paragraph(nombre))
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(7);
                        table.AddCell(nombreCell);
                    }

                    var detalleCell = new Cell(1, 1)
                        .Add(new Paragraph(detalleTexto))
                        .SetTextAlignment(TextAlignment.LEFT)
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
    public static string IsNullDatetime(DateTime value, string format)
    {
        return value != null ? value.ToString(format) : string.Empty;
    }
    public static string IsNullString(string value)
    {
        return value != null ? value : string.Empty;
    }
    
    public static string IsNull<T>(T value)
    {
        return value != null ? value.ToString() : string.Empty;
    }
    public static void SetLabelValue(Table table, Cell label, Cell value)
    {
        table.AddCell(label);
        table.AddCell(value);
    }
    public static Cell CreateCellFormat(int rowSpan, int colSpan, string text, TextAlignment textAlignment = TextAlignment.LEFT)
    {
        const int DefaultHeight = 10;
        const int DefaultFontSize = 7;

        if (string.IsNullOrEmpty(text))
        {
            return new Cell().SetHeight(DefaultHeight)
                .SetFontSize(DefaultFontSize)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
        }
    
        return new Cell(rowSpan, colSpan)
            .SetTextAlignment(textAlignment)
            .Add(new Paragraph(text))
            .SetFontSize(DefaultFontSize)
            .SetVerticalAlignment(VerticalAlignment.MIDDLE);
    }
    
    public static Cell CreateCellFormatHead(int rowSpan, int colSpan, string text, TextAlignment textAlignment = TextAlignment.CENTER)
    {
        return new Cell(rowSpan, colSpan)
                .SetTextAlignment(textAlignment)
                .Add(new Paragraph(text))
                .SetFontSize(5)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);   
    }
    public static void SetSignature(Document document, string namePerson1, string namePerson2, string valorPerson1 = "", string valorPerson2 = "")
    {
        var columnsSignature = new float[] { 35, 15, 15, 35 };
        var tableSignature = new Table(UnitValue.CreatePercentArray(columnsSignature)).UseAllAvailableWidth();
    
        AddSignatureRow(tableSignature, false, namePerson1.Equals("") ? "" : valorPerson1, "");
        AddSignatureRow(tableSignature, false,"", namePerson2.Equals("") ? "" : valorPerson2);
        
        AddSignatureRow(tableSignature, true,namePerson1, "");
        AddSignatureRow(tableSignature, true,"", namePerson2);
    
        tableSignature.SetMarginTop(40f);
        document.Add(tableSignature);
    }

    public static void AddSignatureRow(Table table, bool border, string cell1Text, string cell2Text)
    {
        Border topBorder = new SolidBorder(1);
        Cell cell1 = CreateCellFormat(1, 1, cell1Text, TextAlignment.CENTER).SetBorder(Border.NO_BORDER);
        Cell cell2 = CreateCellFormat(1, 1, cell2Text, TextAlignment.CENTER).SetBorder(Border.NO_BORDER);
    
        if (border)
        {
            if (!string.IsNullOrEmpty(cell1Text))
            {
                cell1.SetBorderTop(topBorder);
            }
            else if (!string.IsNullOrEmpty(cell2Text))
            {
                cell2.SetBorderTop(topBorder);
            }
        }

        SetLabelValue(table, cell1, cell2);
    }
    

    // public static setSpaceIntoTable(Document document)
    // {
    //     document.Add()
    // }

    public static void saltoLinea(Document document)
    {
        // Crear un salto de página
        AreaBreak areaBreak = new AreaBreak();

        // Agregar la tabla tableSanc en la nueva página
        document.Add(areaBreak);
    }

    public static string getEvaluateString(string valor)
    {
        return valor.Equals("") ? " " : valor;
    }
    
    public static string GetMonthName(string monthNumber)
    {
        Dictionary<string, string> monthNames = new Dictionary<string, string>
        {
            {"01", "Enero"},
            {"02", "Febrero"},
            {"03", "Marzo"},
            {"04", "Abril"},
            {"05", "Mayo"},
            {"06", "Junio"},
            {"07", "Julio"},
            {"08", "Agosto"},
            {"09", "Septiembre"},
            {"10", "Octubre"},
            {"11", "Noviembre"},
            {"12", "Diciembre"}
        };

        return monthNames.TryGetValue(monthNumber, out string monthName) ? monthName : "Mes no válido";
    }
    
    public static string StripHtmlTags(string input)
    {
        // Reemplazar <br/> por saltos de línea \n
        string stringWithLineBreaks = Regex.Replace(input, "<br\\s*/?>", " - ");

        // Eliminar otras etiquetas HTML
        string strippedString = Regex.Replace(stringWithLineBreaks, "<.*?>", string.Empty);

        return strippedString;
    }

    //public static Cell getCellxImage(int rowSpan, int colSpan, string imagePath, bool border = false)
    //{
    //    // Crear un objeto Image
    //    Image image = new Image(ImageDataFactory.Create(imagePath));

    //    // Ajustar el tamaño de la imagen si es necesario
    //    image.SetWidth(120); // Reemplaza con el ancho deseado en puntos (1 punto = 1/72 pulgadas)
    //    image.SetHeight(35); // Reemplaza con el alto deseado en puntos (1 punto = 1/72 pulgadas)

    //    // Agregar la imagen al documento PDF
    //    if (border) {
    //        return new Cell(rowSpan, colSpan).Add(image);
    //    } else {
    //        return new Cell(rowSpan, colSpan).Add(image).SetBorder(Border.NO_BORDER);
    //    }
    //}
    public static Cell getCellxImage(int rowSpan, int colSpan, string imagePath, bool border = false)
    {
        try
        {
            // Validar si la ruta es nula o vacía
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return CreateErrorTextCell("Sin imagen", rowSpan, colSpan, border);
            }

            // Verificar si el archivo existe
            if (!File.Exists(imagePath))
            {
                // Puedes agregar un log aquí si quieres
                // Console.WriteLine($"Advertencia: Imagen no encontrada: {imagePath}");
                return CreateErrorTextCell("Imagen no disponible", rowSpan, colSpan, border);
            }

            // Crear un objeto Image
            Image image = new Image(ImageDataFactory.Create(imagePath));

            // Ajustar el tamaño de la imagen si es necesario
            image.SetWidth(120); // Reemplaza con el ancho deseado en puntos (1 punto = 1/72 pulgadas)
            image.SetHeight(35); // Reemplaza con el alto deseado en puntos (1 punto = 1/72 pulgadas)

            // Agregar la imagen al documento PDF
            Cell cell = new Cell(rowSpan, colSpan).Add(image);

            if (!border)
            {
                cell.SetBorder(Border.NO_BORDER);
            }

            return cell;
        }
        catch (Exception ex) when (ex is IOException || ex is FileNotFoundException || ex is DirectoryNotFoundException)
        {
            // Error específico de archivo
            return CreateErrorTextCell("Error al cargar", rowSpan, colSpan, border);
        }
        catch (Exception ex)
        {
            // Cualquier otro error
            return CreateErrorTextCell("Error", rowSpan, colSpan, border);
        }
    }

    private static Cell CreateErrorTextCell(string message, int rowSpan, int colSpan, bool border)
    {
        // Crear una celda con mensaje de error
        Cell errorCell = new Cell(rowSpan, colSpan)
            .Add(new Paragraph(message)
                .SetFontSize(6)
                .SetFontColor(ColorConstants.RED)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE));

        if (!border)
        {
            errorCell.SetBorder(Border.NO_BORDER);
        }

        // Establecer altura mínima para que coincida con las celdas de imagen
        errorCell.SetHeight(35);

        return errorCell;
    }
}