using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Models.Response;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.Services.Implementaciones
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        private readonly ILogger<CategoriaService> _logger;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository repository, ILogger<CategoriaService> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        public async Task<BaseResponseGeneric<ICollection<CategoriaDtoResponse>>> ListAsync()
        {
            var response = new BaseResponseGeneric<ICollection<CategoriaDtoResponse>>();

            try
            {
                response.Data = await _repository.ListAsync(x => new CategoriaDtoResponse
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al Listar las Categorias";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
    }
}
