using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskAlbayan.Common;
using TaskAlbayan.Common.Exceptions;
using TaskAlbayan.DB;
using TaskAlbayan.Service;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VisitController : ControllerBase
{
    private readonly VisitService _service;
    private readonly BaseService _baseService;

    public VisitController(VisitService service, BaseService baseService)
    {
        _service = service;
        _baseService = baseService;
    }
    [HttpGet]
    [Authorize]
    public async Task<HttpResponse<List<VisitDto>>> GetAll()
    {
                if (_baseService.CheckUserRole(UserRoles.Admin))

        return new HttpResponse<List<VisitDto>>(await _service.GettAllAsync());
                else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));

    }
    [HttpGet("{id}")]
    [Authorize]

    public async Task<HttpResponse<VisitDto>> GetById(Guid id)
    {        if (_baseService.CheckUserRole(UserRoles.Admin))

        return new HttpResponse<VisitDto>(await _service.GetById(id));
                else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));
    }

    [HttpPost]
    [Authorize]

    public async Task<HttpResponse<VisitDto>> AddAsync(CreateUpdateVisitDto createUpdateVisitDto)
    {        if (_baseService.CheckUserRole(UserRoles.User))

        return new HttpResponse<VisitDto>(await _service.AddAsync(createUpdateVisitDto));
                else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));
    }

    [HttpPut("{id}")]
    [Authorize]

    public async Task<HttpResponse<VisitDto>> UpdateAsync(Guid id, CreateUpdateVisitDto createUpdateVisitDto)
    {        if (_baseService.CheckUserRole(UserRoles.Admin))

        return new HttpResponse<VisitDto>(await _service.UpdateAsync(id, createUpdateVisitDto));
                else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));

    }


    [HttpDelete("{id}")]
    [Authorize]

    public async Task<HttpResponse<Guid>> DeleteAsync(Guid id)
    {        if (_baseService.CheckUserRole(UserRoles.Admin))

        return new HttpResponse<Guid>(await _service.DeleteAsync(id));
                else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));

    }

}
