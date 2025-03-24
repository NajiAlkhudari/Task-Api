using System.Text;
using AutoMapper;
using TaskAlbayan.Common;
using TaskAlbayan.Common.Dtos;
using TaskAlbayan.Common.Exceptions;
using TaskAlbayan.DB.Models;
using TaskAlbayan.Repository;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Service;

public class UserService
{
    private readonly UserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(UserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        return _mapper.Map<List<UserDto>>(await _repository.GetAllAsync());
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user != null)
        {
            return _mapper.Map<UserDto>(user);
        }
        else
        {
            throw new BaseException("User Not Found", 404, new ErrorResponse("User Not Found", 404, "User Not Found"));
        }
    }

    public async Task<UserDto> RegisterAsync(CreateUpdateUserDto createUserDto)
    {
        // التحقق من وجود البريد الإلكتروني
        var existingUser = await _repository.GetByEmailAsync(createUserDto.Email);
        if (existingUser != null)
        {
            throw new BaseException("Email Already Exists", 400, new ErrorResponse("Email Already Exists", 400, "Email Already Exists"));
        }

        var user = _mapper.Map<User>(createUserDto);
        user.PasswordHash = HashPassword(createUserDto.Password);

        var addedUser = await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();

        return _mapper.Map<UserDto>(addedUser);
    }

    public async Task<UserDto> UpdateAsync(Guid id, CreateUpdateUserDto updateUserDto)
    {
        var existingUser = await _repository.GetByIdAsync(id);
        if (existingUser == null)
        {
            throw new BaseException("User Not Found", 404, new ErrorResponse("User Not Found", 404, "User Not Found"));
        }

        _mapper.Map(updateUserDto, existingUser);
        existingUser.PasswordHash = HashPassword(updateUserDto.Password);

        var updatedUser = await _repository.UpdateAsync(existingUser);
        if (updatedUser != null)
        {
            await _repository.SaveChangesAsync();
            return _mapper.Map<UserDto>(updatedUser);
        }
        else
        {
            throw new BaseException("Failed to Update User", 500, new ErrorResponse("Failed to Update User", 500, "Internal Server Error"));
        }
    }

    public async Task<Guid> DeleteAsync(Guid id)
    {
        var deletedId = await _repository.DeleteAsync(id);
        if (deletedId != null)
        {
            await _repository.SaveChangesAsync();
            return deletedId.Value;
        }
        else
        {
            throw new BaseException("User Not Found", 404, new ErrorResponse("User Not Found", 404, "User Not Found"));
        }
    }

    private string HashPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        return HashPassword(password) == passwordHash;
    }
}