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

public class ControlRemojoHabas : IRequest<StatusResponse>
{
    public string ordenId    { get; set; }
}

public class ControlRemojoHabasHandler : IRequestHandler<ControlRemojoHabas, StatusResponse>
{
    private readonly IUnitOfWork _uow;

    public ControlRemojoHabasHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<StatusResponse> Handle(ControlRemojoHabas request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_OrdenId = request.ordenId };
            var nameProcedure = "PDF.ACO_LISTAR_REMOJO_HABA_CONTROL";

            
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
                                "IKC.PRO.F.54",
                                "Control de Remojo habas  o  Reposo maíz",
                                "02",
                                "04/01/2022"
                            );
                            
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderDocument(document, objHead));
                            TemplateAcondicionamiento.GetTemplateRemojoReposo(document, AcondicionamientoMateriaPrima, 1);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            }          
        }
    }
} 