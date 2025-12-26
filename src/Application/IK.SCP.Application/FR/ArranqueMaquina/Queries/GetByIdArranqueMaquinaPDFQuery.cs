using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.FR.ViewModels;
using IK.SCP.Infrastructure;
using MediatR;
using System.Data;
using IK.SCP.Application.Common.Helpers.PDF;
using IK.SCP.Application.ENV.ViewModels;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Events;

namespace IK.SCP.Application.FR.Queries
{
    public class GetByIdArranqueMaquinaPDFQuery : IRequest<StatusResponse>
    {
        public string Orden    { get; set; }
        public int Linea { get; set; }
        public int IdArranqueMaquina    { get; set; }
        public string Articulo { get; set; }
    }

    public class GetByIdArranqueMaquinaPDFQueryHandler : IRequestHandler<GetByIdArranqueMaquinaPDFQuery, StatusResponse>
    {
        private readonly IUnitOfWork _uow;
        public GetByIdArranqueMaquinaPDFQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse> Handle(GetByIdArranqueMaquinaPDFQuery request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                // Information general of document
                Dictionary<string, string> DataGeneral = new Dictionary<string, string>
                {
                    { "Articulo", request.Articulo },
                    { "Orden", request.Orden },
                    { "Coordinador", "Coordinador" },
                    { "Turno", "Turno 1" },
                };
                
                var parameters = new
                {
                    p_Linea = request.Linea,
                    p_OrdenId = request.Orden,
                    p_ArranqueMaquinaId = request.IdArranqueMaquina
                };
                
                var results = await cnn.QueryMultipleAsync("FR.OBTENER_ARRANQUE_MAQUINA_ACTIVO_PDF", parameters, commandType: CommandType.StoredProcedure);
                
                var arranque = await results.ReadFirstOrDefaultAsync<GetByIdArranqueManufacturaResponse>();
                
                if (arranque != null)
                {
                    var condiciones = await results.ReadAsync<GetByIdArranqueMaquinaCondicionResponse>();
                    
                    var verificacionesEquipoCollection = await results.ReadAsync<GetByIdMaquinaVerificacionEquipoResponse>();
                    var verificacionesEquipo = verificacionesEquipoCollection.FirstOrDefault(); // Obtiene el primer (y único) elemento de la colección

                    if (verificacionesEquipo != null)
                    {
                        var verificacionesEquipoArranque = await results.ReadAsync<GetByIdMaquinaVerificacionEquipoArranqueResponse>();
                        verificacionesEquipo.VerificacionesArranque = verificacionesEquipoArranque.ToList();
                    }

                    
                    var observaciones = await results.ReadAsync<GetByIdArranqueMaquinaObservacionResponse>();
                    var sensoriales = await results.ReadAsync<GetByIdArranqueMaquinaEvaluacionSensorial>();
                    
                    arranque.Condiciones = condiciones.ToList();
                    arranque.Verificaciones = verificacionesEquipo;
                    arranque.Observaciones = observaciones.ToList();
                    arranque.Sensorials = sensoriales.ToList();
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
                                pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler(document, "0", request.Linea));
                                
                                TemplateFritura.GetTemplateLineasFrituras(document, arranque.Condiciones,
                                                                                    arranque.Verificaciones,
                                                                                    arranque.Observaciones,
                                                                                    arranque.Sensorials,
                                                                                    DataGeneral);
                            }
                        }
                    }
                    return StatusResponse.True("Datos PDF obtenidos correctamente", data: pdfStream);
                }
            }
        }
    }
}
