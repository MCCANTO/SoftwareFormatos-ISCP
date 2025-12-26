

using IK.SCP.Application.PDF.Fritura.Model;
using IK.SCP.Application.PDF.Helpers;
using iText.Layout.Borders;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Newtonsoft.Json;
using Border = iText.Layout.Borders.Border;
using Cell = iText.Layout.Element.Cell;
using Table = iText.Layout.Element.Table;

namespace IK.SCP.Application.PDF.Templates;

public class TemplateAcondicionamiento
{
    public static Document GetTemplateRemojoReposo(Document document, ControlReposoRemojoResponse materiaPrima, int verificar)
    {

        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<ControlReposoRemojoDetail> listControlReposoORemojo = materiaPrima.ListaControlReposoRemojo;

        /************************************************************************************************************
         *  General Information  of Control de remojo
         ************************************************************************************************************/
        var columnsControl = new float[] { 10, 50, 40 };
        var tableControl = new Table(UnitValue.CreatePercentArray(columnsControl)).UseAllAvailableWidth();
        tableControl.SetMargins(5f, 0, 5f, 0);

        tableControl.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CONTROL DE: ").SetPaddings(0, 0, 0, 0));

        if (verificar == 1)
        {
            /************************************************************************************************************
             *  Remojo de habas
             ************************************************************************************************************/
            var cellHabas = new Cell(1, 1).SetPaddings(2, 0, 2, 0);
            var columnsHabas = new float[] { 26, 12, 62 };
            var tableHabas = new Table(UnitValue.CreatePercentArray(columnsHabas)).UseAllAvailableWidth();
            tableHabas.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Remojo de Habas").SetBorder(Border.NO_BORDER));
            var cellCaja1 = new Cell(1, 1);
            var columnsCaja1 = new float[] { 1 };
            var tableCaja1 = new Table(UnitValue.CreatePercentArray(columnsCaja1)).UseAllAvailableWidth();
            tableCaja1.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Si").SetHeight(12));
            cellCaja1.Add(tableCaja1);
            tableHabas.AddCell(cellCaja1.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER));

            var cellMaquinista = new Cell(1, 1).SetBorder(Border.NO_BORDER);
            var columnsMaquinista = new float[] { 1 };
            var tableMaquinista = new Table(UnitValue.CreatePercentArray(columnsMaquinista)).UseAllAvailableWidth();
            tableMaquinista.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Maquinista Responsable del Remojo de Habas"));
            tableMaquinista.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(materiaPrima.responsable)));
            cellMaquinista.Add(tableMaquinista);

            tableHabas.AddCell(cellMaquinista);
            cellHabas.Add(tableHabas);
            tableControl.AddCell(cellHabas);
        }
        else if (verificar == 2) 
        {
            /************************************************************************************************************
             *  Remojo de maiz
             ************************************************************************************************************/
            var cellMaiz = new Cell(1, 1).SetPaddings(2, 0, 2, 0);
            var columnsMaiz = new float[] { 14, 6, 80 };
            var tableMaiz = new Table(UnitValue.CreatePercentArray(columnsMaiz)).UseAllAvailableWidth();
            tableMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Remojo de Maiz").SetBorder(Border.NO_BORDER));
            var cellCaja2 = new Cell(1, 1);
            var columnsCaja2 = new float[] { 1 };
            var tableCaja2 = new Table(UnitValue.CreatePercentArray(columnsCaja2)).UseAllAvailableWidth();
            tableCaja2.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Si").SetHeight(12));
            cellCaja2.Add(tableCaja2);
            tableMaiz.AddCell(cellCaja2.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER));

            var cellSancochado = new Cell(1, 1).SetBorder(Border.NO_BORDER).SetPaddings(2, 0, 2, 2);
            var columnsSancochado = new float[] { 37, 38, 25 };
            var tableSancochado = new Table(UnitValue.CreatePercentArray(columnsSancochado)).UseAllAvailableWidth();
            tableSancochado.AddCell(PDFBuilder.CreateCellFormat(1, 1, "N\u00b0 Tanque de remojo del que proviene el maíz"));
            tableSancochado.AddCell(PDFBuilder.CreateCellFormat(1, 1, "N\u00b0 de Batch\nsancochados por tanque"));
            tableSancochado.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Maquinista del Sancochado"));
            tableSancochado.AddCell(PDFBuilder.CreateCellFormat(1, 1, materiaPrima.numeroTanque.ToString()));
            tableSancochado.AddCell(PDFBuilder.CreateCellFormat(1, 1, materiaPrima.numeroBatch.ToString()));
            tableSancochado.AddCell(PDFBuilder.CreateCellFormat(1, 1, materiaPrima.responsable));
            cellSancochado.Add(tableSancochado);

            tableMaiz.AddCell(cellSancochado);
            cellMaiz.Add(tableMaiz);

            tableControl.AddCell(cellMaiz);
        }
        
        PDFBuilder.RemoveAllBorder(tableControl);
        document.Add(tableControl);

        /************************************************************************************************************
         *  Detail of Control de remojo
         ************************************************************************************************************/
        var columnWidths = new float[] { 10, 10, 10, 10, 10, 10, 10, 20 };
        var table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();
        table.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Nro Batch").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Cantidad de Producto x Batch").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 2, "REMOJO DE HABAS / REPOSO DE MAIZ").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 3, "FRITURA").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(2, 1, "OBSERVACION").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Fecha de inicio de remojo de habas / reposo de maiz").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora de inicio de remojo de habas / reposo de maiz").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Fecha de inicio de fritura").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora de inicio de fritura").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Maquinista de Fritura").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        double sumarCantidadBatch = 0;
        foreach (var remojo in listControlReposoORemojo)
        {
            table.AddCell(PDFBuilder.CreateCellFormat(1, 1, remojo.numeroBatch.ToString())
                .SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(PDFBuilder.CreateCellFormat(1, 1, remojo.cantidadBatch.ToString())
                .SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(PDFBuilder.CreateCellFormat(1, 1, remojo.fechaHoraInicioReposo.ToString("dd/MM/yyyy"))
                .SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(PDFBuilder.CreateCellFormat(1, 1, remojo.fechaHoraInicioReposo.ToString("HH:mm:ss"))
                .SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(PDFBuilder.CreateCellFormat(1, 1, remojo.fechaHoraInicioFritura.ToString("dd/MM/yyyy"))
                .SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(PDFBuilder.CreateCellFormat(1, 1, remojo.fechaHoraInicioFritura.ToString("HH:mm:ss"))
                .SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(PDFBuilder.CreateCellFormat(1, 1, remojo.usuario).SetTextAlignment(TextAlignment.CENTER));
            table.AddCell(PDFBuilder.CreateCellFormat(1, 1, remojo.observacion).SetTextAlignment(TextAlignment.CENTER));

            sumarCantidadBatch = sumarCantidadBatch + remojo.cantidadBatch;
        }

        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Total(Kg)").SetBold().SetTextAlignment(TextAlignment.CENTER));
        table.AddCell(PDFBuilder.CreateCellFormat(1, 1, sumarCantidadBatch.ToString()).SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        document.Add(table);
        document.Add(new Paragraph("* Considerar como inicio de reposo para maíz, la fecha y hora de fin de sancochado")
            .SetBold().SetFontSize(7));

        /************************************************************************************************************
         *  Signature (Firmas del Maquinista y del Coordinador)
         ************************************************************************************************************/ 
        PDFBuilder.SetSignature(document, "Maquinista \n (Nombre y Firma)", "Coordinador de produccion \n (Nombre y Firma)", materiaPrima.responsable);

        document.Close();
        return document;
    }

    public static Document getTemplatePeladoRemojoSancochado(Document document,ControlPeladoRemojoSancochadoResponse materiaPrima)
    {

        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<MateriaPrimaConsumo> listaMateriaPrimaConsumos = materiaPrima.listaMateriaPrimaConsumos;
        List<InsumosConsumo> listaInsumoConsumos = materiaPrima.listaInsumoConsumos;
        Pelado peladoHead = materiaPrima.pelado;
        List<PeladoDetail> listaPeladoDetails = peladoHead.listaPelado;
        Remojo remojoHead = materiaPrima.remojo;
        List<Lavado> listaLavado = remojoHead.ListaLavado;
        Sancochado sancochadoHead = materiaPrima.sancochado;
        List<SancochadoDetail> listaSancochado = sancochadoHead.ListaSancochado;

        /************************************************************************************************************
         *  Consumos : Almacenar Materia Prima y Insumos
         ************************************************************************************************************/
        var columnsConsumos = new float[] { 50, 50 };
        var tableConsumos = new Table(UnitValue.CreatePercentArray(columnsConsumos)).UseAllAvailableWidth();

        /************************************************************************************************************
         *  Detail Materia Prima
         ************************************************************************************************************/
        var cellMP = new Cell(1, 1).SetPaddings(2, 2, 2, 0);
        var columnsMP = new float[] { 25,25,25,25 };
        var tableMP = new Table(UnitValue.CreatePercentArray(columnsMP)).UseAllAvailableWidth();
        tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 4, "DATOS DE MATERIA PRIMA").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Materia Prima").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Calidad").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Cantidad (Kg)").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Lote").SetBold().SetTextAlignment(TextAlignment.CENTER));

        double sumCantidad = 0;
        foreach (var rowMP in listaMateriaPrimaConsumos)
        {
            tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 1, materiaPrima.MateriaPrima).SetTextAlignment(TextAlignment.CENTER));
            tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowMP.calidad).SetTextAlignment(TextAlignment.CENTER));
            tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowMP.cantidad.ToString())
                .SetTextAlignment(TextAlignment.CENTER));
            tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowMP.lote).SetTextAlignment(TextAlignment.CENTER));

            sumCantidad = sumCantidad + rowMP.cantidad;
        }

        tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Total(Kg)").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableMP.AddCell(PDFBuilder.CreateCellFormat(1, 1, sumCantidad.ToString()).SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        cellMP.Add(tableMP);
        tableConsumos.AddCell(cellMP);
        /************************************************************************************************************
         *  Detail Insumos
         ************************************************************************************************************/
        var cellIns = new Cell(1, 1).SetPaddings(2, 0, 2, 2);
        var columnIns = new float[] { 15, 40, 15, 15, 15 };
        var tableIns = new Table(UnitValue.CreatePercentArray(columnIns)).UseAllAvailableWidth();
        tableIns.AddCell(PDFBuilder.CreateCellFormat(1, 1, "INSUMO").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableIns.AddCell(PDFBuilder.CreateCellFormat(1, 1, "LOTE").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableIns.AddCell(PDFBuilder.CreateCellFormat(1, 1, "INICIO (KG)").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableIns.AddCell(PDFBuilder.CreateCellFormat(1, 1, "FINAL (KG)").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableIns.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CONSUMO (KG)").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        foreach (var rowIns in listaInsumoConsumos)
        {
            tableIns.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.insumo).SetTextAlignment(TextAlignment.CENTER));
            tableIns.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.lote.ToString())
                .SetTextAlignment(TextAlignment.CENTER));
            tableIns.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.cantidadInicio.ToString())
                .SetTextAlignment(TextAlignment.CENTER));
            tableIns.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.cantidadFinal.ToString())
                .SetTextAlignment(TextAlignment.CENTER));
            tableIns.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.consumo.ToString())
                .SetTextAlignment(TextAlignment.CENTER));
        }

        cellIns.Add(tableIns);
        tableConsumos.AddCell(cellIns);
        PDFBuilder.RemoveAllBorder(tableConsumos);
        tableConsumos.SetMarginTop(10f);
        document.Add(tableConsumos);
        
        /************************************************************************************************************
         *  Detail Control de Pelado, remojo y sancochado de maiz
         ************************************************************************************************************/
        var columnsPRSMaiz = new float[] { 11, 11, 11, 9, 9, 9, 9, 30 };
        var tablePRSMaiz = new Table(UnitValue.CreatePercentArray(columnsPRSMaiz)).UseAllAvailableWidth();
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 8, "PELADO (NIXTAMALIZACION)").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Tiempo del Prceso de Pelado").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(
            PDFBuilder.CreateCellFormat(1, 4, "Fecha").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Inicio de Pelado").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 4, peladoHead.FechaHoraInicio.ToString("dd/MM/yyyy"))
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, peladoHead.FechaHoraInicio.ToString("hh:mm:ss"))
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Fin de Pelado").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 4, peladoHead.FechaHoraFin.ToString("dd/MM/yyyy"))
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, peladoHead.FechaHoraFin.ToString("hh:mm:ss"))
            .SetTextAlignment(TextAlignment.CENTER));

        /***** CABECERA *****/
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(2, 1, "N\u00b0 BATCH").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 2, "Inicio").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 2, "Fin").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(2, 1, "CAL (Kg)").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Va al Tankque N\u00b0").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(2, 1, "OBSERVACIONES").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, "T \u00b0C").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, "T \u00b0C").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        foreach (var rowPD in listaPeladoDetails)
        {
            tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowPD.numeroBatch)
                .SetTextAlignment(TextAlignment.CENTER));
            tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullDatetime(rowPD.fechaHoraInicio,"hh:mm"))
                .SetTextAlignment(TextAlignment.CENTER));
            tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowPD.temperaturaInicio))
                .SetTextAlignment(TextAlignment.CENTER));
            tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullDatetime(rowPD.fechaHoraFin, "hh:mm"))
                .SetTextAlignment(TextAlignment.CENTER));
            tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowPD.temperaturaFin))
                .SetTextAlignment(TextAlignment.CENTER));
            tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowPD.cal)).SetTextAlignment(TextAlignment.CENTER));
            tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowPD.numeroTanque))
                .SetTextAlignment(TextAlignment.CENTER));
            tablePRSMaiz.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.StripHtmlTags(PDFBuilder.IsNullString(rowPD.observacion)))
                .SetTextAlignment(TextAlignment.CENTER));
        }

        tablePRSMaiz.SetMarginTop(10f);
        document.Add(tablePRSMaiz);
        
        // Signature Segunda Pagina
        PDFBuilder.SetSignature(document, "Maquinista \n (Nombre y Firma)", "Coordinador de Produccion / \n Encargado de Turno \n (Nombre y Firma)", materiaPrima.Usuario);
        PDFBuilder.saltoLinea(document);
        
        /************************************************************************************************************
         *  Detail Detail Remojo
         ************************************************************************************************************/
        var columnRj = new float[] { 10,10,10,10,10, 10,10,10,20 };
        var tableRj = new Table(UnitValue.CreatePercentArray(columnRj)).UseAllAvailableWidth();
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Tiempo del Proceso de Remojo").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 2, "N\u00b0 Tanques de Remojo").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Fecha)").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora)").SetBold().SetTextAlignment(TextAlignment.CENTER));
        
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Inicio de Remojo").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 2, remojoHead.numeroTanque).SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 3, PDFBuilder.IsNullDatetime(remojoHead.fechaInicio, "dd/MM/yyyy")).SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullDatetime(remojoHead.fechaInicio, "hh:mm")).SetTextAlignment(TextAlignment.CENTER));
        
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Fin de Remojo").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 2, remojoHead.numeroTanque).SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 3, PDFBuilder.IsNullDatetime(remojoHead.fechaFin, "dd/MM/yyyy")).SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullDatetime(remojoHead.fechaFin, "hh:mm")).SetTextAlignment(TextAlignment.CENTER));
        
        tableRj.AddCell(
            PDFBuilder.CreateCellFormat(1, 9, "LAVADO (CAMBIO DE AGUA Y AGITACIÓN DEL PRODUCTO REMOJADO )")
                .SetTextAlignment(TextAlignment.CENTER));
        
        /*** CABECERA REMOJO ***/
        tableRj.AddCell(PDFBuilder.CreateCellFormat(3, 1, "N\u00b0 Tanque en remojo").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Olor (C/NC)").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 2, "Cambio de Agua").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 4, "Agitación del producto remojado").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(3, 1, "OBSERVACIONES").SetTextAlignment(TextAlignment.CENTER));
        
        tableRj.AddCell(PDFBuilder.CreateCellFormat(2, 1, "pH de agua (antes)").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(2, 1, "pH de agua (después)").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 2, "Inicio de Agitación").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 2, "Fin de Agitación").SetTextAlignment(TextAlignment.CENTER));
        
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Fecha").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Fecha").SetTextAlignment(TextAlignment.CENTER));
        tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora").SetTextAlignment(TextAlignment.CENTER));
        
        foreach (var rowLv in listaLavado)
        {
            tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowLv.numeroTanque).SetTextAlignment(TextAlignment.CENTER));
            tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowLv.olorDesc)).SetTextAlignment(TextAlignment.CENTER));
            tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowLv.phAntes).SetTextAlignment(TextAlignment.CENTER));
            tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowLv.phDespues).SetTextAlignment(TextAlignment.CENTER));
            tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowLv.inicioAgitacion.ToString("dd/MM/yyyy")).SetTextAlignment(TextAlignment.CENTER));
            tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowLv.inicioAgitacion.ToString("hh:mm")).SetTextAlignment(TextAlignment.CENTER));
            tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowLv.finAgitacion.ToString("dd/MM/yyyy")).SetTextAlignment(TextAlignment.CENTER));
            tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowLv.finAgitacion.ToString("hh:mm")).SetTextAlignment(TextAlignment.CENTER));
            tableRj.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.StripHtmlTags(PDFBuilder.IsNullString(rowLv.observacion))).SetTextAlignment(TextAlignment.CENTER));
        }
        
        tableRj.SetMarginTop(10f);
        document.Add(tableRj);
        
        /************************************************************************************************************
         *  Detail Detail Sancochado
         ************************************************************************************************************/
        var columnsSanc = new float[] { 10, 10, 11, 10, 10, 10, 40 };
        var tableSanc = new Table(UnitValue.CreatePercentArray(columnsSanc)).UseAllAvailableWidth();
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 7, "SANCOCHADO").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Tiempo del Proceso de Sancochado").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Fecha").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Inicio de Sancochado").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 3, sancochadoHead.fechaInicio.ToString("dd/MM/yyyy"))
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, sancochadoHead.fechaInicio.ToString("hh:mm:ss"))
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 3, "Fin de Sancochado").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 3, sancochadoHead.fechaFinal.ToString("dd/MM/yyyy"))
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, sancochadoHead.fechaFinal.ToString("hh:mm:ss"))
            .SetTextAlignment(TextAlignment.CENTER));

        /***** CABECERA *****/
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Viene del Tanque").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(2, 1, "N\u00b0 BATCH").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 2, "Inicio").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 2, "Fin").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(2, 1, "OBSERVACIONES").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, "T \u00b0C").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Hora").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));
        tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, "T \u00b0C").SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        foreach (var rowSanc in listaSancochado)
        {
            tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowSanc.numeroTanque)
                .SetTextAlignment(TextAlignment.CENTER));
            tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowSanc.numeroBatch)
                .SetTextAlignment(TextAlignment.CENTER));
            tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullDatetime(rowSanc.fechaHoraInicio,"hh:mm"))
                .SetTextAlignment(TextAlignment.CENTER));
            tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowSanc.temperaturaInicio))
                .SetTextAlignment(TextAlignment.CENTER));
            tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullDatetime(rowSanc.fechaHoraFin, "hh:mm"))
                .SetTextAlignment(TextAlignment.CENTER));
            tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowSanc.temperaturaFin))
                .SetTextAlignment(TextAlignment.CENTER));
            tableSanc.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNullString(rowSanc.observacion))
                .SetTextAlignment(TextAlignment.CENTER));
        }

        tableSanc.SetMarginTop(10f);
        document.Add(tableSanc);
        
        // Signature Segunda Pagina
        PDFBuilder.SetSignature(document, "Maquinista \n (Nombre y Firma)", "Coordinador de Produccion / \n Encargado de Turno \n (Nombre y Firma)", materiaPrima.Usuario);
        
        return document;
    }

    public static Document GetTemplateTratamientoPEF(Document document, ControlTratamientoPEFResponse materiaPrima)
    {
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<CondicionPrevia> condicionPrevias = materiaPrima.ListaCondicionesPrevias;
        List<FuerzaCortes> fuerzaCortes = materiaPrima.ListaFuerzaCortes;
        List<Tiempos> tiempoPefeado = materiaPrima.ListaTiempos;
        
        /************************************************************************************************************
         *  Encabezado de la materia prima
         ************************************************************************************************************/
        var columnsHead = new float[] { 25, 25, 25, 25 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        PDFBuilder.SetLabelValue(tableHead,
            PDFBuilder.CreateCellFormat(1, 1, "FECHA").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.FechaEjecucion == null ? "10-04-1992" : materiaPrima.FechaEjecucion.ToString()));
        PDFBuilder.SetLabelValue(tableHead,
            PDFBuilder.CreateCellFormat(1, 1, "PROVEEDOR").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.proveedor));
        PDFBuilder.SetLabelValue(tableHead,
            PDFBuilder.CreateCellFormat(1, 1, "MATERIA PRIMA").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Nombre == null ? "MP-TEST": materiaPrima.Nombre));
        PDFBuilder.SetLabelValue(tableHead,
            PDFBuilder.CreateCellFormat(1, 1, "LOTE").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.lote));
        PDFBuilder.SetLabelValue(tableHead,
            PDFBuilder.CreateCellFormat(1, 1, "%HUMEDAD PEF").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.humedad.ToString()));
        PDFBuilder.SetLabelValue(tableHead,
            PDFBuilder.CreateCellFormat(1, 1, "\u00b0BRIX").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.brix.ToString()));
        
        tableHead.SetMarginTop(10f);
        document.Add(tableHead);
        
        /************************************************************************************************************
         *  Condiciones Previas
         ************************************************************************************************************/
        var columnConP = new float[] { 52, 8,8,8,8,8,8 };
        var tableConP = new Table(UnitValue.CreatePercentArray(columnConP)).UseAllAvailableWidth();
        
        foreach (var rowIns in condicionPrevias)
        {
            tableConP.AddCell(PDFBuilder.CreateCellFormat(1, 1, rowIns.Nombre).SetTextAlignment(TextAlignment.CENTER));
            tableConP.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Valor_1))
                .SetTextAlignment(TextAlignment.CENTER));
            tableConP.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Obs_1))
                .SetTextAlignment(TextAlignment.CENTER));
            tableConP.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Valor_2))
                .SetTextAlignment(TextAlignment.CENTER));
            tableConP.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Obs_2))
                .SetTextAlignment(TextAlignment.CENTER));
            tableConP.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Valor_3))
                .SetTextAlignment(TextAlignment.CENTER));
            tableConP.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Obs_3))
                .SetTextAlignment(TextAlignment.CENTER));
        }
        
        tableConP.SetMarginTop(10f);
        document.Add(tableConP);
        
        /************************************************************************************************************
         *  Fuerzas de corte
         ************************************************************************************************************/
        var columnFzC = new float[] { 14, 14,14,14,14,14,14 };
        var tableFzC = new Table(UnitValue.CreatePercentArray(columnFzC)).UseAllAvailableWidth();
        
        tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 7, "FUERZA DE CORTE (N)").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, "ORDEN").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CONTROL").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, "PEF1").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CONTROL").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, "PEF2").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CONTROL").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, "PEF3").SetBold().SetTextAlignment(TextAlignment.CENTER));
        
        foreach (var rowIns in fuerzaCortes)
        {
            tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Secuencial)).SetTextAlignment(TextAlignment.CENTER));
            tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.ControlSinPef_1)).SetTextAlignment(TextAlignment.CENTER));
            tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Pef_1)).SetTextAlignment(TextAlignment.CENTER));
            tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.ControlSinPef_2)).SetTextAlignment(TextAlignment.CENTER));
            tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Pef_2)).SetTextAlignment(TextAlignment.CENTER));
            tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.ControlSinPef_3)).SetTextAlignment(TextAlignment.CENTER));
            tableFzC.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Pef_3)).SetTextAlignment(TextAlignment.CENTER));
        }
        
        tableFzC.SetMarginTop(10f);
        document.Add(tableFzC);
            
        /************************************************************************************************************
         *  Control de tiempode pefeado por lote
         ************************************************************************************************************/
        var columnTPef = new float[] { 20, 20, 20, 20, 20 };
        var tableTPef = new Table(UnitValue.CreatePercentArray(columnTPef)).UseAllAvailableWidth();

        tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 5, "CONTROL DE TIEMPO DE PEFEADO POR LOTE DE PRODUCTO").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 1, "NUMERO DE PALETA").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 1, "CANTIDAD (KG)").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 1, "HORA INICIO PEF*").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 1, "HORA INICIO FRITADO").SetBold().SetTextAlignment(TextAlignment.CENTER));
        tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 1, "OBSERVACIONES").SetBold().SetTextAlignment(TextAlignment.CENTER));
        
        foreach (var rowIns in tiempoPefeado)
        {
            tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.numeroPaleta)).SetTextAlignment(TextAlignment.CENTER));
            tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.cantidadKg)).SetTextAlignment(TextAlignment.CENTER));
            tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.horaInicioPef)).SetTextAlignment(TextAlignment.CENTER));
            tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.horaInicioFritura)).SetTextAlignment(TextAlignment.CENTER));
            tableTPef.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.observacion)).SetTextAlignment(TextAlignment.CENTER));
        }
        
        tableTPef.SetMarginTop(10f);
        document.Add(tableTPef);
        document.Add(new Paragraph("* Se debe considerar desde que sale los 1eros plátanos pefeados. \n" +
                                   "Tener en cuenta: Por ningun motivo la materia prima que pase por el PEF puede esperar más de 1.0 hora para su uso")
            .SetBold().SetFontSize(6));
        
        /************************************************************************************************************
         *  Firma del documento
         ************************************************************************************************************/
        PDFBuilder.SetSignature(document, "Nombre y Firma del Coordinador de Turno", "Nombre y firma del Responsable");
        
        return document;
    }

    public static Document getTemplateCheckArranqueElectroporador(Document document, ChecklistArranqueElectroporadorResponse materiaPrima)
    {
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<CondicionesBasicas> condicionBasicas = materiaPrima.listaCondicionesBasicas;
        List<VerificacionEquipoElectroporador> verificacionEquipo = materiaPrima.listaVerificacionEquipo;
        List<VariablesBasicas> variablesBasicas = materiaPrima.listaVariablesBasicas;
        
        /************************************************************************************************************
         *  General Information  of Production
         ************************************************************************************************************/
        var columnsHead = new float[] { 20, 30, 15, 35 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "MATERIA PRIMA: "), 
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.MateriaPrima));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, " "), 
            PDFBuilder.CreateCellFormat(1, 1,  ""));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "RESPONSABLE: "), 
            PDFBuilder.CreateCellFormat(1, 1,  materiaPrima.Responsable));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "COORDINADOR/ENCARGADO: "), 
            PDFBuilder.CreateCellFormat(1, 1,  materiaPrima.Encargado));
        PDFBuilder.RemoveInsideBorder(tableHead);
        tableHead.SetMarginBottom(10f);
        document.Add(tableHead);
       
        /************************************************************************************************************
         *  Data de control
         ************************************************************************************************************/
        var columnsDataControl = new float[] { 6, 23, 6, 23, 9, 23 };
        var tableDataControl = new Table(UnitValue.CreatePercentArray(columnsDataControl)).UseAllAvailableWidth();

        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "FECHA").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Fecha.ToString("yyyy-M-d")));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "TURNO").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Turno));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "HORA INICIO").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Fecha.ToString("HH:mm:ss")));
        tableDataControl.SetMarginBottom(10f);
        document.Add(tableDataControl);
        
        /************************************************************************************************************
         *  Condiciones Previas
         ************************************************************************************************************/
        var columnWidths = new float[] { 55, 10, 10, 35 };
        var header = new string[] { "I. Condiciones previas al arranque", "Si", "No", "Observaciones" };
        
        string[][] dataCondicionesArranque = condicionBasicas
            .Select(condicion => new string[] { condicion.Orden + ".-" + condicion.Descripcion, 
                condicion.Valor.Equals("True") ? "X": " ",
                condicion.Valor.Equals("False")? "X": " ",
                condicion.Observacion==null?"":condicion.Observacion
            })
            .ToArray();

        // Generar la tabla en el documento PDF
        PDFBuilder.GenerateTable(document, columnWidths, header, dataCondicionesArranque);
        
        /************************************************************************************************************
         *  VERIFICACION DE EQUIPO
         ************************************************************************************************************/
        var columnsVerif = new float[] { 35, 15, 15, 35 };
        var tableVerif = new Table(UnitValue.CreatePercentArray(columnsVerif)).UseAllAvailableWidth();
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(2, 1, "II. Verificación de Equipo previa al arranque").SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 2, " Si: Check(✓), No: (X), No Aplica: (N.A.)").SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(2, 1, "Observaciones").SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Operativo").SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Limpio").SetBold());
        
        foreach (var rowIns in verificacionEquipo)
        {
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Descripcion)).SetTextAlignment(TextAlignment.LEFT));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Operativo)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Limpio)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Observacion)).SetTextAlignment(TextAlignment.CENTER));
        }
        tableVerif.SetMarginBottom(10f);
        document.Add(tableVerif);
        
        /************************************************************************************************************
         *  VARIABLES BASICAS PARA EL ARRANQUE
         ************************************************************************************************************/
        var columnsVB = new float[] { 30, 20, 50 };
        var tableVB = new Table(UnitValue.CreatePercentArray(columnsVB)).UseAllAvailableWidth();
        tableVB.AddCell(PDFBuilder.CreateCellFormat(1, 2, "III. Variables basicas para el arranque").SetBold());
        tableVB.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Observaciones").SetBold());
        
        foreach (var rowIns in variablesBasicas)
        {
            tableVB.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Descripcion)).SetTextAlignment(TextAlignment.LEFT));
            tableVB.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Valor)).SetTextAlignment(TextAlignment.CENTER));
            tableVB.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Observacion)).SetTextAlignment(TextAlignment.CENTER));
        }
        tableVB.SetMarginBottom(10f);
        document.Add(tableVB);
        document.Add(new Paragraph("* Materiales duro/quebradizo e instrumento afilado").SetBold().SetFontSize(6));
        
        /************************************************************************************************************
         *  SIGNATURE
         ************************************************************************************************************/
        PDFBuilder.SetSignature(document, "Responsable (Nombre y Firma)", "", materiaPrima.Responsable);
        
        return document;
    }

    public static Document getTemplateCheckArranqueLavadoTuberculos(Document document, ChecklistArranqueLavadoTuberculosResponse materiaPrima)
    {
        
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<CondicionesPrevias> condicionBasicas = materiaPrima.listaCondicionesPrevias;
        List<AcondicionamientoArranqueVerificacionEquipo> verificacionEquipo = materiaPrima.listaVerificacionEquipo;
        
        /************************************************************************************************************
         *  General Information  of Production
         ************************************************************************************************************/
        var columnsHead = new float[] { 12, 38, 15, 35 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "PRODUCTO: "), 
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.MateriaPrima));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, " "), 
            PDFBuilder.CreateCellFormat(1, 1,  ""));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "MAQUINISTA: "), 
            PDFBuilder.CreateCellFormat(1, 1,  materiaPrima.Responsable));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "COORDINADOR/ENCARGADO: "), 
            PDFBuilder.CreateCellFormat(1, 1,  materiaPrima.Encargado));
        PDFBuilder.RemoveInsideBorder(tableHead);
        tableHead.SetMarginBottom(10f);
        document.Add(tableHead);

        /************************************************************************************************************
         *  Data de control
         ************************************************************************************************************/
        var columnsDataControl = new float[] { 6, 23, 6, 23, 9, 23 };
        var tableDataControl = new Table(UnitValue.CreatePercentArray(columnsDataControl)).UseAllAvailableWidth();

        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "FECHA").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Fecha.ToString("yyyy-M-d")));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "TURNO").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Turno));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "HORA INICIO").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Fecha.ToString("HH:mm:ss")));
        tableDataControl.SetMarginBottom(10f);
        document.Add(tableDataControl);

        /************************************************************************************************************
         *  Condiciones Previas de Arranque
         ************************************************************************************************************/
        var columnWidths = new float[] { 55, 10, 10, 35 };
        var header = new string[] { "I. Condiciones previas al arranque", "Si", "No", "Observaciones" };
        
        string[][] dataCondicionesArranque = condicionBasicas
            .Select(condicion => new string[] { condicion.Orden + ".-" + condicion.Descripcion, 
                condicion.Valor.Equals("True") ? "X": " ",
                condicion.Valor.Equals("False")? "X": " ",
                condicion.Observacion==null?"":condicion.Observacion
            })
            .ToArray();

        // Generar la tabla en el documento PDF
        PDFBuilder.GenerateTable(document, columnWidths, header, dataCondicionesArranque);

        /************************************************************************************************************
         *  Verificacion del equipo previa  al arranque
         ************************************************************************************************************/
        var columnsTable = new float[] { 4, 30, 25, 5, 5, 31 };
        var listVerificaciones = verificacionEquipo
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
        document.Add(new Paragraph("* Materiales duro/quebradizo e instrumento afilado").SetFontSize(7));
        
        /************************************************************************************************************
         *  SIGNATURE
         ************************************************************************************************************/
        PDFBuilder.SetSignature(document, "Responsable (Nombre y Firma)", "", materiaPrima.Responsable);
        
        return document;
    }

    public static Document getTemplateContrlRayosX(Document document, ControlRayosXAcondResponse materiaPrima)
    {
        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<ControlMonitoreo> verificacionEquipo = materiaPrima.listaControlMonitoreo;
        
        /************************************************************************************************************
         *  General Information  of Production - REVSISAR NO ME GUSTA
         ************************************************************************************************************/
        var columnsHead = new float[] { 5, 10, 62, 5, 10 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        Cell mesLabelCell = PDFBuilder.CreateCellFormat(1, 1, "Mes: ").SetBorder(Border.NO_BORDER);
        Cell mesValueCell = PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.GetMonthName(materiaPrima.Mes)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
        
        Cell spaciado = PDFBuilder.CreateCellFormat(1, 1, "").SetBorder(Border.NO_BORDER);
        
        Cell añoLabelCell = PDFBuilder.CreateCellFormat(1, 1, "Año: ").SetBorder(Border.NO_BORDER);
        Cell añoValueCell = PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Año).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
            
        PDFBuilder.SetLabelValue(tableHead, mesLabelCell, mesValueCell);
        tableHead.AddCell(spaciado);
        PDFBuilder.SetLabelValue(tableHead, añoLabelCell, añoValueCell);
        
        tableHead.SetMarginBottom(10f);
        tableHead.SetMarginTop(10f);
        document.Add(tableHead);
        
        /************************************************************************************************************
         *  VERIFICACION DE EQUIPO
         ************************************************************************************************************/
        var columnsVerif = new float[] { 6,6,24,6,6,6,40,6 };
        var tableVerif = new Table(UnitValue.CreatePercentArray(columnsVerif)).UseAllAvailableWidth();
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 8, "Frecuencia: Al inicio de cada turno",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Fecha",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Turno/Hora",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Materia Prima",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 2, "¿Detecta y rechaza? Si(ü) No(x)",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Conforme/No conforme",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Observaciones",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Ejecutado por (iniciales)",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "1\u00b0",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "2\u00b0",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "0.80 mm 316SS\nP156268699A-276",TextAlignment.CENTER).SetBold());
        tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, "1.0 mm 316SS\nP156277619E-36",TextAlignment.CENTER).SetBold());
        
        foreach (var rowIns in verificacionEquipo)
        {
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.FechaHora.ToString("dd/MM/yyyy"))).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.FechaHora.ToString("HH:mm:ss"))).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.MateriaPrima)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.DeteccionUno)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.DeteccionDos)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Conformidad)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.Observacion)).SetTextAlignment(TextAlignment.CENTER));
            tableVerif.AddCell(PDFBuilder.CreateCellFormat(1, 1, PDFBuilder.IsNull(rowIns.UsuarioEjecucion)).SetTextAlignment(TextAlignment.CENTER));
            
        }
        tableVerif.SetMarginBottom(10f);
        document.Add(tableVerif);
        
        /************************************************************************************************************
         *  FINAL DEL REPORTE RAYOS X
         ************************************************************************************************************/
        var columnsAccionEjecutar = new float[] { 7, 35, 58 };
        var tableAccionEjecutar = new Table(UnitValue.CreatePercentArray(columnsAccionEjecutar)).UseAllAvailableWidth();
        tableAccionEjecutar.AddCell(PDFBuilder.CreateCellFormat(1, 1, "N\u00b0"));
        tableAccionEjecutar.AddCell(PDFBuilder.CreateCellFormat(1, 1, "OCURRENCIAS"));
        tableAccionEjecutar.AddCell(PDFBuilder.CreateCellFormat(1, 1, "ACCIONES A EJECUTAR"));
        
        tableAccionEjecutar.AddCell(PDFBuilder.CreateCellFormat(3, 1, "1"));
        tableAccionEjecutar.AddCell(PDFBuilder.CreateCellFormat(3, 1, "No detección de patrón"));
        tableAccionEjecutar.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Parar el proceso y comunicar a técnico de mantenimiento para reparación de máquina, asimismo comunicar a Facilitador de Calidad y Coordinador de Producción."));
        
        tableAccionEjecutar.AddCell(PDFBuilder.CreateCellFormat(3, 1, "2"));
        tableAccionEjecutar.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Detección de piedra"));
        tableAccionEjecutar.AddCell(PDFBuilder.CreateCellFormat(3, 1, "Descarte de la materia prima con piedra que se encuentra en bandeja de rechazo."));
        document.Add(tableAccionEjecutar);
            
        /************************************************************************************************************
         *  SIGNATURE
         ************************************************************************************************************/
        PDFBuilder.SetSignature(document, "Jefe de Control de Calidad", "Coordinador/Encargado de Producción");
        
        return document;
    }

    public static Document getTemplateCheckArranqueMaiz(Document document, ChecklistArranqueMaizResponse materiaPrima)
    {

        /************************************************************************************************************
         *  Data entrante
         ************************************************************************************************************/
        List<CondicionesPreviasMaiz> condicionBasicas = materiaPrima.listaCondicionesPrevias;
        List<AcondicionamientoArranqueVerificacionEquipoMaiz> verificacionEquipo = materiaPrima.listaVerificacionEquipo;
        
        /************************************************************************************************************
         *  General Information  of Production
         ************************************************************************************************************/
        var columnsHead = new float[] { 20, 30, 15, 35 };
        var tableHead = new Table(UnitValue.CreatePercentArray(columnsHead)).UseAllAvailableWidth();
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "MATERIA PRIMA: "), 
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.MateriaPrima));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, " "), 
            PDFBuilder.CreateCellFormat(1, 1,  ""));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "RESPONSABLE: "), 
            PDFBuilder.CreateCellFormat(1, 1,  materiaPrima.Responsable));
        PDFBuilder.SetLabelValue(tableHead, PDFBuilder.CreateCellFormat(1, 1, "COORDINADOR/ENCARGADO: "), 
            PDFBuilder.CreateCellFormat(1, 1,  materiaPrima.Encargado));
        PDFBuilder.RemoveInsideBorder(tableHead);
        tableHead.SetMarginBottom(10f);
        document.Add(tableHead);

        /************************************************************************************************************
         *  Data de control
         ************************************************************************************************************/
        var columnsDataControl = new float[] { 6, 23, 6, 23, 9, 23 };
        var tableDataControl = new Table(UnitValue.CreatePercentArray(columnsDataControl)).UseAllAvailableWidth();

        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "FECHA").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Fecha.ToString("yyyy-M-d")));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "TURNO").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Turno));
        PDFBuilder.SetLabelValue(tableDataControl, PDFBuilder.CreateCellFormat(1, 1, "HORA INICIO").SetBorder(Border.NO_BORDER),
            PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Fecha.ToString("HH:mm:ss")));
        tableDataControl.SetMarginBottom(10f);
        document.Add(tableDataControl);

        /************************************************************************************************************
         *  Condiciones Previas de Arranque
         ************************************************************************************************************/
        var columnWidths = new float[] { 55, 10, 10, 35 };
        var header = new string[] { "I. Condiciones previas al arranque", "Si", "No", "Observaciones" };
        
        string[][] dataCondicionesArranque = condicionBasicas
            .Select(condicion => new string[] { condicion.Orden + ".-" + condicion.Descripcion, 
                condicion.Valor.Equals("True") ? "X": " ",
                condicion.Valor.Equals("False")? "X": " ",
                condicion.Observacion==null?"":condicion.Observacion
            })
            .ToArray();

        // Generar la tabla en el documento PDF
        PDFBuilder.GenerateTable(document, columnWidths, header, dataCondicionesArranque);

        /************************************************************************************************************
         *  Verificacion del equipo previa  al arranque
         ************************************************************************************************************/
        var columnsTable = new float[] { 4, 30, 25, 5, 5, 31 };
        var listVerificaciones = verificacionEquipo
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
        document.Add(new Paragraph("* Materiales duro/quebradizo e instrumento afilado").SetFontSize(7));
        
        /************************************************************************************************************
         *  General Information  of Production
         ************************************************************************************************************/
        var columnsVB = new float[] { 25, 25, 50 };
        var tableVB = new Table(UnitValue.CreatePercentArray(columnsVB)).UseAllAvailableWidth();
        tableVB.AddCell(PDFBuilder.CreateCellFormat(1, 2, "III. Variables basicas para el arranque"));
        tableVB.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Observaciones"));
        tableVB.AddCell(PDFBuilder.CreateCellFormat(1, 1, "Temperatura de agua en marmita (\u00b0C)"));
        tableVB.AddCell(PDFBuilder.CreateCellFormat(1, 1, materiaPrima.Temperatura));
        tableVB.AddCell(PDFBuilder.CreateCellFormat(1, 1, materiaPrima.ObservacionTemperatura));
        tableVB.SetMarginBottom(10f);
        document.Add(tableVB);

        /************************************************************************************************************
         *  SIGNATURE
         ************************************************************************************************************/
        PDFBuilder.SetSignature(document, "Responsable (Nombre y Firma)", "", materiaPrima.Responsable);
        
        return document;
    }
}