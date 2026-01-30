
using Identity.Client.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Client.Services;

public abstract class CrudServiceBase<TListDto, TGetDto, TCreateRequest, TUpdateRequest>
    : ICrudService<TListDto, TGetDto, TCreateRequest, TUpdateRequest>
{
    protected readonly SafeHttp _http;
    private readonly string _baseUrl;

    protected CrudServiceBase(SafeHttp http, string baseUrl)
    {
        _http = http;
        _baseUrl = baseUrl.TrimEnd('/');
    }

    public Task<Result<List<TListDto>>> GetAllAsync()
        => _http.Get<List<TListDto>>($"{_baseUrl}");

    public Task<Result<TGetDto>> GetAsync(Guid id)
        => _http.Get<TGetDto>($"{_baseUrl}/{id}");

    public Task<Result<TGetDto>> CreateAsync(TCreateRequest request)
        => _http.Post<TGetDto>(
            $"{_baseUrl}",
            request,
            $"{typeof(TGetDto).Name} created successfully"
        );

    public Task<Result<TGetDto>> UpdateAsync(TUpdateRequest request)
        => _http.Put<TGetDto>(
            $"{_baseUrl}/{GetId(request)}",
            request,
            $"{typeof(TGetDto).Name} updated successfully"
        );

    public Task<Result<bool>> DeleteAsync(Guid id)
        => _http.Delete(
            $"{_baseUrl}/{id}",
            $"{typeof(TGetDto).Name} deleted successfully"
        );

    // Each module implements this to extract the ID from its update request
    protected abstract Guid GetId(TUpdateRequest request);
}

