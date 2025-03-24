using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using TaskAlbayan.Common.Dtos;
using TaskAlbayan.Common.Exceptions;
using TaskAlbayan.DB;
using TaskAlbayan.Repository;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Service;


public class VisitTaskService
{
    private readonly VisitTaskRepository _repository;
    private readonly IMapper _mapper;

    public VisitTaskService(VisitTaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<VisitTaskDto>> GettAll()
    {
        return _mapper.Map<List<VisitTaskDto>>(await _repository.GetAll());
    }

    public async Task<VisitTaskDto> GetById(Guid id)
    {
        var res = await _repository.GetById(id);
        if (res != null)

            return _mapper.Map<VisitTaskDto>(res);

        else
            throw new BaseException("VisiDetails Not Found", 404, new ErrorResponse("VisiDetails Not Found", 404, "VisiDetails Not Found"));

    }

    public async Task<VisitTaskDto> AddAsync(CreateUpdateVisitTaskDto createUpdateVisitTaskDto)
    {
        var details = _mapper.Map<VisitTask>(createUpdateVisitTaskDto);
        var res = await _repository.AddAsync(details);

        try
        {
            await _repository.SaveChangesAsync();
            return _mapper.Map<VisitTaskDto>(res);
        }
        catch (Exception e)
        {

            throw new BaseException("Unhandeled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));

        }
    }

    public async Task<VisitTaskDto> UpdateAsync(Guid id, CreateUpdateVisitTaskDto createUpdateVisitTaskDto)
    {
        var exstingDetails = await _repository.GetById(id);
        if (exstingDetails != null)
        {
            _mapper.Map(createUpdateVisitTaskDto, exstingDetails);

            try
            {
                var res = await _repository.UpdateAsync(exstingDetails);
                await _repository.SaveChangesAsync();
                return _mapper.Map<VisitTaskDto>(res);

            }
            catch (Exception e)
            {

                throw new BaseException("Unhandled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));
            }
        }
        else
        {
            throw new BaseException("VisitDetails Not Found", 404, new ErrorResponse("VisitDetails Not Found", 404, "VisitDetails Not Found"));
        }
    }

    public async Task<Guid> DeleteAsync(Guid id)
    {
        try
        {
            var res = await _repository.DeleteAsync(id);
            if (res != null)
            {
                await _repository.SaveChangesAsync();

                return res.Value;
            }
            else
                throw new BaseException("VisitDetails Not Found", 404, new ErrorResponse("VisitDetails Not Found", 404, "VisitDetails Not Found"));
        }
        catch (Exception e)
        {
            throw new BaseException("Unhandeled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));
        }
    }

}