using AutoMapper;
using TaskAlbayan.Common;
using TaskAlbayan.Common.Dtos;
using TaskAlbayan.Common.Exceptions;
using TaskAlbayan.DB;
using TaskAlbayan.Repository;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Service;

public class VisitService
{
    private readonly VisitRepository _repository;
    private readonly VisitTaskRepository _visitTaskRepository;
    private readonly IMapper _mapper;

    public VisitService(VisitRepository repository, VisitTaskRepository visitTaskRepository ,IMapper mapper)
    {
        _repository = repository;
        _visitTaskRepository=visitTaskRepository;
        _mapper = mapper;
    }

    public async Task<List<VisitDto>> GettAllAsync()
    {
        return _mapper.Map<List<VisitDto>>(await _repository.GetAll());
    }

    public async Task<VisitDto> GetById(Guid id)
    {
        var visit = await _repository.GetById(id);
        if (visit != null)

            return _mapper.Map<VisitDto>(visit);

        else
            throw new BaseException("Visit Not Found", 404, new ErrorResponse("Visit Not Found", 404, "Client Not Found"));
    }

    // public async Task<VisitDto> AddAsync(CreateUpdateVisitDto createUpdateVisitDto)
    // {
    //     var res = await _repository.AddAsync(_mapper.Map<Visit>(createUpdateVisitDto));

    //     try
    //     {
    //         await _repository.SaveChangesAsync();
    //         return _mapper.Map<VisitDto>(res);
    //     }
    //     catch (Exception e)
    //     {

    //         throw new BaseException("Unhandeled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));
    //     }
    // }


public async Task<VisitDto> AddAsync(CreateUpdateVisitDto createUpdateVisitDto)
{
    try
    {
        var visit = _mapper.Map<Visit>(createUpdateVisitDto);

        var res = await _repository.AddAsync(visit);

        await _repository.SaveChangesAsync(); 

        foreach (var taskDto in createUpdateVisitDto.TaskDto)
        {
            var visitTask = new VisitTask
            {
                VisitId = res.Id, 
                TaskId = taskDto.TaskId,
                Notes = taskDto.Notes
            };

            await _visitTaskRepository.AddAsync(visitTask);
        }

        await _visitTaskRepository.SaveChangesAsync();

        var visitDto = _mapper.Map<VisitDto>(res);
        visitDto.visitTasks = res.visitTasks.Select(vt => _mapper.Map<VisitTaskDto>(vt)).ToList();

        return visitDto;
    }
    catch (Exception e)
    {
        throw new BaseException("Unhandled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));
    }
}


    public async Task<VisitDto> UpdateAsync(Guid id, CreateUpdateVisitDto createUpdateVisitDto)
    {
        var exstingVisit = await _repository.GetById(id);
        if (exstingVisit != null)
        {
            _mapper.Map(createUpdateVisitDto, exstingVisit);

            try
            {
                var res = await _repository.UpdateAsync(exstingVisit);
                await _repository.SaveChangesAsync();
                return _mapper.Map<VisitDto>(res);
            }
            catch (Exception e)
            {
                throw new BaseException("Unhandled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));
            }
        }
        else
        {
            throw new BaseException("Visit Not Found", 404, new ErrorResponse("Visit Not Found", 404, "Visit Not Found"));
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
                throw new BaseException("Visit Not Found", 404, new ErrorResponse("Visit Not Found", 404, "Visit Not Found"));
        }
        catch (Exception e)
        {
            throw new BaseException("Unhandeled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));
        }
    }
}









