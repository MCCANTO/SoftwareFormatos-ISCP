using System.Data;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;
using IK.SCP.Application.Common.Helpers.PDF;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Events;
using Dapper;
using IK.SCP.Application.ACO.General.ViewModels;
using IK.SCP.Application.FR.ViewModels;

namespace IK.SCP.Application.ACO.Queries;

public class GetByIdReposoMaizPDFQuery : IRequest<StatusResponse>
{
    public int materiaPrimaId { get; set; }
    public string ordenId    { get; set; }
}

public class GetByIdReposoMaizPDFQueryHandler : IRequestHandler<GetByIdReposoMaizPDFQuery, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public GetByIdReposoMaizPDFQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(GetByIdReposoMaizPDFQuery request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_OrdenId = request.ordenId };
            var nameProcedure = request.materiaPrimaId == 1 ? "ACO.LISTAR_REPOSO_MAIZ_CONTROL" : "ACO.LISTAR_REMOJO_HABA_CONTROL";
            var results = await cnn.QueryMultipleAsync(nameProcedure, parameters,
                commandType: CommandType.StoredProcedure);

            var sancochadoMaiz = await results.ReadFirstOrDefaultAsync<GetByIdReposoMaizPDFResponse>();

            if (sancochadoMaiz != null)
            {
                var reposoMaiz = await results.ReadAsync<GetByOrdenReposoMaiz>();
                sancochadoMaiz.getByOrdenReposoMaiz = reposoMaiz.ToList();
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
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler(document, "54", 0));
                            TemplateFritura.GetTemplateAcondicionamientoMaizHabas(document, sancochadoMaiz);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            }          
        }
    }
}