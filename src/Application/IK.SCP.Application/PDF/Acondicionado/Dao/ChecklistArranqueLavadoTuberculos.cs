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

public class ChecklistArranqueLavadoTuberculos : IRequest<StatusResponse>
{
    public string ArranqueLavadoTuberculoId { get; set; }
}

public class ChecklistArranqueLavadoTuberculosHandler : IRequestHandler<ChecklistArranqueLavadoTuberculos, StatusResponse>
{
    private readonly IUnitOfWork _uow;

    public ChecklistArranqueLavadoTuberculosHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ChecklistArranqueLavadoTuberculos request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_ArranqueLavadoTuberculoId = request.ArranqueLavadoTuberculoId };
            var nameProcedure = "PDF.ACO_CHECKLIST_ARRANQUE_LAVADO_TUBERCULO";

            var results = await cnn.QueryMultipleAsync(nameProcedure, parameters,
                commandType: CommandType.StoredProcedure);
            
            var checklistArranqueLavadoTuberculos = await results.ReadFirstOrDefaultAsync<ChecklistArranqueLavadoTuberculosResponse>();

            if (checklistArranqueLavadoTuberculos != null)
            {
                var condicionesBasicas = await results.ReadAsync<CondicionesPrevias>();
                checklistArranqueLavadoTuberculos.listaCondicionesPrevias = condicionesBasicas.ToList();
            
                var VerificacionEquipo = await results.ReadAsync<AcondicionamientoArranqueVerificacionEquipo>();
                checklistArranqueLavadoTuberculos.listaVerificacionEquipo = VerificacionEquipo.ToList();
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
                            TemplateAcondicionamiento.getTemplateCheckArranqueLavadoTuberculos(document, checklistArranqueLavadoTuberculos);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            }      
        }
    }
}