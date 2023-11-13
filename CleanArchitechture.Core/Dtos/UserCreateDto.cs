using AutoMapper;
using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Interfaces.Common;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitechture.Core.Dtos;

public class UserCreateDto:IMapFrom<AppUser>
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
   
}