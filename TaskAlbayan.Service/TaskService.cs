using AutoMapper;
using TaskAlbayan.Common.Exceptions;
using TaskAlbayan.Repository;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Service;


public class TaskService
{
    private readonly TaskRepository _repository;
    private readonly IMapper _mapper;

    public TaskService(TaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<TaskDto>> GetAllAsync()
    {
        return _mapper.Map<List<TaskDto>>(await _repository.GetAll());
    }

    public async Task<TaskDto> GetById(Guid id)
    {
        var res = await _repository.GetById(id);
        if (res != null)

            return _mapper.Map<TaskDto>(res);

        else
            throw new BaseException("Task Not Found", 405, new ErrorResponse("Task Not Found", 405, "Task Not Found"));
    }

    public async Task<TaskDto> AddAsync(CreateUpdateTaskDto createUpdateTaskDto)
    {
        var task = _mapper.Map<Task>(createUpdateTaskDto);
        var res = await _repository.AddAsync(task);

        try
        {
            await _repository.SaveChangesAsync();
            return _mapper.Map<TaskDto>(res);
        }
        catch (Exception e)
        {
            throw new BaseException("Unhandeled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));

        }
    }

    public async Task<TaskDto> UpdateAsync(Guid id, CreateUpdateTaskDto createUpdateTaskDto)
    {
        var exstingTask = await _repository.GetById(id);
        if (exstingTask != null)
        {
            _mapper.Map(createUpdateTaskDto, exstingTask);
            try
            {
                var res = await _repository.UpdteAsync(exstingTask);
                await _repository.SaveChangesAsync();
                return _mapper.Map<TaskDto>(res);
            }
            catch (Exception e)
            {

                throw new BaseException("Unhandled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));

            }
        }
        else
        {
            throw new BaseException("Task Not Found", 404, new ErrorResponse("Task Not Found", 404, "Task Not Found"));
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
                throw new BaseException("Task Not Found", 404, new ErrorResponse("Task Not Found", 404, "Task Not Found"));
        }
        catch (Exception e)
        {
            throw new BaseException("Unhandeled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));
        }
    }

}