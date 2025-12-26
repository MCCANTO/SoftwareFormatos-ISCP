using System.Data;
using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.PDF.Fritura.Model;
using IK.SCP.Application.PDF.Helpers;
using IK.SCP.Application.PDF.Templates;
using IK.SCP.Infrastructure;
using MediatR;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Events;

namespace IK.SCP.Application.PDF.Fritura.Dao;

public class ControlPeladoRemojoSancochadoMaiz : IRequest<StatusResponse>
{
    public string ordenId { get; set; }
}

public class ControlPeladoRemojoSancochadoMaizHandler : IRequestHandler<ControlPeladoRemojoSancochadoMaiz, StatusResponse>
{
    private readonly IUnitOfWork _uow;

    public ControlPeladoRemojoSancochadoMaizHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ControlPeladoRemojoSancochadoMaiz request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_OrdenId = request.ordenId };
            var nameProcedure = "PDF.ACO_LISTAR_PELADO_REMOJO_SANCOCHADO_MAIZ";

            
            var results = await cnn.QueryMultipleAsync(nameProcedure, parameters,
                commandType: CommandType.StoredProcedure);
            
            var AcondicionamientoMateriaPrima = await results.ReadFirstOrDefaultAsync<ControlPeladoRemojoSancochadoResponse>();

            if (AcondicionamientoMateriaPrima != null)
            {
                var materiaPrimaConsumo = await results.ReadAsync<MateriaPrimaConsumo>();
                AcondicionamientoMateriaPrima.listaMateriaPrimaConsumos = materiaPrimaConsumo.ToList();
                
                var insumoConsumo = await results.ReadAsync<InsumosConsumo>();
                AcondicionamientoMateriaPrima.listaInsumoConsumos = insumoConsumo.ToList();
                
                var observacionConsumo = await results.ReadAsync<ObservacionConsumo>();
                AcondicionamientoMateriaPrima.listaObservacionConsumos = observacionConsumo.ToList();
                
                /*Maiz Pelado*/
                var maizPeladoSql = await results.ReadAsync<Pelado>();
                var maizPelado = maizPeladoSql.FirstOrDefault(); // Obtiene el primer (y único) elemento de la colección

                if (maizPelado != null)
                {
                    var detallePeladoMaiz = await results.ReadAsync<PeladoDetail>();
                    maizPelado.listaPelado = detallePeladoMaiz.ToList();
                    AcondicionamientoMateriaPrima.pelado = maizPelado;
                }
                
                /*Maiz Remojado*/
                var maizRemojoSql = await results.ReadAsync<Remojo>();
                var maizRemojo = maizRemojoSql.FirstOrDefault(); // Obtiene el primer (y único) elemento de la colección

                if (maizRemojo != null)
                {
                    var lavado = await results.ReadAsync<Lavado>();
                    maizRemojo.ListaLavado = lavado.ToList();
                    AcondicionamientoMateriaPrima.remojo = maizRemojo;

                }
                
                /*Maiz Sancochado*/
                var maizSancochadoSql = await results.ReadAsync<Sancochado>();
                var maizSancochado = maizSancochadoSql.FirstOrDefault(); // Obtiene el primer (y único) elemento de la colección

                if (maizSancochado != null)
                {
                    var sancochado = await results.ReadAsync<SancochadoDetail>();
                    maizSancochado.ListaSancochado = sancochado.ToList();
                    AcondicionamientoMateriaPrima.sancochado = maizSancochado;

                }
            }
            
            using (MemoryStream pdfStream = new MemoryStream())
            {
                // Crear un objeto PDF y un escritor PDF
                using (var pdfWriter = new PdfWriter(pdfStream))
                {
                    using (var pdfDocument = new PdfDocument(pdfWriter))
                    {
                        // Crear un objeto Document
                        using (var document = new Document(pdfDocument))
                        {
                            InformacionHeadDocument objHead = new InformacionHeadDocument(
                                "IKC.PRO.F.02",
                                "CONTROL DE PELADO, REMOJO Y SANCOCHADO DE MAÍZ",
                                "12",
                                "04/01/2022"
                            );
                            
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderDocument(document, objHead));
                            TemplateAcondicionamiento.getTemplatePeladoRemojoSancochado(document, AcondicionamientoMateriaPrima);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            }  
        }
    }
}