namespace Alteration.Application.Infrastructure.Services
{
    internal class EmailNotificationService : IEmailNotificationService
    {
        public async Task NotifyCustomerAsync(Guid alterationFormId, CancellationToken cancellationToken = default)
        {
        }
    }
}
