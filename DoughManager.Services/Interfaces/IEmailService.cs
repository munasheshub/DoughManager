

using DoughManager.Services.Dtos;

namespace DoughManager.Services.Interfaces
{
    public interface IEmailService
    {
        Task Send(string to, string subject, string code);
    }
}
