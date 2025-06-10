using DoughManager.Services.Dtos.Discrepancy;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DoughManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscrepancyController : BaseController
{
    private readonly IDiscrepancyService _discrepancyService;

    public DiscrepancyController(IDiscrepancyService discrepancyService)
    {
        _discrepancyService = discrepancyService;
    }

    [HttpGet("getToday")]
    public async Task<ActionResult<ServiceResponse<DiscrepancyStatus>>> GetDiscrepancyRecordsToday()
    {
        var response = await _discrepancyService.GetTodayDiscrepancies();
        return Ok(response);
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<ServiceResponse<DiscrepancyRecordDto>>> Create(DiscrepancyRecordDto dto)
    {
        var response = await _discrepancyService.RecordShiftDiscrepancy(dto, Account);
        return Ok(response);
    }
    
    
}