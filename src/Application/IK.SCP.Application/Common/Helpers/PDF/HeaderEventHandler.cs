using IK.SCP.Application.SAZ.Queries;
using iText.IO.Font.Constants;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace IK.SCP.Application.Common.Helpers.PDF;
public class HeaderEventHandler : IEventHandler
    {
        protected Document doc;
        protected string codigo;
        protected int linea;

        public HeaderEventHandler(Document doc, string codigo, int linea)
        {
            this.doc = doc;
            this.codigo = codigo;
            this.linea = linea;
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

        private string getNumberTemplate()
        {
            if (linea.ToString().Equals("0"))
            {
                return this.codigo;
            }
            else
            {
                return string.Concat("L",this.linea);
            }
        }

        private string getTitleDocument()
        {
            string result = "";
            if (linea.ToString().Equals("0"))
            {
                switch (this.codigo)
                {
                    case "54":
                        result = "Control de Remojo habas o Reposo maiz";
                        break;
                    
                }    
            }
            else
            {
                result = "CHECK LIST DE FRITURA: L" + linea;   
            }

            return result;
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
                                     CreateCellFormat(1, 1, "IKC.PRO.F." + getNumberTemplate()).SetBold());
            setLabelValue(tableHead, CreateCellFormat(2, 1, "Versión: " + version + " \n Fecha: " + fecha),
                                     CreateCellFormat(1, 1, getTitleDocument()).SetBold());

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
    }