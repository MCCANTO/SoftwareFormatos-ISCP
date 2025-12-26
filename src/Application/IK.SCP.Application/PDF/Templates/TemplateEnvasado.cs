using System.Text;
using IK.SCP.Application.PDF.Envasado.Model;
using IK.SCP.Application.PDF.Helpers;
using IK.SCP.Domain.Dtos;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace IK.SCP.Application.PDF.Templates;

public static class TemplateEnvasado
{
    public static void printDocumentArranqueMaquinistaEnvasado(Document document, ArranqueMaquinistaEnvasadoResponse arranquePDF)
        {
            /************************************************************************************************************
             *  Data entrante
             ************************************************************************************************************/
            List<CondicionesPrevias> condicionesPrevias = arranquePDF.condicionesPrevias;
            List<FechaVariableBasica> fechaVariableBasicas = arranquePDF.fechaVariableBasica;
 
            
            var columnsHead = new float[] { 50, 50 };
            var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
            
            // ---------------------------------------------------------------------------------
            //  Nro Orden EN/RE           _______________             Reempaque  Si____   No____                                                              
            //  Descripcion del Producto  _______________                                                                           
            //  Envasadora                _______________                                                             
            //  Fecha                     _______________                                                        
            //  Turno                     _______________
            //  Maquinista                _______________
            // ---------------------------------------------------------------------------------
            var celdaChild1 = new Cell(1, 1); 
            var celdaChild2 = new Cell(1, 1);
            
            var columnsChild1 = new float[] { 40, 60 };
            var tableChild1 = new Table(UnitValue.CreatePercentArray(columnsChild1)).UseAllAvailableWidth();
            PDFBuilder.SetLabelValue(tableChild1, PDFBuilder.CreateCellFormat(1, 1, "N\u00b0 Orden EN/RE"), 
                PDFBuilder.CreateCellFormat(1, 1, arranquePDF.OrdenId));
            PDFBuilder.SetLabelValue(tableChild1, PDFBuilder.CreateCellFormat(1, 1, "Descripcion del Producto"), 
                PDFBuilder.CreateCellFormat(1, 1, arranquePDF.Descripcion));
            PDFBuilder.SetLabelValue(tableChild1, PDFBuilder.CreateCellFormat(1, 1, "Fecha"), 
                PDFBuilder.CreateCellFormat(1, 1, arranquePDF.FechaCreacion.ToString()));
            PDFBuilder.SetLabelValue(tableChild1, PDFBuilder.CreateCellFormat(1, 1, "Turno"), 
                PDFBuilder.CreateCellFormat(1, 1, "NOCTURNO"));
            PDFBuilder.SetLabelValue(tableChild1, PDFBuilder.CreateCellFormat(1, 1, "Maquinista"), 
                PDFBuilder.CreateCellFormat(1, 1, arranquePDF.UsuarioCreacion));
            
            var columnsChild2 = new float[] { 30, 10,10,10,10 };
            var tableChild2 = new Table(UnitValue.CreatePercentArray(columnsChild2)).UseAllAvailableWidth();
            tableChild2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Reempaque:").SetBorder(Border.NO_BORDER));
            tableChild2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Si").SetBorder(Border.NO_BORDER));
            tableChild2.AddCell(PDFBuilder.CreateCellFormat(1, 1, arranquePDF.EsReempaque.Equals(true)?"X":" "));
            tableChild2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "No").SetBorder(Border.NO_BORDER));
            tableChild2.AddCell(PDFBuilder.CreateCellFormat(1, 1, arranquePDF.EsReempaque.Equals(false)?"X":" "));
            tableChild2.AddCell(PDFBuilder.CreateCellFormat(2, 5, " ").SetBorder(Border.NO_BORDER));
            tableChild2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Observaciones:").SetBorder(Border.NO_BORDER));
            tableChild2.AddCell(PDFBuilder.CreateCellFormat(1,4, arranquePDF.Observacion).SetBorder(Border.NO_BORDER));
            
            PDFBuilder.RemoveAllBorder(tableChild1);
            celdaChild1.Add(tableChild1);
            celdaChild2.Add(tableChild2);
            tableHead.AddCell(celdaChild1);
            tableHead.AddCell(celdaChild2);
            document.Add(tableHead);

            //                           ___________________                          ___________________
            //  HORA INICIO ENVASADO:   |__________________|  HORA FINAL ENVASADO:   |__________________|         
            //   ORDEN DE PRODUCCION                          ORDEN DE PRODUCCION     
            //

            // Agregar informacion (Hora Inicio Envasado - Hora Final Envasado)
            var tableHorasEnvasado = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
            PDFBuilder.SetLabelValue(tableHorasEnvasado, PDFBuilder.CreateCellFormat(1, 1, "HORA INICIO DE ENVASADO:\nORDEN DE PRODUCCION").SetBold(),
                PDFBuilder.CreateCellFormat(1, 1, arranquePDF.FechaCreacion.ToString()));
            PDFBuilder.SetLabelValue(tableHorasEnvasado, PDFBuilder.CreateCellFormat(1, 1, "HORA FINAL DE ENVASADO:\nORDEN DE PRODUCCION").SetBold(),
                PDFBuilder.CreateCellFormat(1, 1, arranquePDF.FechaModificacion.ToString()));

            PDFBuilder.RemoveAllBorder(tableHorasEnvasado);
            document.Add(tableHorasEnvasado);

            //
            // SE CONSIDERA ARRANQUE DE LA ENVASADORA CUANDO SE INICIA LA PRODUCCION EN EL TURNO, 
            // SE CAMBIA DE PRODUCTO O DESPUES DE UN REINICIO PROLONGADO
            // ---------------------------------------------------------------------------------
            //  CONDICIONES PREVIAS SOLO AL ARRANQUE        | SI | NO |     Observaciones       |                           
            // ---------------------------------------------------------------------------------
            //                                              |    |    |                         |   
            // ---------------------------------------------------------------------------------
            document.Add(new Paragraph("SE CONSIDERA ARRANQUE DE LA ENVASADORA CUANDO SE INICIA LA PRODUCCION EN EL TURNO, " +
                                       "SE CAMBIA DE PRODUCTO O DESPUES DE UN REINICIO PROLONGADO").SetFontSize(7).SetBold());
            
            /************************************************************************************************************
             *  Condiciones Previas
             ************************************************************************************************************/
            var columnWidths = new float[] { 45, 5, 5, 45 };
            var header = new string[] { "I. Condiciones previas al arranque", "Si", "No", "Observaciones" };
        
            string[][] dataCondicionesArranque = condicionesPrevias
                .Select(condicion => new string[] { condicion.Orden + ".-" + condicion.Descripcion, 
                    condicion.Valor.Equals("True") ? "X": " ",
                    condicion.Valor.Equals("False")? "X": " ",
                    condicion.Observacion==null?"":condicion.Observacion
                })
                .ToArray();

            // Generar la tabla en el documento PDF
            PDFBuilder.GenerateTable(document, columnWidths, header, dataCondicionesArranque);

            // Agregar letras pequeñas que estan debajo de la tabla Condiciones Previas
            document.Add(new Paragraph("¹Sensores, transportadores de sobres, paradas de emergencia, conos, puertas de envasadora, " +
                                       "separadores de bioseguridad, protectores acrílicos (si aplican)        ²Tijera \n" +
                                       "³Se verifica cuando hay cambio de producto o variación del peso del sobre       " +
                                       "⁴Se realiza después de sacar las tolvas de pesado para una limpieza \n" +
                                       "5Se debe retroalimentar al personal sobre el método de llenado de bolsas en cajas, " +
                                       "correcta codificación, correcto paletizado, correcto control con la balanza, etc.").SetFontSize(6));


            // LOS CONTROLES BÁSICOS SE APLICARÁN ANTES Y DURANTE LA OPERACIÓN DE LA ENVASADORA EN 3 MOMENTOS DEL TURNO 
            //
            // ---------------------------------------------------------------------------------------------
            //          VARIABLES BÁSICAS PARA ARRANQUE Y OPERACIÓN DE ENVASADORAS
            // ---------------------------------------------------------------------------------------------
            //                                              |_____________________________|                 |                           
            //                  Controles Basicoz           |_Inicio_|_Intermedio_|_Final_|  Observaciones  |                           
            //                                              |        |            |       |                 |                           
            // ---------------------------------------------------------------------------------------------
            //                                              |        |            |       |                 |   
            // ---------------------------------------------------------------------------------------------
            document.Add(new Paragraph("LOS CONTROLES BÁSICOS SE APLICARÁN ANTES Y DURANTE LA OPERACIÓN DE LA ENVASADORA " +
                                       "EN 3 MOMENTOS DEL TURNO").SetFontSize(7));
            // Agregar Tabla Variables Basicas
            var columnVariablesBasicas = new float[] { 10,35, 7,7,7, 34 };
            var tableVariablesBasicas = new Table(UnitValue.CreatePercentArray(columnVariablesBasicas)).UseAllAvailableWidth();
            
            tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 6, "VARIABLES BÁSICAS PARA ARRANQUE Y OPERACIÓN DE ENVASADORAS"));
            tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(3, 2, "Controles Básicos"));
            tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Control en Turno"));
            tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Observaciones"));
            tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Inicio"));
            tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Intermedio"));
            tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Final"));

            // Horas de inicio-intermedio-final (Dinamicas)
            int totalIteraciones = 3;

            for (int i = 0; i < totalIteraciones; i++) {
                if (i < fechaVariableBasicas.Count) {
                    tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, fechaVariableBasicas[i].fecha.ToString("HH:mm")));
                }
                else {
                    tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, string.Empty));
                }
            }


            //Agregar contenido de la tabla con un iterador-foreach-while
            // Envasados (contar cuantos hay??), se agregaria en new Cell(3, 1)
            List<VariablesBasicas> filteredEnvasadora = arranquePDF.variablesBasicas
                .Where(variable => variable.Padre == "1. Envasadora")
                .ToList();

            tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(filteredEnvasadora.Count, 1, "Envasadora"));

            foreach (var rowFilteredEnvasadora in filteredEnvasadora)
            {
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredEnvasadora.Nombre));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredEnvasadora.Inicio));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredEnvasadora.Intermedio));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredEnvasadora.Final));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, "--"));
            }


            List<VariablesBasicas> filteredSobres = arranquePDF.variablesBasicas
                .Where(variable => variable.Padre == "2. Sobres")
                .ToList();
            // Sobres (contar cuantos hay??), se agregaria en new Cell(3, 1)
            tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(filteredSobres.Count, 1, "Sobres"));

            // for sobres
            foreach (var rowFilteredSobres in filteredSobres)
            {
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredSobres.Nombre));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredSobres.Inicio));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredSobres.Intermedio));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredSobres.Final));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, "--"));
            }
            
            List<VariablesBasicas> filteredCajas = arranquePDF.variablesBasicas
                .Where(variable => variable.Padre == "3. Cajas")
                .ToList();
            // Cajas (contar cuantos hay??), se agregaria en new Cell(3, 1)
            tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(filteredCajas.Count, 1, "Cajas"));

            // for cajas
            foreach (var rowFilteredCajas in filteredCajas)
            {
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredCajas.Nombre));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredCajas.Inicio));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredCajas.Intermedio));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowFilteredCajas.Final));
                tableVariablesBasicas.AddCell(PDFBuilder.CreateCellFormat(1, 1, "--"));
            }

            document.Add(tableVariablesBasicas);

            document.Add(new Paragraph(" \n "));

            var tablePesos = new Table(UnitValue.CreatePercentArray(9)).UseAllAvailableWidth();

            // Cabecera con los labels de los Pesos
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 3, " "));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "1", TextAlignment.CENTER));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "2", TextAlignment.CENTER));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "3", TextAlignment.CENTER));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "4", TextAlignment.CENTER));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "5", TextAlignment.CENTER));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "PROMEDIO", TextAlignment.CENTER));

            // Row con la informacion de los pesos
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "PESO DEL SOBRE VACIO (TARA) (g)"));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, arranquePDF.PesoSobreVacio.ToString()));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "PESO DEL SOBRE CON PRODUCTO (g)"));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, arranquePDF.PesoSobreProducto1.ToString()));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, arranquePDF.PesoSobreProducto2.ToString()));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, arranquePDF.PesoSobreProducto3.ToString()));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, arranquePDF.PesoSobreProducto4.ToString()));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, arranquePDF.PesoSobreProducto5.ToString()));
            tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, arranquePDF.PesoSobreProductoProm.ToString()));

            document.Add(tablePesos);

            /************************************************************************************************************
             *  OBSERVACIONES
             ************************************************************************************************************/
            document.Add(new Paragraph("Observaciones:").SetFontSize(9));
            StringBuilder observacionesConcatenadas = new StringBuilder();

            foreach (var rowObservaciones in arranquePDF.observaciones)
            {
                observacionesConcatenadas.AppendLine(rowObservaciones.Observacion);
            }
            document.Add(new Paragraph(observacionesConcatenadas.ToString()).SetFontSize(7));

            /************************************************************************************************************
             *  SIGNATURE
             ************************************************************************************************************/
            PDFBuilder.SetSignature(document, "Maquinista", "Coordinador / Encargado", arranquePDF.UsuarioCreacion);
            
            document.Close();
        }
    public static void printDocumentArranqueEnvasado(Document document, ArranqueEnvasadoResponse arranquePDF)
        {
            
            /************************************************************************************************************
             *  Data entrante
             ************************************************************************************************************/
            List<CondicionesPreviasArranqueEnvasado> condicionesPrevias = arranquePDF.condicionPrevia;
            List<VariablesBasicasArranqueEnvasado> variablesBasicas = arranquePDF.VariablesBasicas;
            List<ImagenCodificacionSobre> imagenesSobres  = arranquePDF.ImagenesSobres;
            List<ImagenCodificacionCaja> imagenesCajas = arranquePDF.ImagenesCajas;
            List<CantidadesCajaSobre> cantidadesCajaSobres = arranquePDF.CantidadesValores;
            List<dynamic> empacadorPaletizadors = arranquePDF.empacadorPaletizador;
            List<EvaluacionSensorialArranqueComponente> evaluacionSensorial = arranquePDF.EvaluacionSensorial;
            List<ObservacionesArranque> observacionesArranques = arranquePDF.Observaciones;
            List<InspeccionEtiquetadoArranqueEnvasado> inspeccionEtiquetado = arranquePDF.InspeccionEtiquetado;
            List<dynamic> revisores = arranquePDF.revisores;
            List<ResponsablesVariablesBasicas> responsablesVB = arranquePDF.responsablesVarBasicas;
            
            // ---------------------------------------------------------------------------------                                                                           
            //  Descripcion del Producto  _______________          _____________________________                                                                 
            //  Nro Orden EN/RE           _______________          |                            |                                     
            //  Envasadora                _______________          |                            |                       
            //  Fecha                     _______________          |                            |                  
            //  Linea de fritura          _______________          |                            |
            //  Envasadora                _______________          |____________________________|
            //  Reempaque                 Si____   No____          Para mix: Incluir compo.... 
            //                                                      ____________________________
            //                                                     |Componentes|   |   |   |    |
            // ---------------------------------------------------------------------------------

            //-- Crear una tabla con 3 filas y 3 columnas
            var columnsTableHead = new float[] { 50, 50 };
            var tableHead = new Table(UnitValue.CreatePercentArray(columnsTableHead)).UseAllAvailableWidth();
            //
            //-- Crear la primera celda de la tabla padre
            var celdaChild1 = new Cell(1, 1);
            //
            //-- Crear una tabla interna para la primera celda
            var columnsTableChild1 = new float[] { 20, 20,20,20,20 };
            var tableChild1 = new Table(UnitValue.CreatePercentArray(columnsTableChild1));
            //
            //
            //-- Agregar contenido a la tabla interna de la primera celda
            //-- setLabelValue(Table, label, value)
            PDFBuilder.SetLabelValue(tableChild1, PDFBuilder.CreateCellFormat(1, 1, "Descripcion del Producto", TextAlignment.LEFT), 
                PDFBuilder.CreateCellFormat(1, 4, arranquePDF.DescripcionProducto, TextAlignment.LEFT));
            PDFBuilder.SetLabelValue(tableChild1, PDFBuilder.CreateCellFormat(1, 1, "Nro Orden EN/RE", TextAlignment.LEFT), 
                PDFBuilder.CreateCellFormat(1, 4, arranquePDF.Orden, TextAlignment.LEFT));
            PDFBuilder.SetLabelValue(tableChild1, PDFBuilder.CreateCellFormat(1, 1, "Fecha", TextAlignment.LEFT), 
                PDFBuilder.CreateCellFormat(1, 4, arranquePDF.Fecha.ToString(), TextAlignment.LEFT));
            PDFBuilder.SetLabelValue(tableChild1, PDFBuilder.CreateCellFormat(1, 1, "Linea de fritura", TextAlignment.LEFT), 
                PDFBuilder.CreateCellFormat(1, 4, arranquePDF.Linea, TextAlignment.LEFT));
            PDFBuilder.SetLabelValue(tableChild1, PDFBuilder.CreateCellFormat(1, 1, "Envasadora", TextAlignment.LEFT), 
                PDFBuilder.CreateCellFormat(1, 4, arranquePDF.NameEnvasadora, TextAlignment.LEFT));
            PDFBuilder.RemoveAllBorder(tableChild1);
            tableChild1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Reempaque:").SetBorder(Border.NO_BORDER));
            tableChild1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Si").SetBorder(Border.NO_BORDER));
            tableChild1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "X"));
            tableChild1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "No").SetBorder(Border.NO_BORDER));
            tableChild1.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
          
            var celdaChild2 = new Cell(1, 1);
            var tableChild2 = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
            //
            var celdaChild21 = new Cell(1, 1);
            var tableChild21 = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();

            foreach (var rowIns in imagenesSobres)
            {
                tableChild21.AddCell(PDFBuilder.getCellxImage(1,1, rowIns.Ruta));
            }
            
            tableChild21.SetHeight(80f);

            var celdaChild22 = new Cell(1, 1);
            var columnsTableChild22 = new float[] { 28, 18, 18, 18, 18 };
            var tableChild22 = new Table(UnitValue.CreatePercentArray(columnsTableChild22)).UseAllAvailableWidth();
            tableChild22.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Components").SetFontSize(7));
            tableChild22.AddCell(PDFBuilder.CreateCellFormat(1, 1, " - ").SetBold());
            tableChild22.AddCell(PDFBuilder.CreateCellFormat(1, 1, " - ").SetBold());
            tableChild22.AddCell(PDFBuilder.CreateCellFormat(1, 1, " - ").SetBold());
            tableChild22.AddCell(PDFBuilder.CreateCellFormat(1, 1, " - ").SetBold());
            //
            celdaChild21.Add(tableChild21);
            tableChild2.AddCell(celdaChild21);
            //
            tableChild2.AddCell("Para MIX:  Incluir componentes y ver reverso de hoja para detalles").SetFontSize(6);
            //
            
            celdaChild22.Add(tableChild22);
            tableChild2.AddCell(celdaChild22);
            tableChild2.AddCell("Consideraciones para la Blending: Si finaliza el Mixeo o hay una parada, descargar el " +
                "producto en bolos y \n filiar para evitar que el producto este expuesto mas de 3 min al ambiente.").SetFontSize(6);
            //PDFBuilder.RemoveAllBorder(tableChild2);
            //
            //-- Agregar Tabla a las celdas -> Agregar celda a las tabla Head
            celdaChild1.Add(tableChild1);
            celdaChild2.Add(tableChild2);
            tableHead.AddCell(celdaChild1);
            tableHead.AddCell(celdaChild2);
            PDFBuilder.RemoveAllBorder(tableHead);
            document.Add(tableHead);
            //
            // //
            // // ---------------------------------------------------------------------------------
            // //      CONDICIONES PREVIAS     | SI | NO |     CONDICIONES PREVIAS      | SI | NO |                           
            // // ---------------------------------------------------------------------------------
            // // ...                          |    |    | ...                          |    |    |          
            // // ---------------------------------------------------------------------------------
              //Agregar Tabla Condiciones Previas
             var tableCondicionesPrevias = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
             tableCondicionesPrevias.SetMargins(0, 0, 0, 0);
             tableCondicionesPrevias.SetPaddings(0, 0, 0, 0);
            
              //Crear la primera celda de la tabla padre
             var celdaCondPrevias1 = new Cell(1, 1);
            
              //Crear una tabla interna para la primera celda
             var columnsCondPrevias1 = new float[] { 80, 10, 10 };
             var tableCondPrevias1 = new Table(UnitValue.CreatePercentArray(columnsCondPrevias1)).UseAllAvailableWidth();
             tableCondPrevias1.SetMargins(0, 0, 0, 0);
             tableCondPrevias1.SetPaddings(0, 0, 0, 0);
            
             tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CONDICIONES PREVIAS").SetBold());
             tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "SI").SetBold());
             tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "NO").SetBold());
            
             int mitadObservaciones = (int)Math.Round(condicionesPrevias.Count / 2.0);
             List<CondicionesPreviasArranqueEnvasado> condPrevias1 = condicionesPrevias.Take(mitadObservaciones).ToList();
             List<CondicionesPreviasArranqueEnvasado> condPrevias2 = condicionesPrevias.Skip(mitadObservaciones).ToList();
            
             //Agregar contenido de la tabla con un iterador-foreach-while
              foreach (var rowCondicionesPrevias in condPrevias1)
              {
                  tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Nombre));
                  tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Valor.Equals(true)?"X":" "));
                  tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Valor.Equals(false)?"X":" "));
                  
              }
            
             var celdaCondPrevias2 = new Cell(1, 1);
            
              //Crear una tabla interna para la primera celda
             var columnsCondPrevias2 = new float[] { 80, 10, 10 };
             var tableCondPrevias2 = new Table(UnitValue.CreatePercentArray(columnsCondPrevias2)).UseAllAvailableWidth();
             tableCondPrevias2.SetMargins(0, 0, 0, 0);
             tableCondPrevias2.SetPaddings(0, 0, 0, 0);
            
             tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CONDICIONES PREVIAS").SetBold());
             tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "SI").SetBold());
             tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "NO").SetBold());
             //Agregar contenido de la tabla con un iterador-foreach-while
              foreach (var rowCondicionesPrevias in condPrevias2)
              {
                  tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Nombre));
                  tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Valor.Equals(true)?"X":" "));
                  tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Valor.Equals(false)?"X":" "));
              }
            
             celdaCondPrevias1.Add(tableCondPrevias1);
             celdaCondPrevias2.Add(tableCondPrevias2);
             tableCondicionesPrevias.AddCell(celdaCondPrevias1);
             tableCondicionesPrevias.AddCell(celdaCondPrevias2);
             tableCondicionesPrevias.SetMargins(0, 0, 0, 0);
             PDFBuilder.RemoveAllBorder(tableCondicionesPrevias);
             document.Add(tableCondicionesPrevias);
            //
            // //
            // // LOS CONTROLES BÁSICOS SE APLICARÁN ANTES Y DURANTE LA OPERACIÓN DE LA ENVASADORA EN 3 MOMENTOS DEL TURNO 
            // //
            // // ---------------------------------------------------------------------------------------------
            // //          VARIABLES BÁSICAS PARA ARRANQUE Y OPERACIÓN DE ENVASADORAS
            // // ---------------------------------------------------------------------------------------------
            // //               Turno             |   TA   |     TR     |   TC  |_______|                      |                           
            // //               Hora              |  16:00 |    17:00   | 18:00 |_______|     Observaciones    |                           
            // //               Maquinista        |        Jose Roberto         |       |                      |                            
            // // ---------------------------------------------------------------------------------------------
            // //                           VARIABLES BASICAS - ARRANQUE DE ENVASADO                           |   
            // // ---------------------------------------------------------------------------------------------
            // // 
            // //---------------------------------------------------------------------------------------------
            //
            document.Add(new Paragraph("Re-arranque: Se considera despues de una parada de 2 horas").SetFontSize(6));
            
            //  Agregar Tabla Variables Basicas
            var columnsDatosPrincipales = new float[] { 18, 30, 7, 7, 7, 7, 24 };
            var tableDatosPrincipales = new Table(UnitValue.CreatePercentArray(columnsDatosPrincipales)).UseAllAvailableWidth();
            
            
            foreach (var info in responsablesVB)
            {
                tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 2, "Turno"));
                tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 4, info.Turno + " "));
                tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Observaciones"));
                
                tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 2, "Hora"));
                tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 4, info.Fechas));
                
                tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 2, "Maquinistas"));
                tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 4, info.Maquinistas));
                
                

            }
            
            tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 12, "VARIABLES BASICAS - ARRANQUE DE ENVASADO"));

            foreach (var rowIns in variablesBasicas)
            {
                
                tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Padre));
                
                tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Nombre));
                var valoresFaltantes = 4 -rowIns.Valor.Split(',').Length;
                foreach (string valor in rowIns.Valor.Split(',')) {
                    tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(valor)));
                }

                if (valoresFaltantes > 0)
                {
                    for (var i = 0; i<valoresFaltantes; i++)
                    {
                        tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString("")));
                    }
                }
                
                tableDatosPrincipales.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Observacion)));
                
            }
            
            
            tableDatosPrincipales.SetMarginBottom(10f); // Agregar un margen Bottom a la tabla
            document.Add(tableDatosPrincipales);
            document.Add(new Paragraph("Leyenda:   C:Conforme  NC: No Conforme  NA: No Aplica  P:Pendiente       (*) Solo se imprimela codificacion  \n ").SetFontSize(6).SetBold());
           
            // // Tabla de pesos
            

             var columnsPesos = new float[] { 16, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };
             var tablePesos = new Table(UnitValue.CreatePercentArray(columnsPesos)).UseAllAvailableWidth();
            
            // Cabecera con los labels de los Pesos
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Contramuestras"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Turno/ \n #Caja"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Cantidad de \n sobres"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "sobres \n retirados"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Turno/ \n #Caja"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Cantidad de \n sobres"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "sobres \n retirados"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Turno/ \n #Caja"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Cantidad de \n sobres"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "sobres \n retirados"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Turno/ \n #Caja"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Cantidad de \n sobres"));
             tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "sobres \n retirados"));

             var contador = 0;
             foreach (var rowCant in cantidadesCajaSobres)
             {
                 foreach (string valor in rowCant.Valores.Split(","))
                 {
                     tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, valor));
                     contador++;
                 }
             }

             if (12-contador > 0)
             {
                 for (int i = 0; i < 12-contador; i++)
                 {
                     tablePesos.AddCell(PDFBuilder.CreateCellFormat(1, 1, "-"));
                 }
             }
             
            
             tablePesos.SetMarginBottom(10f);  //Agregar un margen Bottom a la tabla
             document.Add(tablePesos);
            
             var tableEmpacadoraPaletizador = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();
             tableEmpacadoraPaletizador.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Empacador"));
             tableEmpacadoraPaletizador.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Paletizador"));
             tableEmpacadoraPaletizador.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Turno"));
            
             var itemsEmpPal = empacadorPaletizadors
                 .GroupBy(x => new { x.NroGrupo, x.UsuarioCreacion, x.FechaCreacion })
                 .Select(x => new
                 {
                     Item = x.Key.NroGrupo,
                     Usuario = x.Key.UsuarioCreacion,
                     Fecha = x.Key.FechaCreacion,
                     Empacadores = x.Where(f => f.CargoId == "EMPACADOR").Select(p => p.Nombre),
                     Paletizadores = x.Where(f => f.CargoId == "PALETIZADOR").Select(p => p.Nombre),
                 })
                 .OrderBy(o => o.Item)
                 .ToList();
             
             foreach (var rowIns in itemsEmpPal)
             {
                 var mStrEnpacador = " ";
                 foreach (string enpacador in rowIns.Empacadores)
                 {
                     mStrEnpacador = mStrEnpacador + enpacador + "\n";
                 }
                 
                 tableEmpacadoraPaletizador.AddCell(PDFBuilder.CreateCellFormat(1, 1, mStrEnpacador));
                 
                 var mStrPaletizador = " ";
                 foreach (string paletizador in rowIns.Paletizadores)
                 {
                     mStrPaletizador = mStrPaletizador + paletizador + "\n";
                 }
                 tableEmpacadoraPaletizador.AddCell(PDFBuilder.CreateCellFormat(1, 1, mStrPaletizador));
                 
                 tableEmpacadoraPaletizador.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Fecha.ToString()));
             }
            
             document.Add(tableEmpacadoraPaletizador);
            
            // OBSERVACIONES
            document.Add(new Paragraph("Observaciones:").SetFontSize(7).SetBold());
            
            foreach (var rowObservaciones in observacionesArranques)
            {
                 document.Add(new Paragraph(rowObservaciones.Observacion).SetFontSize(7));
            }
            
            // Leyenda Informacion adicional
            var tableLeyenda = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();
            tableLeyenda.AddCell(PDFBuilder.CreateCellFormat(1, 3, "*Material plástico duro/quebradizo e instrumento afilado integro"));
            tableLeyenda.AddCell(PDFBuilder.CreateCellFormat(1, 1, "1. Protectores acrílicos"));
            tableLeyenda.AddCell(PDFBuilder.CreateCellFormat(1, 1, "4. Bolos de Producto Terminado"));
            tableLeyenda.AddCell(PDFBuilder.CreateCellFormat(1, 1, "7. Separadores de bioseguridad"));
            tableLeyenda.AddCell(PDFBuilder.CreateCellFormat(1, 1, "2. Cono de envasadora"));
            tableLeyenda.AddCell(PDFBuilder.CreateCellFormat(1, 1, "5. Transportadores de sobres"));
            tableLeyenda.AddCell(PDFBuilder.CreateCellFormat(1, 1, "8. Tijera (Del modulo de calidad)"));
            tableLeyenda.AddCell(PDFBuilder.CreateCellFormat(1, 1, "3. Puerta de acrílico de envasadora"));
            tableLeyenda.AddCell(PDFBuilder.CreateCellFormat(1, 1, "6. Sensores"));
            tableLeyenda.AddCell(PDFBuilder.CreateCellFormat(1, 1, "9. Protectores de Luminarias)"));
            tableLeyenda.SetMarginTop(10f);
            tableLeyenda.SetMarginBottom(10f);
            PDFBuilder.RemoveAllBorder(tableLeyenda);
            document.Add(tableLeyenda);    
                
            // Evaluacion Sensroial
            var tableEvaluacionSensorial = new Table(UnitValue.CreatePercentArray(7)).UseAllAvailableWidth();
            tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Fecha Registro"));
            tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Componente"));
            tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Lote"));
            tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Humedad"));
            tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Evaluacion Sensorial"));
            tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Observacion"));
            tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Usuario"));

            foreach (var rowIns in evaluacionSensorial)
            {
                tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.FechaCreacion.ToString("M/d/yy")));
                tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Componente));
                tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Lote));
                tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Humedad));
                tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.EvaluacionSensorial));
                tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Observacion));
                tableEvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.UsuarioCreacion));
            }

            tableEvaluacionSensorial.SetMarginBottom(10f);  //Agregar un margen Bottom a la tabla
            document.Add(tableEvaluacionSensorial);
            
            
            // Evaluacion Sensroial
            var tableInspeccionEtiquetado = new Table(UnitValue.CreatePercentArray(7)).UseAllAvailableWidth();
            tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Fecha Registro"));
            tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Cantidad de cajas"));
            tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Persona de etiqueta"));
            tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Posicion de etiqueta"));
            tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Inspector"));
            tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Usuario"));
            tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Imagen"));

            foreach (var rowIns in inspeccionEtiquetado)
            {
                tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.FechaCreacion.ToString("M/d/yy")));
                tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.CantidadCajas));
                tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Etiquetador));
                tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Posicion));
                tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Inspector));
                tableInspeccionEtiquetado.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.UsuarioCreacion));
                tableInspeccionEtiquetado.AddCell(PDFBuilder.getCellxImage(1,1,rowIns.Imagen, true));
            }
            
            tableInspeccionEtiquetado.SetMarginBottom(10f);
            document.Add(tableInspeccionEtiquetado);

            var tableCodificacionHead = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
            var tableCodificacionCaja = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
            var celdaCodificacionCaja = new Cell(1, 1);
            
            foreach (var rowIns in imagenesCajas)
            {
                tableCodificacionCaja.AddCell(PDFBuilder.getCellxImage(1,1, rowIns.Ruta));
            }

            celdaCodificacionCaja.Add(tableCodificacionCaja);
            tableCodificacionHead.AddCell(celdaCodificacionCaja);
            tableCodificacionHead.SetHeight(320f);
            
            document.Add(tableCodificacionHead);
            
            /************************************************************************************************************
             *  SIGNATURE
             ************************************************************************************************************/
            PDFBuilder.SetSignature(document, "Punta Estrella (Nombre y Firma)", "Facilitador y/o Inspector de Calidad (Nombre y Firma)", "");
            
            // Cerrar el objeto Document
            document.Close();
        }
    public static void printDocumentArranqueBlending(Document document, ArranqueBlendingResponse arranqueBlending)
    {
        
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<ComponenteBlending> componentes = arranqueBlending.Componentes;
        List<ArranqueCondicionPreviaBlending> condicionPrevia = arranqueBlending.CondicionesPrevias;
        List<VerificacionEquipoBlending> verificacionEquipo = arranqueBlending.VerificacionesEquipo;
        List<ObservacionBlending> observacion = arranqueBlending.Observaciones;
        
        /************************************************************************************************************
         *  Data de control
         ************************************************************************************************************/
        var columnsDataControl = new float[] { 11, 19, 9, 21, 7, 23 };
        var tableDataControl = new Table(UnitValue.CreatePercentArray(columnsDataControl)).UseAllAvailableWidth();

        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "N\u00b0 ORDEN :"),
            PDFBuilder.CreateCellFormat(1, 1, arranqueBlending.Orden));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "PRODUCTO :"),
            PDFBuilder.CreateCellFormat(1, 3, arranqueBlending.Producto));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "RESPONSABLE :"),
            PDFBuilder.CreateCellFormat(1, 1, arranqueBlending.Responsable));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "FECHA :"),
            PDFBuilder.CreateCellFormat(1, 1, arranqueBlending.FechaInicio.ToString("yyyy-M-d")));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "TURNO :"),
            PDFBuilder.CreateCellFormat(1, 1, arranqueBlending.Turno));
        
        PDFBuilder.RemoveInsideBorder(tableDataControl);
        tableDataControl.SetMarginBottom(10f);
        document.Add(tableDataControl);
        
        var columnsFechas = new float[] { 21, 10, 21, 10, 21, 18 };
        var tableFechas = new Table(UnitValue.CreatePercentArray(columnsFechas)).UseAllAvailableWidth();

        PDFBuilder.SetLabelValue(tableFechas, PDFBuilder.CreateCellFormat(1, 1, "HORA INICIO PRODUCCIÓN :").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, arranqueBlending.FechaInicio.ToString("HH:mm")));
        PDFBuilder.SetLabelValue(tableFechas, PDFBuilder.CreateCellFormat(1, 1, "HORA FINAL PRODUCCIÓN :").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, arranqueBlending.Cerrado.ToString() == "1"? arranqueBlending.FechaFin.ToString("HH:mm") : " "));
        tableFechas.SetMarginBottom(10f);
        document.Add(tableFechas);
        
        /************************************************************************************************************
         *  Componentes
         ************************************************************************************************************/
        var columnComponentes = new float[] { 12, 80, 8 };
        var headerComponentes = new string[] { "Articulo", "Descripcion", "Porcentaje" };
        
        string[][] dataComponentes = componentes
            .Select(componente => new string[] { componente.Articulo, 
                componente.Descripcion,
                componente.Porcentaje.ToString("")
            })
            .ToArray();

        // Generar la tabla en el documento PDF
        PDFBuilder.GenerateTable(document, columnComponentes, headerComponentes, dataComponentes);
        
        /************************************************************************************************************
         *  Condiciones Previas de Arranque
         ************************************************************************************************************/
        var columnCondicionPrevia = new float[] { 55, 10, 10, 35 };
        var headerCondicionPrevia = new string[] { "I. Condiciones previas al arranque", "Si", "No", "Observaciones" };
        
        string[][] dataCondicionesPrevia = condicionPrevia
            .Select(condicion => new string[] { condicion.Orden + ".-" + condicion.Nombre, 
                condicion.Valor.Equals("True") ? "X": " ",
                condicion.Valor.Equals("False")? "X": " ",
                condicion.Observacion==null?"":condicion.Observacion
            })
            .ToArray();

        // Generar la tabla en el documento PDF
        PDFBuilder.GenerateTable(document, columnCondicionPrevia, headerCondicionPrevia, dataCondicionesPrevia);
        
        /************************************************************************************************************
         *  VERIFICACION DE EQUIPO
         ************************************************************************************************************/
        var columnsVerif = new float[] { 35, 15, 15, 35 };
        var tableVerif = new Table(UnitValue.CreatePercentArray(columnsVerif)).UseAllAvailableWidth();
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(2, 1, "II. Verificación de Equipo previa al arranque").SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 2, " Si: Check(✓), No: (X), No Aplica: (N.A.)").SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Observaciones / Causa aparente / Acciones inmediatas").SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Operativo").SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Limpio").SetBold());
        
        foreach (var rowIns in verificacionEquipo)
        {
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Nombre)).SetTextAlignment(TextAlignment.LEFT));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Operativo)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Limpio)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Observacion)).SetTextAlignment(TextAlignment.CENTER));
        }
        document.Add(tableVerif);
        document.Add(new Paragraph("* Materiales plásticos duros/quebradizos e instrumentos afilados").SetFontSize(6));
        
        /************************************************************************************************************
         *  Observaciones
         ************************************************************************************************************/
        document.Add(new Paragraph("Observaciones:").SetFontSize(7));
        foreach (var rowObs in observacion)
        {
            document.Add(new Paragraph(PDFBuilder.IsNullString(rowObs.valor)).SetFontSize(7));
        }
        
        /************************************************************************************************************
         *  SIGNATURE
         ************************************************************************************************************/
        PDFBuilder.SetSignature(document, "Responsable (Nombre y Firma)", "Maquinista (Nombre y firma)", arranqueBlending.Responsable);

    }
    public static void printDocumentControlBlending(Document document, ControlBlendingResponse controlBlending)
    {
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<ComponenteMix> componentes = controlBlending.componentes;
        List<HeadTableBlending> headTable = controlBlending.headTable;
        List<MermaBlending> merma = controlBlending.merma;
        List<dynamic> dataTable = controlBlending.dataTable;
        
        /************************************************************************************************************
         *  Datos de control - Componentes del mix
         ************************************************************************************************************/
        var columnsHead = new float[] { 50, 50 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        
        // 2 sub tablas
        var cellDatosControl   = new Cell();
        var cellComponentesMix = new Cell();
        
        var columnsDatosControl = new float[] { 15, 20, 20, 45 };
        var tableDatosControl = new Table(UnitValue.CreatePercentArray(columnsDatosControl)).UseAllAvailableWidth();
        PDFBuilder.SetLabelValue(tableDatosControl, PDFBuilder.CreateCellFormat(1, 1, "Orden Reempaque: "), 
            PDFBuilder.CreateCellFormat(1, 1, controlBlending.Orden));
        PDFBuilder.SetLabelValue(tableDatosControl, PDFBuilder.CreateCellFormat(1, 1, "Descripción del producto: "), 
            PDFBuilder.CreateCellFormat(1, 1, controlBlending.Producto));
        PDFBuilder.SetLabelValue(tableDatosControl, PDFBuilder.CreateCellFormat(1, 1, "Fecha: "), 
            PDFBuilder.CreateCellFormat(1, 3, controlBlending.Fecha.ToString("dd/MM/yyyy")));
        PDFBuilder.SetLabelValue(tableDatosControl, PDFBuilder.CreateCellFormat(1, 1, "Turno: "), 
            PDFBuilder.CreateCellFormat(1, 1, controlBlending.Turno));
        PDFBuilder.SetLabelValue(tableDatosControl, PDFBuilder.CreateCellFormat(1, 1, "Maquinista: "), 
            PDFBuilder.CreateCellFormat(1, 1, controlBlending.Maquinista));
        
        var columnsComponentesMix = new float[] { 10,50,8,8,8, 16 };
        var tableComponentesMix = new Table(UnitValue.CreatePercentArray(columnsComponentesMix)).UseAllAvailableWidth();
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Articulo"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Descripcion"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "% Mezcla"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Granel"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Linea"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Orden"));
        
        foreach (var rowIns in componentes)
        {
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Articulo)).SetTextAlignment(TextAlignment.LEFT));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Descripcion)).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Porcentaje)).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Granel)).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Linea)).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.OrdenFritura)).SetTextAlignment(TextAlignment.CENTER));
        }
        
        PDFBuilder.RemoveAllBorder(tableDatosControl);
        cellDatosControl.Add(tableDatosControl);
        cellComponentesMix.Add(tableComponentesMix);
        tableHead.AddCell(cellDatosControl);
        tableHead.AddCell(cellComponentesMix);
        PDFBuilder.RemoveAllBorder(tableHead);
        tableHead.SetMarginBottom(10f);
        document.Add(tableHead);

        /************************************************************************************************************
         *  Componentes Dinamicos
         ************************************************************************************************************/
        var cantidadColumnas = 2 + (headTable.Count * 3) + 1;  
        var tableControl = new Table(UnitValue.CreatePercentArray(cantidadColumnas)).UseAllAvailableWidth();
        tableControl.AddCell(PDFBuilder.CreateCellFormat(2, 1, "USUARIO"));
        tableControl.AddCell(PDFBuilder.CreateCellFormat(2, 1, "FECHA"));
        foreach (var rowIns in headTable) {
            tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 3, rowIns.Descripcion));
        }
        tableControl.AddCell(PDFBuilder.CreateCellFormat(2, 1, "OBSERVACION"));
        foreach (var rowIns in headTable) {
            tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CANTIDAD DE CAJAS O BOLOS"));
            tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, "KG X CAJA O BOLOS"));
            tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, "LOTE"));
        }
        
        // Data de la tabla
        foreach (var rowIns in dataTable)
        {
            tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Usuario));
            tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Fecha.ToString("dd/MM/yyyy")));

            foreach (var rowH in headTable)
            {
                string valor1 = Calculos.ObtenerValorEnPosicion(Calculos.ObtenerValorAtributo(rowIns, rowH.Articulo).ToString(), 0).ToString();
                string valor2 = Calculos.ObtenerValorEnPosicion(Calculos.ObtenerValorAtributo(rowIns, rowH.Articulo).ToString(), 1).ToString();
                string valor3 = Calculos.ObtenerValorEnPosicion(Calculos.ObtenerValorAtributo(rowIns, rowH.Articulo).ToString(), 2).ToString();
                
                tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, valor1));
                tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, valor2));
                tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, valor3));
            }
            tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Observacion)));
        }
        tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 2, "TOTAL USADO (kg)"));
    
        foreach (var rowIns in headTable) {
            tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 3, Calculos.CalcularPesoTotal(dataTable, rowIns.Articulo).ToString()));
        }
        
        tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 2, "MERMA (kg)"));
    
        foreach (var rowIns in headTable) {
            tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 3, Calculos.ObtenerValorMerma(merma, rowIns.Articulo).ToString()));
        }
        tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
        
        tableControl.SetMarginBottom(10f);
        document.Add(tableControl);
        
        /************************************************************************************************************
         *  SIGNATURE
         ************************************************************************************************************/
        PDFBuilder.SetSignature(document, "", "COORDINADOR PRODUCCIÓN", "");
        
    }
    public static void printDocumentPedaceria(Document document, List<dynamic> pedaceria)
    {
        
        /************************************************************************************************************
         *  SIGNATURE
         ************************************************************************************************************/
        var columnsComponentesMix = new float[] { 7.14f,7.14f,7.14f,8.14f,16.18f, 7.14f,5.14f,7.14f, 3.14f, 7.14f, 3.14f, 7.14f, 7.14f,7.14f };
        var tableComponentesMix = new Table(UnitValue.CreatePercentArray(columnsComponentesMix)).UseAllAvailableWidth();
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Fecha"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "N\u00b0 Orden"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Producto"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Descripción"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Envasadora"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Peso"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hojuelas enteras   (g)"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "%"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Pedaceria (g)"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "%"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Inspector"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Observacion"));
        tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Usuario"));
        
        foreach (var rowIns in pedaceria)
        {
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.fechaHora.ToString("dd/MM/yyyy")).SetTextAlignment(TextAlignment.LEFT));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Orden)).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.fechaHora.ToString("HH:m")).SetTextAlignment(TextAlignment.LEFT));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Articulo)).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Descripcion)).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Envasadora)).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.peso.ToString())).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.hojuelasEnteras.ToString())).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.porcentajeHojuelasEnteras.ToString())).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.pedaceria.ToString())).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.porcentajePedaceria.ToString())).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.inspector)).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.observacion)).SetTextAlignment(TextAlignment.CENTER));
            tableComponentesMix.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.usuario)).SetTextAlignment(TextAlignment.CENTER));
        }
        tableComponentesMix.SetMarginBottom(10f);
        document.Add(tableComponentesMix);
        
        /************************************************************************************************************
         *  SIGNATURE
         ************************************************************************************************************/
        PDFBuilder.SetSignature(document, "", "Coordinador de Control de Calidad", "");
        
    }
    public static void printDocumentEnvasadoGranelCheckListArranque(Document document, ArranqueControlProcesosE4Response arranqueGranel)
    {
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<dynamic> especificaciones = arranqueGranel.EspecificacionesEnvasadoGranel;
        List<CondicionesOperativasEnvasadoGranel> condicionesOperativas = arranqueGranel.CondicionesOperativas;
        List<CondicionesProcesoEnvasadoGranel> condicionesProceso = arranqueGranel.CondicionesProceso;
        List<ObservacionesEnvasadoGranel> observacionesFirstSheet = arranqueGranel.Observacion;
        ControlProcesoEnvasadoGranel controlProceso = arranqueGranel.ControlProceso;
        List<dynamic> observacionesControlProcesos = arranqueGranel.ObservacionControlProceso;
        List<evaluacionPTControlProceso> evaluacionAtributos = arranqueGranel.EvaluacionAtributos;
        List<dynamic> imgCodificacionCaja = arranqueGranel.ImgCodificacionCaja;
        List<turnosE4CP> TgranelTurnosE4 = arranqueGranel.TurnosE4Cps;
        
        /************************************************************************************************************
         *  Data Principales
         ************************************************************************************************************/
        var columnsHead = new float[] { 50, 50 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        
        // 2 sub tablas
        var cellDatosControl1   = new Cell();
        var cellDatosControl2 = new Cell();
        
        var columnsDatosControl1 = new float[] { 30, 70 };
        var tableDatosControl1 = new Table(UnitValue.CreatePercentArray(columnsDatosControl1)).UseAllAvailableWidth();
        PDFBuilder.SetLabelValue(tableDatosControl1, PDFBuilder.CreateCellFormat(1, 1, "Descripción del producto: "), 
            PDFBuilder.CreateCellFormat(1, 1, arranqueGranel.DescripcionProducto));
        PDFBuilder.SetLabelValue(tableDatosControl1, PDFBuilder.CreateCellFormat(1, 1, "N\u00b0 Orden EN/RE: "), 
            PDFBuilder.CreateCellFormat(1, 3, arranqueGranel.Orden));
        PDFBuilder.SetLabelValue(tableDatosControl1, PDFBuilder.CreateCellFormat(1, 1, "Fecha: "), 
            PDFBuilder.CreateCellFormat(1, 1, arranqueGranel.Fecha.ToString("dd/MM/yyyy")));
        PDFBuilder.SetLabelValue(tableDatosControl1, PDFBuilder.CreateCellFormat(1, 1, "Línea de fritura: "), 
            PDFBuilder.CreateCellFormat(1, 1, arranqueGranel.LineaFritura));
        PDFBuilder.SetLabelValue(tableDatosControl1, PDFBuilder.CreateCellFormat(1, 1, "Turno: "), 
            PDFBuilder.CreateCellFormat(1, 1, arranqueGranel.Turno));
        PDFBuilder.SetLabelValue(tableDatosControl1, PDFBuilder.CreateCellFormat(1, 1, "Selladora: "), 
            PDFBuilder.CreateCellFormat(1, 1, arranqueGranel.Selladora));
        
        var columnsDatosControl2 = new float[] { 16.6f,16.6f,16.6f, 16.6f,16.6f,16.6f };
        var tableDatosControl2 = new Table(UnitValue.CreatePercentArray(columnsDatosControl2)).UseAllAvailableWidth();
        PDFBuilder.SetLabelValue(tableDatosControl2, PDFBuilder.CreateCellFormat(1, 2, "Fecha de envasado: "), 
            PDFBuilder.CreateCellFormat(1, 4, arranqueGranel.FechaEnvasado.ToString("dd/MM/yyyy")));
        PDFBuilder.SetLabelValue(tableDatosControl2, PDFBuilder.CreateCellFormat(1, 2, "Fecha vencimiento / Lote: "), 
            PDFBuilder.CreateCellFormat(1, 4, arranqueGranel.FechaVencimiento.ToString("dd/MM/yyyy")));
        PDFBuilder.SetLabelValue(tableDatosControl2, PDFBuilder.CreateCellFormat(1, 2, "Maquinista: "), 
            PDFBuilder.CreateCellFormat(1, 4, arranqueGranel.Maquinista));
        PDFBuilder.SetLabelValue(tableDatosControl2, PDFBuilder.CreateCellFormat(1, 2, "Punta Estrella: "), 
            PDFBuilder.CreateCellFormat(1, 4, arranqueGranel.UsuarioCreacion));
        
        PDFBuilder.RemoveAllBorder(tableDatosControl2);
        PDFBuilder.SetLabelValue(tableDatosControl2, PDFBuilder.CreateCellFormat(1, 1, "Exportacion: ").SetBorder(Border.NO_BORDER), 
            PDFBuilder.CreateCellFormat(1, 1, arranqueGranel.Tipo.Equals("E")?"X":" "));
        PDFBuilder.SetLabelValue(tableDatosControl2, PDFBuilder.CreateCellFormat(1, 1, "Local: ").SetBorder(Border.NO_BORDER), 
            PDFBuilder.CreateCellFormat(1, 1, arranqueGranel.Tipo.Equals("L")?"X":" "));
        PDFBuilder.SetLabelValue(tableDatosControl2, PDFBuilder.CreateCellFormat(1, 1, "Transito: ").SetBorder(Border.NO_BORDER), 
            PDFBuilder.CreateCellFormat(1, 1, arranqueGranel.Tipo.Equals("T")?"X":" "));
        
        PDFBuilder.RemoveAllBorder(tableDatosControl1);
        cellDatosControl1.Add(tableDatosControl1);
        tableHead.AddCell(cellDatosControl1);
        
        cellDatosControl2.Add(tableDatosControl2);
        tableHead.AddCell(cellDatosControl2);
        document.Add(tableHead);
        
        /************************************************************************************************************
         *  CHECK LIST DE ENVASADO GRANEL
         ************************************************************************************************************/
        document.Add(new Paragraph("CHECK LIST DE ENVASADO GRANEL \n ").SetFontSize(7).SetBold());
        
        var ListEspecificaciones = especificaciones
            .GroupBy(g => new { g.Id, g.EspecificacionId, g.Nombre, g.Valor, g.Otro })
            .Select(p => new EspecificacionGranelDto()
            {
                Id = p.Key.Id,
                EspecificacionId = p.Key.EspecificacionId,
                Nombre = p.Key.Nombre,
                Valor = p.Key.Valor,
                Otro = p.Key.Otro,
                Valores = p.Select(p => new EspecificacionGranelDetalleDto()
                {
                    Id = p.ParametroGeneralId,
                    Nombre = p.NombreParametro
                }).ToList()
            });

        var columnsCheckListEnvasadoGranelHead = new float[] { 20, 80};
        var tableCheckListEnvasadoGranelHead = new Table(UnitValue.CreatePercentArray(columnsCheckListEnvasadoGranelHead)).UseAllAvailableWidth();
        
        foreach (var rowEspecificacion in ListEspecificaciones)
        {
            tableCheckListEnvasadoGranelHead.AddCell(PDFBuilder.CreateCellFormat(1,1,rowEspecificacion.Nombre));

            Cell cellEnvasadoGranel = new Cell(1,1);
            var tableCheckListEnvasadoGranel = new Table(UnitValue.CreatePercentArray(6)).UseAllAvailableWidth();    
            foreach (var rowValores in rowEspecificacion.Valores)
            {
                tableCheckListEnvasadoGranel.AddCell(PDFBuilder.CreateCellFormat(1,1,rowValores.Nombre));
                tableCheckListEnvasadoGranel.AddCell(PDFBuilder.CreateCellFormat(1,1,rowEspecificacion.Valor == rowValores.Id ? "X":" "));
            }

            cellEnvasadoGranel.Add(tableCheckListEnvasadoGranel);
            tableCheckListEnvasadoGranelHead.AddCell(cellEnvasadoGranel.SetBorder(Border.NO_BORDER));
        }

        tableCheckListEnvasadoGranelHead.SetMarginBottom(10);
        document.Add(tableCheckListEnvasadoGranelHead);
        
        /************************************************************************************************************
         *  Condiciones Operativas
         ************************************************************************************************************/
            var tableCondicionesPrevias = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
             tableCondicionesPrevias.SetMargins(0, 0, 0, 0);
             tableCondicionesPrevias.SetPaddings(0, 0, 0, 0);
            
              //Crear la primera celda de la tabla padre
             var celdaCondPrevias1 = new Cell(1, 1);
            
              //Crear una tabla interna para la primera celda
             var columnsCondPrevias1 = new float[] { 80, 10, 10 };
             var tableCondPrevias1 = new Table(UnitValue.CreatePercentArray(columnsCondPrevias1)).UseAllAvailableWidth();
             tableCondPrevias1.SetMargins(0, 0, 0, 0);
             tableCondPrevias1.SetPaddings(0, 0, 0, 0);
            
             tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CONDICIONES OPERATIVAS").SetBold());
             tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "SI").SetBold());
             tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "NO").SetBold());
            
             int mitadObservaciones = (int)Math.Round(condicionesOperativas.Count / 2.0);
             List<CondicionesOperativasEnvasadoGranel> condPrevias1 = condicionesOperativas.Take(mitadObservaciones).ToList();
             List<CondicionesOperativasEnvasadoGranel> condPrevias2 = condicionesOperativas.Skip(mitadObservaciones).ToList();
            
             //Agregar contenido de la tabla con un iterador-foreach-while
              foreach (var rowCondicionesPrevias in condPrevias1)
              {
                  tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Nombre));
                  tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Valor.Equals(true)?"X":" "));
                  tableCondPrevias1.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Valor.Equals(false)?"X":""));
              }
            
             var celdaCondPrevias2 = new Cell(1, 1);
            
              //Crear una tabla interna para la primera celda
             var columnsCondPrevias2 = new float[] { 80, 10, 10 };
             var tableCondPrevias2 = new Table(UnitValue.CreatePercentArray(columnsCondPrevias2)).UseAllAvailableWidth();
             tableCondPrevias2.SetMargins(0, 0, 0, 0);
             tableCondPrevias2.SetPaddings(0, 0, 0, 0);
            
             tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CONDICIONES OPERATIVAS").SetBold());
             tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "SI").SetBold());
             tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "NO").SetBold());
             //Agregar contenido de la tabla con un iterador-foreach-while
              foreach (var rowCondicionesPrevias in condPrevias2)
              {
                  tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Nombre));
                  tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Valor.Equals(true)?"X":" "));
                  tableCondPrevias2.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowCondicionesPrevias.Valor.Equals(false)?"X":""));
                  
              }
            
             celdaCondPrevias1.Add(tableCondPrevias1);
             celdaCondPrevias2.Add(tableCondPrevias2);
             tableCondicionesPrevias.AddCell(celdaCondPrevias1);
             tableCondicionesPrevias.AddCell(celdaCondPrevias2);
             tableCondicionesPrevias.SetMargins(0, 0, 0, 0);
             PDFBuilder.RemoveAllBorder(tableCondicionesPrevias);
             document.Add(tableCondicionesPrevias);

        /************************************************************************************************************
         *  Condiciones del proceso
         ************************************************************************************************************/
        var columnsCondProceso = new float[] { 10, 36, 7,7,7,7, 28 };
        var tableCondProceso = new Table(UnitValue.CreatePercentArray(columnsCondProceso)).UseAllAvailableWidth();
        // tableCondPrevias2.SetMargins(0, 0, 0, 0);
        // tableCondPrevias2.SetPaddings(0, 0, 0, 0);

        tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1,2,"Condiciones del Proceso").SetBold());
        tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1,5," "));
        tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1,2,"Turno").SetBold());

        foreach (var rE4Cp in TgranelTurnosE4)
        {
            tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1,4, rE4Cp.Turno.Replace(","," -")));
        }
        tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(2,1,"Observaciones").SetBold());
        tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1,2,"Hora").SetBold());
        foreach (var rE4Cp in TgranelTurnosE4)
        {
            tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1,4, rE4Cp.Fechas.Replace(","," -")));
        }
        foreach (var rowIns in condicionesProceso)
        {
            tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Padre)));
            tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Nombre)));
            tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Valor1)));
            tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Valor2)));
            tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Valor3)));
            tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Valor4)));
            tableCondProceso.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowIns.Observacion)));
        }

        document.Add(tableCondProceso);
        document.Add(new Paragraph("Leyenda:       C: Conforme           NC: No Conforme          NA: No Aplica").SetFontSize(7).SetBold());
        /************************************************************************************************************
         *  OBSERVACIONES
         ************************************************************************************************************/
        document.Add(new Paragraph("Observaciones:").SetFontSize(7).SetBold());
        StringBuilder observacionesConcatenadas = new StringBuilder();

        foreach (var rowObservaciones in observacionesFirstSheet)
        {
            observacionesConcatenadas.AppendLine(rowObservaciones.valor);
        }
        document.Add(new Paragraph(observacionesConcatenadas.ToString()).SetFontSize(7));

        /************************************************************************************************************
         *  CONTROL DE PROCESO
         ************************************************************************************************************/
        var tableControlProceso = new Table(UnitValue.CreatePercentArray((controlProceso.FechaControlProceso.Count + 1))).UseAllAvailableWidth();
        // tableCondPrevias2.SetMargins(0, 0, 0, 0);
        // tableCondPrevias2.SetPaddings(0, 0, 0, 0);

        tableControlProceso.AddCell(PDFBuilder.CreateCellFormat(1,controlProceso.FechaControlProceso.Count + 1,"CONTROL DE PROCESO \n Frecuencia: Cada hora").SetBold().SetBorder(Border.NO_BORDER));
        tableControlProceso.AddCell(PDFBuilder.CreateCellFormat(1,1,"Hora"));
        foreach (DateTime fecha in controlProceso.FechaControlProceso)
        {
            tableControlProceso.AddCell(PDFBuilder.CreateCellFormat(1, 1, fecha.ToString("MM/dd/yyyy")));
        }

        foreach (var rowIns in controlProceso.DetalleControlProceso)
        {
            tableControlProceso.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Parametro));
            foreach (DateTime fecha in controlProceso.FechaControlProceso)
            {
                if (rowIns is IDictionary<string, object> rowInsDict)
                {
                    // Fecha formateada
                    var propertyName = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                    // Accede a los valores por clave
                    if (rowInsDict.ContainsKey(propertyName))
                    {
                        var valorColumna = rowInsDict[propertyName];
                        // Usa valorColumna como necesites
                        tableControlProceso.AddCell(PDFBuilder.CreateCellFormat(1, 1, valorColumna?.ToString()));
                    }
                }
            }
        }

        tableControlProceso.SetMarginTop(10);
        document.Add(tableControlProceso);
        document.Add(new Paragraph("Leyenda:       C: Conforme           NC: No Conforme      ").SetFontSize(7).SetBold());

        if (arranqueGranel.PersonalPesa != null)
        {
            var tablePersonasPesa = new Table(UnitValue.CreatePercentArray(arranqueGranel.PersonalPesa.Split(',').Length + 1)).UseAllAvailableWidth();
            tablePersonasPesa.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Personal que pesa:"));
            foreach (string persona in arranqueGranel.PersonalPesa.Split(',')) {
                tablePersonasPesa.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(persona)));
            }
            document.Add(tablePersonasPesa);
        }


        if (arranqueGranel.PersonalSella != null)
        {
            var tablePersonasSella = new Table(UnitValue.CreatePercentArray(arranqueGranel.PersonalSella.Split(',').Length + 1)).UseAllAvailableWidth();
            tablePersonasSella.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Personal que sella:"));
            foreach (string persona in arranqueGranel.PersonalSella.Split(',')) {
                tablePersonasSella.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(persona)));
            }
            document.Add(tablePersonasSella);
        }
        
        /************************************************************************************************************
         *  OBSERVACIONES CONTROL PROCESO
         ************************************************************************************************************/
        document.Add(new Paragraph("Observaciones:").SetFontSize(7).SetBold());
        StringBuilder observacionesConcatenadasCtrProceso = new StringBuilder();

        foreach (var rowObservaciones in observacionesControlProcesos)
        {
            observacionesConcatenadasCtrProceso.AppendLine(rowObservaciones.Observacion);
        }
        document.Add(new Paragraph(observacionesConcatenadasCtrProceso.ToString()).SetFontSize(7));
        document.Add(new Paragraph("Material plástico duro/quebradizo e instrumento afilado integro").SetFontSize(7));
        document.Add(new Paragraph("1. Bolos de Producto Terminado   2. Balde   3. Tijera \n").SetFontSize(7));
        
        /************************************************************************************************************
         *  EVALUACION SENSORIAL
         ************************************************************************************************************/
            var columnsRow4ES = new float[] { 22.5f, 4.875f, 4.875f, 4.875f, 4.875f, 4.875f,  6.25f, 34 };
            var tableRow4EvaluacionSensorial = new Table(UnitValue.CreatePercentArray(columnsRow4ES)).UseAllAvailableWidth();
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 8, "EVALUACIÓN DE PRODUCTO TERMINADO").SetBold().SetBorder(Border.NO_BORDER));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, " "));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 6, "Hora:"));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 2, " "));

            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Panelista", TextAlignment.CENTER));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Apariencia General").SetRotationAngle(Math.PI / 2));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateRotatedCell(1, 1, "Color").SetTextAlignment(TextAlignment.RIGHT));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateRotatedCell(1, 1, "Olor"));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateRotatedCell(1, 1, "Sabor"));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateRotatedCell(1, 1, "Textura"));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Calificacion Final").SetRotationAngle(Math.PI / 2));
            tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Observaciones", TextAlignment.CENTER));

            foreach (var sensorial in evaluacionAtributos)
            {
                tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Panelistas, TextAlignment.CENTER));
                tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Apariencia.ToString(), TextAlignment.CENTER));
                tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Color.ToString(), TextAlignment.CENTER));
                tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Olor.ToString(), TextAlignment.CENTER));
                tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.sabor.ToString(), TextAlignment.CENTER));
                tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Textura.ToString(), TextAlignment.CENTER));
                tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.CalificacionFinal.ToString(), TextAlignment.CENTER));   
                tableRow4EvaluacionSensorial.AddCell(PDFBuilder.CreateCellFormat(1, 1, sensorial.Observacion));
            }

            tableRow4EvaluacionSensorial.SetMarginBottom(10f);  //Agregar un margen Bottom a la tabla
            document.Add(tableRow4EvaluacionSensorial);
            
            var tableCodificacionHead = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
            var tableCodificacionCaja = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
            var celdaCodificacionCaja = new Cell(1, 1);
            
            foreach (var rowIns in imgCodificacionCaja)
            {
                 tableCodificacionCaja.AddCell(PDFBuilder.getCellxImage(1,1, rowIns.Ruta));
            }

            celdaCodificacionCaja.Add(tableCodificacionCaja);
            tableCodificacionHead.AddCell(celdaCodificacionCaja);
            tableCodificacionHead.SetHeight(320f);
            
            document.Add(tableCodificacionHead);
        
            /************************************************************************************************************
             *  SIGNATURE
             ************************************************************************************************************/
            PDFBuilder.SetSignature(document, "Firma Maquinista", "Firma Coordinador / Encargado", "");
            PDFBuilder.SetSignature(document, "Firma Punta Estrella", "Firma Facilitador y/o Inspector de Calidad", "");
    }
}