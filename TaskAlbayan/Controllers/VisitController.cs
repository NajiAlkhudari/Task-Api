using Microsoft.AspNetCore.Mvc;
using TaskAlbayan.Common;
using TaskAlbayan.Service;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VisitController : ControllerBase
{
    private readonly VisitService _service;

    public VisitController(VisitService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<HttpResponse<List<VisitDto>>> GetAll()
    {
        return new HttpResponse<List<VisitDto>>(await _service.GettAllAsync());

    }
    [HttpGet("{id}")]
    public async Task<HttpResponse<VisitDto>> GetById(Guid id)
    {
        return new HttpResponse<VisitDto>(await _service.GetById(id));
    }

    [HttpPost]
    public async Task<HttpResponse<VisitDto>> AddAsync(CreateUpdateVisitDto createUpdateVisitDto)
    {
        return new HttpResponse<VisitDto>(await _service.AddAsync(createUpdateVisitDto));
    }

    [HttpPut("{id}")]
    public async Task<HttpResponse<VisitDto>>UpdateAsync(Guid id, CreateUpdateVisitDto createUpdateVisitDto)
    {
        return new HttpResponse<VisitDto>(await _service.UpdateAsync(id, createUpdateVisitDto));

    }


    [HttpDelete("{id}")]
    public async Task<HttpResponse<Guid>> DeleteAsync(Guid id)
    {
        return new HttpResponse<Guid>(await _service.DeleteAsync(id));

    }

}
