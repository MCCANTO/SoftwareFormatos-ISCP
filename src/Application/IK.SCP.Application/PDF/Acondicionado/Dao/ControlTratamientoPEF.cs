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

public class ControlTratamientoPEF : IRequest<StatusResponse>
{
    public string ordenId    { get; set; }
}

public class ControlTratamientoPEFHandler : IRequestHandler<ControlTratamientoPEF, StatusResponse>
{
    private readonly IUnitOfWork _uow;
    public ControlTratamientoPEFHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<StatusResponse> Handle(ControlTratamientoPEF request, CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
            var parameters = new { p_OrdenId = request.ordenId };
            var nameProcedure = "PDF.ACO_OBTENER_CONTROL_TRATAMIENTO_PEF";

            
            var results = await cnn.QueryMultipleAsync(nameProcedure, parameters,
                commandType: CommandType.StoredProcedure);

            var AcondicionamientoMateriaPrima = await results.ReadFirstOrDefaultAsync<ControlTratamientoPEFResponse>();

            if (AcondicionamientoMateriaPrima != null)
            {
                var condicionPrevias = await results.ReadAsync<CondicionPrevia>();
                var fuerzaCortes = await results.ReadAsync<FuerzaCortes>();
                var controlTratamientoTiempo = await results.ReadAsync<Tiempos>();

                AcondicionamientoMateriaPrima.ListaCondicionesPrevias = condicionPrevias.ToList();
                AcondicionamientoMateriaPrima.ListaFuerzaCortes = fuerzaCortes.ToList();
                AcondicionamientoMateriaPrima.ListaTiempos = controlTratamientoTiempo.ToList();
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
                            TemplateAcondicionamiento.GetTemplateTratamientoPEF(document, AcondicionamientoMateriaPrima);
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }     
            }          
        }
    }
}
    
