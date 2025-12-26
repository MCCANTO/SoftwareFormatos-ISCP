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

public class ArranqueMaquinistaEnvasado : IRequest<StatusResponse>
{
    public int ArranqueMaquinaId { get; set; }
}

public class ArranqueMaquinistaEnvasadoHandler : IRequestHandler<ArranqueMaquinistaEnvasado, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public ArranqueMaquinistaEnvasadoHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ArranqueMaquinistaEnvasado request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var results = await cnn.QueryMultipleAsync("PDF.ENV_PA_OBTENER_ARRANQUE_MAQUINA", new { p_ArranqueMaquinaId = request.ArranqueMaquinaId}, 
                commandType: CommandType.StoredProcedure);

            var arranquePDF = await results.ReadFirstOrDefaultAsync<ArranqueMaquinistaEnvasadoResponse>();

            if (arranquePDF != null)
            {
                var condicionesPrevias = await results.ReadAsync<CondicionesPrevias>();
                var fechaVariableBasicas = await results.ReadAsync<FechaVariableBasica>();
                var variablesBasicas = await results.ReadAsync<VariablesBasicas>();
                var observaciones = await results.ReadAsync<ArranqueMaquinaObservacionViewModel>();

                arranquePDF.condicionesPrevias = condicionesPrevias.ToList();
                arranquePDF.fechaVariableBasica = fechaVariableBasicas.ToList();
                arranquePDF.variablesBasicas = variablesBasicas.ToList();
                arranquePDF.observaciones = observaciones.ToList();
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
                                "IKC.PRO.F.58",
                                "CHECK LIST DE ARRANQUE DE ENVASADO -  MÁQUINAS",
                                "07",
                                "26/07/2022"
                            );
                            
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderDocument(document, objHead));
                            TemplateEnvasado.printDocumentArranqueMaquinistaEnvasado(document, arranquePDF);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            } 
        }
    }
}