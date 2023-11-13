using CleanArchitechture.Core.Dtos;

namespace CleanArchitechture.Core.Interfaces.Services;

public interface IAccountService
{
    Task CreateUser(UserCreateDto model);
}