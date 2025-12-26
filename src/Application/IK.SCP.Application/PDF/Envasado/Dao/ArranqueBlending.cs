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

public class ArranqueBlending : IRequest<StatusResponse>
{
    public int ArranqueVerificacionEquipoId { get; set; }
}

public class ArranqueBlendingHandler : IRequestHandler<ArranqueBlending, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public ArranqueBlendingHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ArranqueBlending request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var results = await cnn.QueryMultipleAsync("PDF.ENV_BLENDING_ARRANQUE", new { p_Id = request.ArranqueVerificacionEquipoId }, 
                commandType: CommandType.StoredProcedure);

            var arranqueBlending = await results.ReadFirstOrDefaultAsync<ArranqueBlendingResponse>();

            if (arranqueBlending != null)
            {
                var componentes = await results.ReadAsync<ComponenteBlending>();
                var condicionesPrevia = await results.ReadAsync<ArranqueCondicionPreviaBlending>();
                var verificacionEquipo = await results.ReadAsync<VerificacionEquipoBlending>();
                var observacion = await results.ReadAsync<ObservacionBlending>();

                arranqueBlending.Componentes = componentes.ToList();
                arranqueBlending.CondicionesPrevias = condicionesPrevia.ToList();
                arranqueBlending.VerificacionesEquipo = verificacionEquipo.ToList();
                arranqueBlending.Observaciones = observacion.ToList();
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
                                "IKC.PRO.F.56",
                                "CHECK LIST DE ARRANQUE DE BLENDING",
                                "02",
                                "10/06/2021"
                            );
                            
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderDocument(document, objHead));
                            TemplateEnvasado.printDocumentArranqueBlending(document, arranqueBlending);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            } 
        }
    }
}