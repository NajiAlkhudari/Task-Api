using Microsoft.AspNetCore.Mvc;
using TaskAlbayan.Common;
using TaskAlbayan.Common.Dtos;
using TaskAlbayan.Service;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VisitTaskController : ControllerBase
{
    private readonly VisitTaskService _service;

    public VisitTaskController(VisitTaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<HttpResponse<List<VisitTaskDto>>> GetAll()
    {
        return new HttpResponse<List<VisitTaskDto>>(await _service.GettAll());

    }
    [HttpGet("{id}")]
    public async Task<HttpResponse<VisitTaskDto>> GetById(Guid id)
    {
        return new HttpResponse<VisitTaskDto>(await _service.GetById(id));
    }

    [HttpPost]
    public async Task<HttpResponse<VisitTaskDto>> AddAsync(CreateUpdateVisitTaskDto createUpdateVisitTaskDto)
    {
        return new HttpResponse<VisitTaskDto>(await _service.AddAsync(createUpdateVisitTaskDto));
    }

    [HttpPut("{id}")]
    public async Task<HttpResponse<VisitTaskDto>>UpdateAsync(Guid id, CreateUpdateVisitTaskDto createUpdateVisitTaskDto)
    {
        return new HttpResponse<VisitTaskDto>(await _service.UpdateAsync(id, createUpdateVisitTaskDto));

    }


    [HttpDelete("{id}")]
    public async Task<HttpResponse<Guid>> DeleteAsync(Guid id)
    {
        return new HttpResponse<Guid>(await _service.DeleteAsync(id));

    }

}
