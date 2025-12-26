using ServiceReference;

namespace IK.SCP.Externo
{
    public interface IAutorizacionService
    {
        Task<Credencial> ObtenerCredencial(string usuario);
        Task<Rol> ObtenerRol(string codigoApp, string usuario);
        Task<Credencial> ValidarUsuario(string usuario, string clave);
        Task<List<Nodo>> ListarNodosXUsuario(string codigoApp, string usuario);
    }
}
