﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Entities;
using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.Services.Implementaciones;

public class InstructorService : IInstructorService
{
    private readonly IInstructorRepository _repository;
    private readonly ILogger<InstructorService> _logger;
    private readonly IMapper _mapper;

    public InstructorService(IInstructorRepository repository, ILogger<InstructorService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponseGeneric<ICollection<InstructorDtoResponse>>> ListAsync(string? nombre, string? nroDocumento, int? categoriaId)
    {
        var response = new BaseResponseGeneric<ICollection<InstructorDtoResponse>>();

        try
        {
            // Codigo
            var data = await _repository.ListAsync(nombre, nroDocumento, categoriaId);

            response.Data = data.Select(_mapper.Map<InstructorDtoResponse>).ToList();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al cargar los instructores";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponseGeneric<InstructorDtoResponse>> FindByIdAsync(int id)
    {

        var response = new BaseResponseGeneric<InstructorDtoResponse>();

        try
        {
            var data = await _repository.FindByIdAsync(id);

            response.Data = _mapper.Map<InstructorDtoResponse>(data);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al buscar el Instructor";
            _logger.LogError(ex, "{ErroMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }


    public async Task<BaseResponse> AddAsync(InstructorDtoRequest request)
    {

        var response = new BaseResponse();

        try
        {
            await _repository.AddAsync(_mapper.Map<Instructor>(request));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al agregar";
            _logger.LogError(ex, "{ErroMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponse> UpdateAsync(int id, InstructorDtoRequest request)
    {

        var response = new BaseResponse();

        try
        {
            var registro = await _repository.FindByIdAsync(id);

            if (registro is not null)
            {
                _mapper.Map(request, registro);

                await _repository.UpdateAsync();
            }

            response.Success = registro != null;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al actualizar";
            _logger.LogError(ex, "{ErroMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponse> DeleteAsync(int id)
    {

        var response = new BaseResponse();

        try
        {
            await _repository.DeleteAsync(id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al eliminar";
            _logger.LogError(ex, "{ErroMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }
}