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

public class ControlReposoMaiz : IRequest<StatusResponse>
{
    public string ordenId    { get; set; }
}

public class ControlReposoMaizHandler : IRequestHandler<ControlReposoMaiz, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public ControlReposoMaizHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<StatusResponse> Handle(ControlReposoMaiz request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_OrdenId = request.ordenId };
            var nameProcedure = "PDF.ACO_LISTAR_REPOSO_MAIZ_CONTROL";

            
            var results = await cnn.QueryMultipleAsync(nameProcedure, parameters,
                commandType: CommandType.StoredProcedure);

            var AcondicionamientoMateriaPrima = await results.ReadFirstOrDefaultAsync<ControlReposoRemojoResponse>();

            if (AcondicionamientoMateriaPrima != null)
            {
                var reposoMaiz = await results.ReadAsync<ControlReposoRemojoDetail>();
                AcondicionamientoMateriaPrima.ListaControlReposoRemojo = reposoMaiz.ToList();
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
                                "IKC.CCA.F.105",
                                "CARACTERIZACION DE PRODUCTO TERMINADO",
                                "08",
                                "23/11/2020"
                            );
                            
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderDocument(document, objHead));
                            TemplateAcondicionamiento.GetTemplateRemojoReposo(document, AcondicionamientoMateriaPrima, 2);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            }          
        }
    }
}