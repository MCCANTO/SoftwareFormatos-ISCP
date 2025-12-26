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

public class ControlRayosXAcond : IRequest<StatusResponse>
{
    public string periodo { get; set; }
}

public class ControlRayosXAcondHandler : IRequestHandler<ControlRayosXAcond, StatusResponse>
{
    private readonly IUnitOfWork _uow;

    public ControlRayosXAcondHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ControlRayosXAcond request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_Periodo = request.periodo };
            var nameProcedure = "PDF.ACO_LISTAR_CONTROL_RAYOS_X";

            var results = await cnn.QueryMultipleAsync(nameProcedure, parameters,
                commandType: CommandType.StoredProcedure);

            ControlRayosXAcondResponse controlRayosXAcond = new  ControlRayosXAcondResponse();
            controlRayosXAcond.Año = request.periodo.Substring(0, 4);;
            controlRayosXAcond.Mes = request.periodo.Substring(4, 2);

            if (controlRayosXAcond != null)
            {
                var condicionesBasicas = await results.ReadAsync<ControlMonitoreo>();
                controlRayosXAcond.listaControlMonitoreo = condicionesBasicas.ToList();
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
                                "IKC.HCP.F.77",
                                "CONTROL Y MONITOREO DE DETECCIÓN DE PATRONES EN RAYOS X",
                                "02",
                                "06/04/2022"
                            );
                            
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderDocument(document, objHead));
                            TemplateAcondicionamiento.getTemplateContrlRayosX(document, controlRayosXAcond);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            }      
        }
    }
}