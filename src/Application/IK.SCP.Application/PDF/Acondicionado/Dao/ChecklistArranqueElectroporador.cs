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

public class ChecklistArranqueElectroporador : IRequest<StatusResponse>
{
    public int ArranqueElectroporadorId { get; set; }
}

public class ChecklistArranqueElectroporadorHandler : IRequestHandler<ChecklistArranqueElectroporador, StatusResponse>
{
    private readonly IUnitOfWork _uow;

    public ChecklistArranqueElectroporadorHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ChecklistArranqueElectroporador request, CancellationToken token)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_ArranqueElectroporadorId = request.ArranqueElectroporadorId };
            var nameProcedure = "PDF.ACO_CHECKLIST_ARRANQUE_ELECTROPORADOR";
            
            var results = await cnn.QueryMultipleAsync(nameProcedure, parameters,
                commandType: CommandType.StoredProcedure);

            var checklistArranqueElectroporador = await results.ReadFirstOrDefaultAsync<ChecklistArranqueElectroporadorResponse>();

            if (checklistArranqueElectroporador != null)
            {
                var condicionesBasicas = await results.ReadAsync<CondicionesBasicas>();
                checklistArranqueElectroporador.listaCondicionesBasicas = condicionesBasicas.ToList();
            
                var VerificacionEquipo = await results.ReadAsync<VerificacionEquipoElectroporador>();
                checklistArranqueElectroporador.listaVerificacionEquipo = VerificacionEquipo.ToList();
            
                var variablesBasicas = await results.ReadAsync<VariablesBasicas>();
                checklistArranqueElectroporador.listaVariablesBasicas = variablesBasicas.ToList();
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
                            TemplateAcondicionamiento.getTemplateCheckArranqueElectroporador(document, checklistArranqueElectroporador);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            } 
        }
    }
}