using System.Data;
using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.PDF.Fritura.Model;
using IK.SCP.Application.PDF.Helpers;
using IK.SCP.Application.PDF.Sazonado.Model;
using IK.SCP.Application.PDF.Templates;
using IK.SCP.Infrastructure;
using MediatR;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Events;
using iText.Kernel.Geom;

namespace IK.SCP.Application.PDF.Fritura.Dao;

public class CaracterizacionProductoTerminado : IRequest<StatusResponse>
{
    public string ordenId { get; set; }
}

public class CaracterizacionProductoTerminadoHandler : IRequestHandler<CaracterizacionProductoTerminado, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public CaracterizacionProductoTerminadoHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(CaracterizacionProductoTerminado request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parametros = new { p_OrdenId = request.ordenId };
            var nameProcedure = "PDF.FR_LISTAR_REGISTRO_CARACTERIZACION_DEFECTOS";
            
            var data = await cnn.QueryAsync<dynamic>(nameProcedure, parametros, commandType: CommandType.StoredProcedure);


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
                            
                            TemplateFritura.GetTemplateCaracterizacionProductosTerminados(document, data);
                        }
                    }
                }
                return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
            }
        }
    }
}