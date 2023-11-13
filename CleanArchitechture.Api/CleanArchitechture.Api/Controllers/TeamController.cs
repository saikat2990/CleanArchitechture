using CleanArchitechture.Api.Controllers.Base;
using CleanArchitechture.Api.Extensions;
using CleanArchitechture.Core.Const;
using CleanArchitechture.Core.Dtos;
using CleanArchitechture.Core.Enums;
using CleanArchitechture.Core.Interfaces.Repositories;
using CleanArchitechture.Core.Interfaces.Services;
using CleanArchitechture.Core.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitechture.Api.Controllers;

[Authorize(Roles = $"{RoleConsts.Admin}")]
public class TeamController : BaseApiController
{
    private readonly ITeamService _teamService;
    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpPost("CreateTeam")]
    public async Task<ActionResult<TeamDto>> CreateTeam(TeamDto teamDto)
    {
        var team = await _teamService.Create(teamDto);
        return Ok(team);
    }
}