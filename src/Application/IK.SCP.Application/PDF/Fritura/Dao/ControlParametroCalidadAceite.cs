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

public class ControlParametroCalidadAceite : IRequest<StatusResponse>
{
    public DateTime Desde { get; set; }
    public DateTime Hasta { get; set; }
    public int LineaId { get; set; }
    public string? OrdenId { get; set; }   
}

public class ControlParametroCalidadAceiteHandler : IRequestHandler<ControlParametroCalidadAceite, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public ControlParametroCalidadAceiteHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<StatusResponse> Handle(ControlParametroCalidadAceite request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parametros = new
            {
                p_Desde = request.Desde,
                p_Hasta = request.Hasta,
                p_LineaId = request.LineaId,
                p_OrdenId = request.OrdenId
            };
            
            var nameProcedure = "PDF.CONTROL_PARAMETROS_CALIDAD_ACEITE";
            
            var result = await cnn.QueryAsync<ControlParametroCalidadAceiteResponse>(nameProcedure, parametros, commandType: CommandType.StoredProcedure);
            
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
                            TemplateFritura.GetTemplateControlAceites(document, result.ToList());
                        }
                    }
                }
                return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
            }
        }
    }
}