using AutoMapper;
using DoughManager.Data.DbContexts;
using DoughManager.Data.EntityModels;
using DoughManager.Services.Dtos.Discrepancy;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.EntityFrameworkCore;

namespace DoughManager.Services.Services;

public class DiscrepancyService : IDiscrepancyService
{
    private readonly DoughManagerDbContext _context;
    private readonly IMapper _mapper;

    public DiscrepancyService(DoughManagerDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<DiscrepancyStatus>> GetTodayDiscrepancies()
    {
        try
        {
            var today = DateTime.Today;
            var discrepancies = await _context.DiscrepancyRecords
                .Where(dr => dr.CreationTime.Value.Date == today.Date)
                .OrderBy(dr => dr.CreationTime.Value.Date)
                .ToListAsync();
            
            var items = _mapper.Map<List<DiscrepancyRecordDto>>(discrepancies);
            
            var status = new DiscrepancyStatus
            {
                Date = today,
                MorningShiftRecord = items.FirstOrDefault(d => d.Shift == ShiftType.Morning) ?? null,
                AfternoonShiftRecord = items.FirstOrDefault(d => d.Shift == ShiftType.Afternoon) ?? null,
                AllRecords = items
            
            };
            return ServiceResponse<DiscrepancyStatus>.Success(status);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ServiceResponse<DiscrepancyRecordDto>> RecordShiftDiscrepancy(DiscrepancyRecordDto request, Account account)
    {
        try
        {
            var newRecord = _mapper.Map<DiscrepancyRecord>(request);
            newRecord.PrepareEntityForCreation(account);
            await _context.DiscrepancyRecords.AddAsync(newRecord);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<DiscrepancyRecordDto>(newRecord);
            return ServiceResponse<DiscrepancyRecordDto>.Success(response);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<ServiceResponse<List<DiscrepancyRecordDto>>> GetDiscrepancyHistory()
    {
        throw new NotImplementedException();
    }
}