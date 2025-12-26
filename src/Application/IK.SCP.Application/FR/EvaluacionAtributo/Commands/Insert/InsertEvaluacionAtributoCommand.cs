using Dapper;
using IK.SCP.Application.Common.Response;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.FR.Commands
{
    public class InsertEvaluacionAtributoCommand : IRequest<StatusResponse<int>>
    {
        public int Linea { get; set; }
        public string OrdenId { get; set; }
        public string Panelistas { get; set; }
        public int AparienciaGeneral { get; set; }
        public int Color { get; set; }
        public int Olor { get; set; }
        public int Sabor { get; set; }
        public int Textura { get; set; }
        public int CalificacionFinal { get; set; }
        public string Observacion { get; set; }
    }

    public class InsertEvaluacionAtributoCommandHandler : IRequestHandler<InsertEvaluacionAtributoCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;
        public InsertEvaluacionAtributoCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatusResponse<int>> Handle(InsertEvaluacionAtributoCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    var parametros = new
                    {
                        p_Linea = request.Linea,
                        p_OrdenId = request.OrdenId,
                        p_Panelista = request.Panelistas,
                        p_AparienciaGeneral = request.AparienciaGeneral,
                        p_Color = request.Color,
                        p_Olor = request.Olor,
                        p_Sabor = request.Sabor,
                        p_Textura = request.Textura,
                        p_CalificacionFinal = request.CalificacionFinal,
                        p_Observacion = request.Observacion ?? "",
                        p_UsuarioCreacion = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("FR.INSERTAR_EVALUACION_ATRIBUTO", parametros, commandType: System.Data.CommandType.StoredProcedure);

                    return new StatusResponse<int> { Ok = true, Data = id_arranque };
                }
                catch (Exception ex)
                {
                    return new StatusResponse<int> { Ok = false, Message = "Error al registrar la información." };
                }
            }
        }
    }
}
