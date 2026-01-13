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

public class EvaluacionAtributo : IRequest<StatusResponse>
{
    public int Linea { get; set; }
    public string Orden    { get; set; }
}

public class EvaluacionAtributoHandler : IRequestHandler<EvaluacionAtributo, StatusResponse>
{
    private readonly IUnitOfWork _uow;

    public EvaluacionAtributoHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<StatusResponse> Handle(EvaluacionAtributo request, CancellationToken cancellationToken)
    {

        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_Linea = request.Linea, p_OrdenId = request.Orden };

            var results = await cnn.QueryMultipleAsync("PDF.FR_OBTENER_INFO_EVALUACION_ATRIBUTO", parameters,
                commandType: CommandType.StoredProcedure);

            var HeadEvaluacionAtributo = await results.ReadFirstOrDefaultAsync<EvaluacionAtributoResponse>();

            if (HeadEvaluacionAtributo != null)
            {
                var LineasEvaluacionAtributo = await results.ReadAsync<LineasEvaluacionAtributo>();
                HeadEvaluacionAtributo.EvaluacionAtributos = LineasEvaluacionAtributo.ToList();
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
                                "IKC.PRO.F.18",
                                "EVALUACIÓN DE ATRIBUTOS",
                                "02",
                                "06/09/2018"
                            );
                            
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderDocument(document, objHead));
                            TemplateFritura.GetTemplateAttributeEvaluation(document, HeadEvaluacionAtributo);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            }          
        }
    }
}