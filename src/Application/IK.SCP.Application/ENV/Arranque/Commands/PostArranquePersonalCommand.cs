using Dapper;
using IK.SCP.Application.Common.Helpers;
using IK.SCP.Application.Common.Response;
using IK.SCP.Application.ENV.ViewModels;
using IK.SCP.Domain.Dtos;
using IK.SCP.Infrastructure;
using MediatR;

namespace IK.SCP.Application.ENV.Commands
{
    public class PostArranquePersonalCommand : IRequest<StatusResponse<int>>
    {
        public int ArranqueId { get; set; }
        public List<PersonalRequest> Personal { get; set; }
    }

    public class PostArranquePersonalCommandHandler : IRequestHandler<PostArranquePersonalCommand, StatusResponse<int>>
    {
        private readonly IUnitOfWork _uow;

        public PostArranquePersonalCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<StatusResponse<int>> Handle(PostArranquePersonalCommand request, CancellationToken cancellationToken)
        {
            using (var cnn = _uow.Context.CreateConnection)
            {
                try
                {
                    var personal = DataConvertHelper.ToDataTable<PersonalRequest>(request.Personal);

                    var parametros = new
                    {
                        p_ArranqueId = request.ArranqueId,
                        p_Personal = personal,
                        p_UsuarioCreacion = _uow.UserName
                    };

                    var id_arranque = await cnn.ExecuteScalarAsync<int>("ENV.INSERTAR_ARRANQUE_PERSONAL", parametros, commandType: System.Data.CommandType.StoredProcedure);

                    return new StatusResponse<int>() { Ok = true, Data = id_arranque };
                }
                catch (Exception ex)
                {
                    return new StatusResponse<int>() { Ok = false };
                }

            }
        }
    }
}
