using DoughManager.Services.Dtos.Dashboard;
using DoughManager.Services.Shared;

namespace DoughManager.Services.Interfaces;

public interface IDashboardService
{
    Task<ServiceResponse<DashboardDto>> GetDashboardAsync();
}