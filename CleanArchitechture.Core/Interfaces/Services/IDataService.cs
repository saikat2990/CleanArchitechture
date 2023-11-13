namespace CleanArchitechture.Core.Interfaces.Services;
public interface IDataService
{
    Task<bool> SeedData();
}