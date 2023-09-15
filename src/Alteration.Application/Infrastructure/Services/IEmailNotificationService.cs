namespace Alteration.Application.Infrastructure.Services
{
    public interface IEmailNotificationService
    {
        Task NotifyCustomerAsync(Guid alterationFormId, CancellationToken cancellationToken = default);
    }
}
