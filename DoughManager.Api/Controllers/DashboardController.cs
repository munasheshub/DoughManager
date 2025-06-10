using DoughManager.Services.Dtos.Dashboard;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DoughManager.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<ServiceResponse<DashboardDto>>> GetAll()
    {
        var response = await _dashboardService.GetDashboardAsync();
        return Ok(response);
    }
}