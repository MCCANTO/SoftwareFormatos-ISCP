using IK.SCP.Application.Common.Helpers;
using IK.SCP.Application.SEG.Auth.Queries;
using IK.SCP.Application.SEG.ViewModels;
using IK.SCP.Application.ViewModels;
using IK.SCP.Externo;
using MediatR;
using Microsoft.Extensions.Configuration;
using ServiceReference;

namespace IK.SCP.Application.Commands
{
    public class LoginCommand : LoginRequest, IRequest<LoginResponse>
    {
        public string usuario { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IAutorizacionService _autorizacionService;
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;

        public LoginCommandHandler(IConfiguration config, IAutorizacionService autorizacionService, IMediator mediator)
        {
            _config = config;
            _autorizacionService = autorizacionService;
            _mediator = mediator;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new LoginResponse() { Ok = false };

            try
            {
                var codApp = _config["AppSettings:_CODAPP"]?.ToString();
                var key = _config["AppSettings:EncryptionKey"]?.ToString();

                var _credencial = await _autorizacionService.ObtenerCredencial(request.usuario);
                var _rol = await _autorizacionService.ObtenerRol(codApp, request.usuario);

                var nodos = await _autorizacionService.ListarNodosXUsuario(codApp ?? "", request.usuario);

                var acciones = (await _mediator.Send(new GetAccionesXRolQuery() { RolId = _rol.RolId })).Data;

                var _authHelper = new AuthHelper();

                var token = _authHelper.GenerateToken(_credencial, _rol, nodos, acciones, key);

                response.Ok = true;
                response.Token = token;
                response.Message = "Usuario correcto.";

            }
            catch (Exception ex)
            {
                response.Ok = false;
            }


            return response;
        }
    }
}
