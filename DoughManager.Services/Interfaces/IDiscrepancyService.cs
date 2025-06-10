using DoughManager.Data.EntityModels;
using DoughManager.Services.Dtos.Discrepancy;
using DoughManager.Services.Shared;

namespace DoughManager.Services.Interfaces;

public interface IDiscrepancyService
{
    Task<ServiceResponse<DiscrepancyStatus>> GetTodayDiscrepancies();
    Task<ServiceResponse<DiscrepancyRecordDto>> RecordShiftDiscrepancy(DiscrepancyRecordDto request, Account account);
    Task<ServiceResponse<List<DiscrepancyRecordDto>>> GetDiscrepancyHistory();
}