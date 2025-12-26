using System.Data;
using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.PDF.Envasado.Model;
using IK.SCP.Application.PDF.Helpers;
using IK.SCP.Application.PDF.Templates;
using IK.SCP.Infrastructure;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using MediatR;

namespace IK.SCP.Application.PDF.Envasado.Dao;

public class Pedaceria : IRequest<StatusResponse>
{
    public int envasadoraId { get; set; }
    public string ordenId { get; set; }
}

public class PedaceriaHandler : IRequestHandler<Pedaceria, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public PedaceriaHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(Pedaceria request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parametros = new { p_EnvasadoraId = request.envasadoraId,  p_OrdenId = request.ordenId };

            var result = await cnn.QueryAsync<dynamic>("PDF.ENV_LISTAR_REGISTRO_PEDACERIA", parametros, commandType: CommandType.StoredProcedure);
            
            using (MemoryStream pdfStream = new MemoryStream())
            {
                // Crear un objeto PDF y un escritor PDF
                using (var pdfWriter = new PdfWriter(pdfStream))
                {
                    using (var pdfDocument = new PdfDocument(pdfWriter))
                    {
                        // Rotate - Horizontal
                        pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());
                        
                        // Crear un objeto Document
                        using (var document = new Document(pdfDocument))
                        {
                            InformacionHeadDocument objHead = new InformacionHeadDocument(
                                "IKC.CCA.F.105",
                                "CARACTERIZACION DE PRODUCTO TERMINADO",
                                "08",
                                "23/11/2020"
                            );
                            
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderDocument(document, objHead));
                            TemplateEnvasado.printDocumentPedaceria(document, result.ToList());
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            } 
        }
    }
}