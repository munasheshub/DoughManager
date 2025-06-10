using DoughManager.Data.EntityModels;
using DoughManager.Services.Dtos.StockTake;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Services;
using DoughManager.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DoughManager.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StockController(IStockTakeService _service) : BaseController
{
    [HttpGet("afternoonStock")]
    public async Task<ActionResult<ServiceResponse<StockTakeDto>>> MorningStock([FromQuery] int productId, [FromQuery] double quantityOnHand)
    {
        var response = await _service.RecordTwoPmStockTakeAsync(productId, quantityOnHand);
        return Ok(response);
    }
    
    [HttpGet("eveningStock")]
    public async Task<ActionResult<ServiceResponse<StockTakeDto>>> EveningStock([FromQuery] int productId, [FromQuery] double quantityOnHand)
    {
        var response = await _service.RecordNinePmStockTakeAsync(productId, quantityOnHand);
        return Ok(response);
    }
    
    [HttpGet("getTodayStock/{productId:int}")]
    public async Task<ActionResult<ServiceResponse<StockTakeDto>>> TodayStock(int productId)
    {
        var response = await _service.GetTodaysStockTakeAsync(productId);
        return Ok(response);
    }
    
    [HttpGet("getAllTodayStock")]
    public async Task<ActionResult<ServiceResponse<StockTakeDto>>> GetAllTodayStock()
    {
        var response = await _service.GetTodayStockTakeAsync();
        return Ok(response);
    }
    
    [HttpGet("checkMorningShiftStatus")]
    public async Task<ActionResult<ServiceResponse<StockTakeStatus>>> CheckMorningShiftStatus()
    {
        var response = await _service.CheckStockTakeStatus();
        return Ok(response);
    }
    
    [HttpGet("closeshift")]
    public async Task<ActionResult<ServiceResponse<bool>>> CloseShift([FromQuery] StockTakee status, [FromQuery] bool open)
    {
        var response = await _service.CloseStockTake(status, open);
        return Ok(response);
    }
}