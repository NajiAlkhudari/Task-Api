using AutoMapper;
using TaskAlbayan.Common;
using TaskAlbayan.Common.Exceptions;
using TaskAlbayan.Repository;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Service;

public class ClientService
{
    private readonly ClientRepository _repository;
    private readonly IMapper _mapper;

    public ClientService(ClientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ClientDto>> GetAllAsync()
    {
        return _mapper.Map<List<ClientDto>>(await _repository.GetAll());
    }

    public async Task<ClientDto> GetById(Guid id)
    {
        var res = await _repository.GetById(id);
        if (res != null)
            return _mapper.Map<ClientDto>(res);
        else
            throw new BaseException("Client Not Found", 404, new ErrorResponse("Clinet Not Found", 404, "Clinet Not Found"));
    }

    public async Task<ClientDto> AddAsync(CreateUpdateClientDto createUpdateClientDto)
    {

        var client = _mapper.Map<Client>(createUpdateClientDto);
        var res = await _repository.AddAsync(client);

        try
        {
            await _repository.SaveChangesAsync();
            return _mapper.Map<ClientDto>(res);
        }
        catch (Exception e)
        {
            throw new BaseException("Unhandeled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));

        }
    }

    public async Task<ClientDto> UpdateAsync(Guid id, CreateUpdateClientDto createUpdateClientDto)
    {
        var exstingClinet = await _repository.GetById(id);
        if (exstingClinet != null)
        {
            _mapper.Map(createUpdateClientDto, exstingClinet);

            try
            {
                var res = await _repository.UpdateAsync(exstingClinet);

                await _repository.SaveChangesAsync();
                return _mapper.Map<ClientDto>(res);
            }
            catch (Exception e)
            {
                throw new BaseException("Unhandled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));
            }
        }
        else
        {
            throw new BaseException("Clinet Not Found", 404, new ErrorResponse("Clinet Not Found", 404, "Clinet Not Found"));
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
                throw new BaseException("Client Not Found", 404, new ErrorResponse("Client Not Found", 404, "Agent Not Found"));
        }
        catch (Exception e)
        {
            throw new BaseException("Unhandeled Exception", 500, new ErrorResponse(e.Message, 500, e.Data));
        }
    }
}

