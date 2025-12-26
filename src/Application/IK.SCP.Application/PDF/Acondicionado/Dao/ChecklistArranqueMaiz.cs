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

public class ChecklistArranqueMaiz : IRequest<StatusResponse>
{
    public string ArranqueMaizId { get; set; }
}

public class ChecklistArranqueMaizHandler : IRequestHandler<ChecklistArranqueMaiz, StatusResponse>
{
    private readonly IUnitOfWork _uow;

    public ChecklistArranqueMaizHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ChecklistArranqueMaiz request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_ArranqueMaizId = request.ArranqueMaizId };
            var nameProcedure = "PDF.ACO_OBTENER_ARRANQUE_MAIZ";

            var results = await cnn.QueryMultipleAsync(nameProcedure, parameters,
                commandType: CommandType.StoredProcedure);
            
            var checklistArranqueMaiz = await results.ReadFirstOrDefaultAsync<ChecklistArranqueMaizResponse>();

            if (checklistArranqueMaiz != null)
            {
                var condicionesBasicas = await results.ReadAsync<CondicionesPreviasMaiz>();
                checklistArranqueMaiz.listaCondicionesPrevias = condicionesBasicas.ToList();
            
                var VerificacionEquipo = await results.ReadAsync<AcondicionamientoArranqueVerificacionEquipoMaiz>();
                checklistArranqueMaiz.listaVerificacionEquipo = VerificacionEquipo.ToList();
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
                            TemplateAcondicionamiento.getTemplateCheckArranqueMaiz(document, checklistArranqueMaiz);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            }      
        }
    }
}