using System;
using System.Collections.Generic;
using Identity.Client.Http;
using System.Threading.Tasks;

namespace Identity.Client.Services;


public interface ICrudService<TListDto, TGetDto, TCreateRequest, TUpdateRequest>
{
    Task<Result<List<TListDto>>> GetAllAsync();
    Task<Result<TGetDto>> GetAsync(Guid id);
    Task<Result<TGetDto>> CreateAsync(TCreateRequest request);
    Task<Result<TGetDto>> UpdateAsync(TUpdateRequest request);
    Task<Result<bool>> DeleteAsync(Guid id);
}
