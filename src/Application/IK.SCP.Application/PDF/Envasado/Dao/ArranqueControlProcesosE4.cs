using System.Data;
using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.PDF.Envasado.Model;
using IK.SCP.Application.PDF.Helpers;
using IK.SCP.Application.PDF.Templates;
using IK.SCP.Infrastructure;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using MediatR;

namespace IK.SCP.Application.PDF.Envasado.Dao;

public class ArranqueControlProcesosE4 : IRequest<StatusResponse>
{
    public string ArranqueGranelId { get; set; }
}

public class ArranqueControlProcesosE4Handler : IRequestHandler<ArranqueControlProcesosE4, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public ArranqueControlProcesosE4Handler(IUnitOfWork unitOf)
    {
        _uow = unitOf;
    }
    public async Task<StatusResponse> Handle(ArranqueControlProcesosE4 request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_ArranqueGranelId = request.ArranqueGranelId };
            var results = await cnn.QueryMultipleAsync("[PDF].[ENV_GRANEL_ARRANQUE_CHECKLIST]", parameters, commandType: CommandType.StoredProcedure);
            
            var arranqueGranel = await results.ReadFirstOrDefaultAsync<ArranqueControlProcesosE4Response>();

            if (arranqueGranel != null)
            {
                var especificaciones = await results.ReadAsync();
                var condicionesOperativas = await results.ReadAsync<CondicionesOperativasEnvasadoGranel>();
                var condicionesProceso = await results.ReadAsync<CondicionesProcesoEnvasadoGranel>();
                var observaciones = await results.ReadAsync<ObservacionesEnvasadoGranel>();
                var signature = await results.ReadAsync();
                var fechaControlProceso = await results.ReadAsync<DateTime>();
                var detalleControlProceso = await results.ReadAsync();
                var observacionesControlProceso = await results.ReadAsync();
                var evaluacionPt = await results.ReadAsync<evaluacionPTControlProceso>();
                var ImgCodificacionCaja = await results.ReadAsync();
                var turnosE4Cps = await results.ReadAsync<turnosE4CP>();

                arranqueGranel.EspecificacionesEnvasadoGranel = especificaciones.ToList();
                arranqueGranel.CondicionesOperativas = condicionesOperativas.ToList();
                arranqueGranel.CondicionesProceso = condicionesProceso.ToList();
                arranqueGranel.Observacion = observaciones.ToList();
                
                var controlProceso = new ControlProcesoEnvasadoGranel();
                if (fechaControlProceso != null && detalleControlProceso != null)
                {
                    controlProceso.FechaControlProceso = fechaControlProceso.ToList();
                    controlProceso.DetalleControlProceso = detalleControlProceso.ToList();
                }

                arranqueGranel.ControlProceso = controlProceso;
                arranqueGranel.ObservacionControlProceso = observacionesControlProceso.ToList();
                arranqueGranel.EvaluacionAtributos = evaluacionPt.ToList();
                arranqueGranel.ImgCodificacionCaja = ImgCodificacionCaja.ToList();
                arranqueGranel.TurnosE4Cps = turnosE4Cps.ToList();
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
                                "IKC.PRO.F.16",
                                "CHECK LIST Y CONTROL DE PROCESO DE ENVASADO - GRANEL",
                                "05",
                                "05/05/2021"
                            );
                            
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderDocument(document, objHead));
                            TemplateEnvasado.printDocumentEnvasadoGranelCheckListArranque(document, arranqueGranel);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            } 
        }
    }
}


