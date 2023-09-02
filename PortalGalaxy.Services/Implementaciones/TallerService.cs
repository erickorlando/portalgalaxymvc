using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Entities;
using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;
using System.Linq.Expressions;

namespace PortalGalaxy.Services.Implementaciones
{
    public class TallerService : ITallerService
    {
        private readonly ITallerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TallerService> _logger;

        public TallerService(ITallerRepository repository, IMapper mapper, ILogger<TallerService> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<BaseResponse> AddAsync(TallerDtoRequest request)
        {

            var response = new BaseResponse();

            try
            {
                await _repository.AddAsync(_mapper.Map<Taller>(request));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al agregar";
                _logger.LogError(ex, "{ErroMessage} {Message}", ex.Message);
            }

            return response;

        }

        public async Task<PaginationResponse<TallerDtoResponse>> ListAsync(string? nombre, int categoriaId, 
            int? situacion, int page, int rows)
        {

            var response = new PaginationResponse<TallerDtoResponse>();

            try
            {
                //Expression<Func<Taller, bool>> predicate = x => x.Nombre.Contains(nombre ?? string.Empty)
                //                            && x.CategoriaId == categoriaId;

                //if (situacion != null)
                //{
                //    predicate = x => x.Nombre.Contains(nombre ?? string.Empty)
                //                            && x.CategoriaId == categoriaId
                //                            && x.Situacion == (SituacionTaller)situacion.Value;
                //}

                //var tupla = await _repository
                //    .ListAsync<TallerDtoResponse, string>(
                //        predicate: predicate,
                //        selector: x => _mapper.Map<TallerDtoResponse>(x),
                //        orderBy: p => p.Nombre,
                //        relationships: "Instructor,Categoria",
                //        page,
                //        rows);

                //response.Data = tupla.Collection;
                //response.TotalPages = tupla.Total / rows;
                //if (tupla.Total % rows > 0)
                //{
                //    response.TotalPages++;
                //}

                var collection = await _repository.ListarTalleresAsync(nombre ?? string.Empty, categoriaId, situacion ?? 0, page, rows);

                response.Data = _mapper.Map<ICollection<TallerDtoResponse>>(collection);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al Listar los Talleres";
                _logger.LogError(ex, "{ErroMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;

        }
    }
}
