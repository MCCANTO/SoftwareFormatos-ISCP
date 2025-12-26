    namespace IK.SCP.Application.PDF.Helpers;
    using iText.IO.Font.Constants;
    using iText.Kernel.Events;
    using iText.Kernel.Font;
    using iText.Kernel.Geom;
    using iText.Layout;
    using iText.Layout.Element;
    using iText.Layout.Properties;

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
            
            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                Rectangle pageSize = docEvent.GetPage().GetPageSize();
                PdfFont font = null;
                try {
                    font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);
                } catch (IOException e) {
                    Console.Error.WriteLine(e.Message);
                }

                doc.SetTopMargin(58f); // 20f + 

                float coordX = ((pageSize.GetLeft() + doc.GetLeftMargin())
                                 + (pageSize.GetRight() - doc.GetRightMargin())) / 2;
                float headerY = pageSize.GetTop() - doc.GetTopMargin() + 10;
                float footerY = doc.GetBottomMargin();

                // ---------------------------------------------------------------------------------
                //                 |                                         |                       
                //     LOGO        |                                         |    Version 07             
                //   INKACROPS     |                                         |    Fecha: 26/07/2022                  
                //                 |                                         |                      
                // ---------------------------------------------------------------------------------    

                // Crear una tabla con 3 filas y 3 columnas
                var tableHead = new Table(UnitValue.CreatePercentArray(new float[] { 20, 60, 20 })).UseAllAvailableWidth();
                
                // Evaluar la version y fecha 
                var version = 1;
                var fecha = "05/05/2021";
                
                // Agregar Logo InkaCrops con RowSpan
                setLabelValue(tableHead, CreateCellFormat(2, 1, "Image Inkacrops"),
                                         CreateCellFormat(1, 1, this.Codigo).SetBold());
                setLabelValue(tableHead, CreateCellFormat(2, 1, "Versión: " + this.Version + " \n Fecha: " + this.Fecha),
                                         CreateCellFormat(1, 1, this.Descripcion).SetBold());

                // Agregar margen y relleno a la tabla
                tableHead.SetMarginLeft(36f);
                tableHead.SetMarginRight(36f);
                tableHead.SetMarginTop(20f);

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