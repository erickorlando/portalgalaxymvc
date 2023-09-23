using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;
using PortalGalaxy.WebMvc.Services.Interfaces;

namespace PortalGalaxy.WebMvc.Services.Implementaciones;

public class InstructorProxy : CrudRestHelperBase<InstructorDtoRequest, InstructorDtoResponse>, IInstructorProxy
{
    public InstructorProxy(HttpClient httpClient) 
        : base("api/Instructores", httpClient)
    {
    }

    public async Task<ICollection<InstructorDtoResponse>> ListAsync(string? nombre, string? nroDocumento, int? categoriaId)
    {
        try
        {
            var response =
               await HttpClient.GetAsync(
                   $"{BaseUrl}?nombre={nombre}&nroDocumento={nroDocumento}&categoriaId={categoriaId}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<BaseResponseGeneric<ICollection<InstructorDtoResponse>>>();

            if (result!.Success == false)
            {
                throw new InvalidOperationException(result.ErrorMessage);
            }

            return result.Data!;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}