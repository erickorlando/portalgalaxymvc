#nullable disable
// La linea de arriba desactiva la comprobacion de nulos a nivel de archivo

namespace PortalGalaxy.Common
{

    public class AppConfiguration
    {
        public Jwt Jwt { get; set; }
        public AzureBlobStorage AzureBlobStorage { get; set; }
    }

    public class Jwt
    {
        public string SecretKey { get; set; }
        public string Audiencia { get; set; }
        public string Emisor { get; set; }
    }

    public class AzureBlobStorage
    {
        public string ConnectionString { get; set; }
        public string PublicUrl { get; set; }
    }
}
