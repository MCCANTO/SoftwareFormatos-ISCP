namespace IK.SCP.App.Routes
{
    public static partial class ApiRoutes
    {
        const string API_BASE = "api";
        const string VERSION = "v1";
        const string URL_BASE = $"{API_BASE}/{VERSION}";

        #region Auth

        public const string POST_AUTH_VALIDATE = URL_BASE + "/auth/validate";
        public const string POST_AUTH_LOGIN = URL_BASE + "/auth/login";
        public const string GET_AUTH_INFO = URL_BASE + "/auth/info";


        #endregion Auth

    }
}
