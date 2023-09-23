using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PortalGalaxy.Common;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.Services.Implementaciones;

public class AzureBlobStorageUploader : IFileUploader
{
    private readonly ILogger<AzureBlobStorageUploader> _logger;
    private readonly AzureBlobStorage _configuration;

    public AzureBlobStorageUploader(IOptions<AppConfiguration> options, ILogger<AzureBlobStorageUploader> logger)
    {
        _logger = logger;
        _configuration = options.Value.AzureBlobStorage;
    }

    public async Task<string> UploadFileAsync(string? base64, string? fileName)
    {
        if (string.IsNullOrWhiteSpace(base64) || string.IsNullOrWhiteSpace(fileName))
        {
            return string.Empty;
        }

        try
        {
            // Nos conectamos a la cuenta de almacenamiento
            var client = new BlobServiceClient(_configuration.ConnectionString);

            // Hacemos la referencia al container
            var container = client.GetBlobContainerClient("portal");

            var blob = container.GetBlobClient(fileName);

            await using var stream = new MemoryStream(Convert.FromBase64String(base64));
            await blob.UploadAsync(stream, overwrite: true); // Aqui escribe en el Blob Storage

            return $"{_configuration.PublicUrl}{fileName}";
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Error al subir el archivo {fileName} {Message}", fileName, ex.Message);

            return string.Empty;
        }
    }
}