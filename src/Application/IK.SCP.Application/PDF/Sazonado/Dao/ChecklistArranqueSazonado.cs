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

namespace IK.SCP.Application.PDF.Sazonado.Dao;

public class ChecklistArranqueSazonado : IRequest<StatusResponse>
{
    public string arranqueId { get; set; }
}


public class ChecklistArranqueSazonadoHandler : IRequestHandler<ChecklistArranqueSazonado, StatusResponse>
{
    private readonly IUnitOfWork _uow;

    public ChecklistArranqueSazonadoHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ChecklistArranqueSazonado request,
        CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_ArranqueId = request.arranqueId };
            var nameProcedure = "PDF.SAZ_CHECKLIST_ARRANQUE_SAZONADO";
            
            var results = await cnn.QueryMultipleAsync(nameProcedure, parameters,
                commandType: CommandType.StoredProcedure);
            
            var checklistArranqueSazonado = await results.ReadFirstOrDefaultAsync<ChecklistArranqueSazonadoResponse>();
            
            if (checklistArranqueSazonado != null)
            {
                var arranqueSazonado = await results.ReadAsync<ArranqueProducto>();
                var condicionPreviasSazonado = await results.ReadAsync<CondicionesPreviasSazonado>();
                var verificacionEquipoSazonado = await results.ReadAsync<VerificacionEquipoSazonado>();
                
                checklistArranqueSazonado.arranque = arranqueSazonado.FirstOrDefault();
                checklistArranqueSazonado.listaCondicionesPrevias = condicionPreviasSazonado.ToList();
                checklistArranqueSazonado.listaVerificacionEquipo = verificacionEquipoSazonado.ToList();
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
                            TemplateSazonado.GetTemplateArranqueSazonado(document, checklistArranqueSazonado);
                        }
                    }
                }
                return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
            }
        }
    }
}