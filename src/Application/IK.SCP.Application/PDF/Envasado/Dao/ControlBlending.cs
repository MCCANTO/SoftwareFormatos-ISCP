using System.Data;
using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.PDF.Envasado.Model;
using IK.SCP.Application.PDF.Templates;
using IK.SCP.Infrastructure;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using MediatR;

namespace IK.SCP.Application.PDF.Envasado.Dao;

public class ControlBlending : IRequest<StatusResponse>
{
    public string Orden { get; set; }
}

public class ControlBlendingHandler : IRequestHandler<ControlBlending, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public ControlBlendingHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ControlBlending request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var results = await cnn.QueryMultipleAsync("PDF.ENV_BLENDING_CONTROL", new { p_OrdenId = request.Orden }, 
                commandType: CommandType.StoredProcedure);

            var controlBlending = await results.ReadFirstOrDefaultAsync<ControlBlendingResponse>();

            if (controlBlending != null)
            {
                var componentes = await results.ReadAsync<ComponenteMix>();
                var headTable = await results.ReadAsync<HeadTableBlending>();
                var merma = await results.ReadAsync<MermaBlending>();
                var dataTable = results.Read<dynamic>();

                controlBlending.componentes = componentes.ToList();
                controlBlending.headTable = headTable.ToList();
                controlBlending.merma = merma.ToList();
                controlBlending.dataTable = dataTable.ToList();
            }
            
            
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
                            TemplateEnvasado.printDocumentControlBlending(document, controlBlending);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            } 
        }
    }
}