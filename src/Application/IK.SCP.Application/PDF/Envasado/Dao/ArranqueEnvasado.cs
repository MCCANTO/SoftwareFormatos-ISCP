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

public class ArranqueEnvasado : IRequest<StatusResponse>
{
    public int envasadoraId { get; set; }
    public string orden { get; set; }
    public string arranqueId { get; set; }
}

public class ArranqueEnvasadoHandler : IRequestHandler<ArranqueEnvasado, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public ArranqueEnvasadoHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ArranqueEnvasado request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parametros = 
                new { p_EnvasadoraId = request.envasadoraId, p_OrdenId = request.orden, p_ArranqueId = request.arranqueId  };
            var results = await cnn.QueryMultipleAsync("[PDF].[ENV_CHECKLIIST_ARRANQUE_ENVASADO]", parametros, 
                commandType: CommandType.StoredProcedure);

            var arranquePDF = await results.ReadFirstOrDefaultAsync<ArranqueEnvasadoResponse>();

            if (arranquePDF != null)
            {
                var condicionesPrevias = await results.ReadAsync<CondicionesPreviasArranqueEnvasado>();
                var variablesBasicas = await results.ReadAsync<VariablesBasicasArranqueEnvasado>();
                var imagenesSobres = await results.ReadAsync<ImagenCodificacionSobre>();
                var imagenesCajas = await results.ReadAsync<ImagenCodificacionCaja>();
                var contramuestras = await results.ReadAsync<CantidadesCajaSobre>();
                var empacadorPaletizadors = await results.ReadAsync();
                var evaluacionSensorial = await results.ReadAsync<EvaluacionSensorialArranqueComponente>();
                var observacionesArranques = await results.ReadAsync<ObservacionesArranque>();
                var inspeccionEtiquetado = await results.ReadAsync<InspeccionEtiquetadoArranqueEnvasado>();
                var revisores = await results.ReadAsync();
                var responsablesVB = await results.ReadAsync<ResponsablesVariablesBasicas>();

                arranquePDF.condicionPrevia = condicionesPrevias.ToList();
                arranquePDF.VariablesBasicas = variablesBasicas.ToList();
                arranquePDF.ImagenesSobres = imagenesSobres.ToList();
                arranquePDF.ImagenesCajas = imagenesCajas.ToList();
                arranquePDF.CantidadesValores = contramuestras.ToList();
                arranquePDF.empacadorPaletizador = empacadorPaletizadors.ToList();
                arranquePDF.EvaluacionSensorial = evaluacionSensorial.ToList();
                arranquePDF.Observaciones = observacionesArranques.ToList();
                arranquePDF.InspeccionEtiquetado = inspeccionEtiquetado.ToList();
                arranquePDF.revisores = revisores.ToList();
                arranquePDF.responsablesVarBasicas = responsablesVB.ToList();
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
                                "IKC.PRO.F.16",
                                "CHECK LIST DE ARRANQUE DE ENVASADO",
                                "15",
                                "12/04/2024"
                            );
                            
                            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderDocument(document, objHead));
                            TemplateEnvasado.printDocumentArranqueEnvasado(document, arranquePDF);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            } 
        }
    }
}