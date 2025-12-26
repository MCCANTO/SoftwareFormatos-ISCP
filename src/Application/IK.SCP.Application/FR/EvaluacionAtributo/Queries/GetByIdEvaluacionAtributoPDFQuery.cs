using System.Data;
using Dapper;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using IK.SCP.Application.Common.Helpers.PDF;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Events;

namespace IK.SCP.Application.FR.Queries;

public class GetByIdEvaluacionAtributoPDFQuery : IRequest<StatusResponse>
{
    public int Linea { get; set; }
    public string Orden    { get; set; }
}

public class  GetByIdEvaluacionAtributoPDFQueryHandler : IRequestHandler<GetByIdEvaluacionAtributoPDFQuery, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public GetByIdEvaluacionAtributoPDFQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<StatusResponse> Handle(GetByIdEvaluacionAtributoPDFQuery request, CancellationToken cancellationToken)
    {

        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_Linea = request.Linea, p_OrdenId = request.Orden };

            var results = await cnn.QueryMultipleAsync("FR.OBTENER_INFO_EVALUACION_ATRIBUTO_PDF", parameters,
                commandType: CommandType.StoredProcedure);

            var orden = await results.ReadFirstOrDefaultAsync<GetByIdEvaluacionAtributoPDFResponse>();

            if (orden != null)
            {
                var evaluacionAtributo = await results.ReadAsync<GetByOrdenEvaluacionAtributo>();
                orden.EvaluacionAtributos = evaluacionAtributo.ToList();
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
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler(document, "0", request.Linea));
                            TemplateFritura.GetTemplateAttributeEvaluation(document, orden);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            }          
        }
    }
}