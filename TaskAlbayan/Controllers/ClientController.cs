using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskAlbayan.Common;
using TaskAlbayan.Common.Exceptions;
using TaskAlbayan.Service;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Controllers;


[ApiController]
[Route("api/[controller]")]

public class ClientController : ControllerBase
{
    private readonly ClientService _service;
    private readonly BaseService _baseService;

    public ClientController(ClientService service, BaseService baseService)
    {
        _service = service;
        _baseService = baseService;
    }

    [HttpGet]
    [Authorize]
    public async Task<HttpResponse<List<ClientDto>>> GetAll()
    {
        if (_baseService.CheckUserRole("Admin"))
            return new HttpResponse<List<ClientDto>>(await _service.GetAllAsync());
        else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));
    }

    [HttpGet("{id}")]
    public async Task<HttpResponse<ClientDto>> GetById(Guid id)
    {
        return new HttpResponse<ClientDto>(await _service.GetById(id));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<HttpResponse<ClientDto>> AddAsync(CreateUpdateClientDto createUpdateClientDto)
    {
        return new HttpResponse<ClientDto>(await _service.AddAsync(createUpdateClientDto));

    }

    [HttpPut("{id}")]
    public async Task<HttpResponse<ClientDto>> UpdateAsync(Guid id, CreateUpdateClientDto createUpdateClientDto)
    {
        return new HttpResponse<ClientDto>(await _service.UpdateAsync(id, createUpdateClientDto));
    }

    [HttpDelete("{id}")]
    public async Task<HttpResponse<Guid>> DeleteAsync(Guid id)
    {
        return new HttpResponse<Guid>(await _service.DeleteAsync(id));

    }

}