namespace ApiVersioning.V1.Models
{
    public class PostRequest
    {
        public string Nome { get; set; }
    }
    public class PostUgualeFraVersioniRequest
    {
        public string Indirizzo { get; set; }
    }
}

namespace ApiVersioning.V2.Models
{
    public class PostRequest
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
    }
}
