using ApiVersioning.V1.Models;

namespace ApiVersioning.BusinessLogic
{
    public class ApiVersion2Business : IApiVersion2Business
    {
        public string PostUgualeFraVersioni(PostUgualeFraVersioniRequest postUgualeFraVersioniRequest)
        {
            return $"Indirizzo: {postUgualeFraVersioniRequest.Indirizzo}";
        }
    }
}
