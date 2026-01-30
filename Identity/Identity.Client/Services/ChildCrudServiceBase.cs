using Identity.Client.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class ChildCrudServiceBase<TListDto, TGetDto, TCreateRequest, TUpdateRequest>
{
    protected readonly SafeHttp _http;
    private readonly string _template;

    protected ChildCrudServiceBase(SafeHttp http, string template)
    {
        _http = http;
        _template = template;
    }

    private string Url(Guid parentId)
        => _template.Replace("{parentId}", parentId.ToString());

    private string Url(Guid parentId, Guid id)
        => $"{Url(parentId)}/{id}";

    public Task<Result<List<TListDto>>> GetAllAsync(Guid parentId)
        => _http.Get<List<TListDto>>(Url(parentId));

    public Task<Result<TGetDto>> GetAsync(Guid parentId, Guid id)
        => _http.Get<TGetDto>(Url(parentId, id));

    public Task<Result<TGetDto>> CreateAsync(Guid parentId, TCreateRequest request)
        => _http.Post<TGetDto>(Url(parentId), request, $"{typeof(TGetDto).Name} created successfully");

    public Task<Result<TGetDto>> UpdateAsync(Guid parentId, TUpdateRequest request)
        => _http.Put<TGetDto>(Url(parentId, GetId(request)), request, $"{typeof(TGetDto).Name} updated successfully");

    public Task<Result<bool>> DeleteAsync(Guid parentId, Guid id)
        => _http.Delete(Url(parentId, id), $"{typeof(TGetDto).Name} deleted successfully");

    protected abstract Guid GetId(TUpdateRequest request);
}
