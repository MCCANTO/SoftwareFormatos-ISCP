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

public class ArranqueManufactura : IRequest<StatusResponse>
{
    public string Orden { get; set; }
    public int Linea { get; set; }
    public int IdArranqueMaquina { get; set; }
    public string Articulo { get; set; }
}

public class ArranqueManufacturaHandler : IRequestHandler<ArranqueManufactura, StatusResponse>
{
    private readonly IUnitOfWork _uow;

    public ArranqueManufacturaHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<StatusResponse> Handle(ArranqueManufactura request,
        CancellationToken cancellationToken)
    {
        using (var cnn = _uow.Context.CreateConnection)
        {
         
            var parameters = new { p_Linea = request.Linea, p_OrdenId = request.Orden, p_ArranqueMaquinaId = request.IdArranqueMaquina };
            
            var results = await cnn.QueryMultipleAsync("PDF.FR_OBTENER_ARRANQUE_MAQUINA_ACTIVO", parameters, commandType: CommandType.StoredProcedure);
            var arranque = await results.ReadFirstOrDefaultAsync<ArranqueManufacturaResponse>();
            
            if (arranque != null)
            {
                var condiciones = await results.ReadAsync<Condicion>();
                
                var verificacionesEquipoCollection = await results.ReadAsync<VerificacionEquipo>();
                var verificacionesEquipo = verificacionesEquipoCollection.FirstOrDefault(); // Obtiene el primer (y único) elemento de la colección

                if (verificacionesEquipo != null)
                {
                    var verificacionesEquipoArranque = await results.ReadAsync<VerificacionPreviaArranque>();
                    verificacionesEquipo.VerificacionesArranque = verificacionesEquipoArranque.ToList();
                }

                var observaciones = await results.ReadAsync<Observacion>();
                var sensoriales = await results.ReadAsync<Sensorial>();
                
                arranque.Condiciones = condiciones.ToList();
                arranque.Verificaciones = verificacionesEquipo;
                arranque.Observaciones = observaciones.ToList();
                arranque.Sensoriales = sensoriales.ToList();
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
                            TemplateFritura.GetTemplateLineasFrituras(document, arranque);
                        }
                    }
                }
                return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
            }
        }
    }
}