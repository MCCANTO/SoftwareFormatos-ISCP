    namespace IK.SCP.Application.PDF.Helpers;
    using iText.IO.Font.Constants;
    using iText.Kernel.Events;
    using iText.Kernel.Font;
    using iText.Kernel.Geom;
    using iText.Layout;
    using iText.Layout.Element;
    using iText.Layout.Properties;
    using iText.IO.Image;
    using iText.Layout.Element;
    using iText.Layout.Properties;
    using iText.Layout.Borders;


public class HeaderDocument : IEventHandler
    {
        private Document doc;
        private string Codigo;
        private string Descripcion;
        private string Version;
        private string Fecha;

            public HeaderDocument(Document doc, InformacionHeadDocument objHeadDocument)
            {
                this.doc = doc;
                this.Codigo = objHeadDocument.Code;
                this.Descripcion = objHeadDocument.Descripcion;
                this.Version = objHeadDocument.Version;
                this.Fecha = objHeadDocument.Fecha;
            }

    //public void HandleEvent(Event @event)
    //{
    //    PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
    //    Rectangle pageSize = docEvent.GetPage().GetPageSize();
    //    PdfFont font = null;
    //    try {
    //        font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);
    //    } catch (IOException e) {
    //        Console.Error.WriteLine(e.Message);
    //    }

    //    doc.SetTopMargin(58f); // 20f + 

    //    float coordX = ((pageSize.GetLeft() + doc.GetLeftMargin())
    //                     + (pageSize.GetRight() - doc.GetRightMargin())) / 2;
    //    float headerY = pageSize.GetTop() - doc.GetTopMargin() + 10;
    //    float footerY = doc.GetBottomMargin();

    //    // ---------------------------------------------------------------------------------
    //    //                 |                                         |                       
    //    //     LOGO        |                                         |    Version 07             
    //    //   INKACROPS     |                                         |    Fecha: 26/07/2022                  
    //    //                 |                                         |                      
    //    // ---------------------------------------------------------------------------------    

    //    // Crear una tabla con 3 filas y 3 columnas
    //    var tableHead = new Table(UnitValue.CreatePercentArray(new float[] { 20, 60, 20 })).UseAllAvailableWidth();

    //    // Evaluar la version y fecha 
    //    var version = 1;
    //    var fecha = "05/05/2021";

    //    // Agregar Logo InkaCrops con RowSpan
    //    setLabelValue(tableHead, CreateCellFormat(2, 1, "Image Inkacrops1"),
    //                             CreateCellFormat(1, 1, this.Codigo).SetBold());
    //    setLabelValue(tableHead, CreateCellFormat(2, 1, "Versión: " + this.Version + " \n Fecha: " + this.Fecha),
    //                             CreateCellFormat(1, 1, this.Descripcion).SetBold());

    //    // Agregar margen y relleno a la tabla
    //    tableHead.SetMarginLeft(36f);
    //    tableHead.SetMarginRight(36f);
    //    tableHead.SetMarginTop(20f);

    //    Canvas canvas = new Canvas(docEvent.GetPage(), pageSize);
    //    canvas
    //        .SetFont(font)
    //        .SetFontSize(5)
    //        .Add(tableHead)
    //        .ShowTextAligned("Serviam Servicios Compartidos SA (Perú)", coordX, footerY, TextAlignment.CENTER)
    //        .Close();
    //}

    public void HandleEvent(Event @event)
    {
        PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
        Rectangle pageSize = docEvent.GetPage().GetPageSize();

        PdfFont font = null;
        try
        {
            font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);
        }
        catch (IOException e)
        {
            Console.Error.WriteLine(e.Message);
        }

        doc.SetTopMargin(58f);

        float coordX = ((pageSize.GetLeft() + doc.GetLeftMargin())
                       + (pageSize.GetRight() - doc.GetRightMargin())) / 2;
        float footerY = doc.GetBottomMargin();

        // Tabla 3 columnas: Logo | Código/Descripción | Versión/Fecha
        var tableHead = new Table(UnitValue.CreatePercentArray(new float[] { 20, 60, 20 }))
            .UseAllAvailableWidth();

        // -------------------- LOGO --------------------
        var logoPath = System.IO.Path.Combine(
            System.IO.Directory.GetCurrentDirectory(),
            "Assets",
            "logo_inka.png"
        );

        Cell cellLogo;
        if (File.Exists(logoPath))
        {
            var logoData = ImageDataFactory.Create(logoPath);
            var logo = new Image(logoData)
                .ScaleToFit(80, 35)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            cellLogo = new Cell(2, 1) // rowspan = 2
                .Add(logo)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
        }
        else
        {
            // Fallback si no encuentra el logo
            cellLogo = CreateCellFormat(2, 1, "INKACROPS")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
        }

        // Columna 1
        tableHead.AddCell(cellLogo);

        // -------------------- CENTRO: CÓDIGO / DESCRIPCIÓN --------------------
        // Fila 1 col 2: Código
        tableHead.AddCell(
            CreateCellFormat(1, 1, this.Codigo)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
        );

        // -------------------- DERECHA: VERSIÓN / FECHA (rowspan 2) --------------------
        tableHead.AddCell(
            CreateCellFormat(2, 1, $"Versión: {this.Version}\nFecha: {this.Fecha}")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
        );

        // Fila 2 col 2: Descripción
        tableHead.AddCell(
            CreateCellFormat(1, 1, this.Descripcion)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
        );

        // Márgenes del header
        tableHead.SetMarginLeft(36f);
        tableHead.SetMarginRight(36f);
        tableHead.SetMarginTop(20f);

        // Pintar en la página
        Canvas canvas = new Canvas(docEvent.GetPage(), pageSize);
        canvas
            .SetFont(font)
            .SetFontSize(5)
            .Add(tableHead)
            .ShowTextAligned("Serviam Servicios Compartidos SA (Perú)", coordX, footerY, TextAlignment.CENTER)
            .Close();
    }



    private void setLabelValue(Table table, Cell label, Cell value)
            {
                table.AddCell(label);
                table.AddCell(value);
            }
            private Cell CreateCellFormat(int rowSpan, int colSpan, string content, TextAlignment textAlignment = TextAlignment.CENTER)
            {
                return new Cell(rowSpan, colSpan)
                    .SetTextAlignment(textAlignment)
                    .Add(new Paragraph(content == null ? "" : content))
                    .SetFontSize(8);
            }
    }