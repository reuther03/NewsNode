using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsNode.Services.Notifications.Database;
using NewsNode.Services.Notifications.Hubs;
using NewsNode.Shared.Abstractions.Services;
using NewsNode.Shared.Infrastructure.Services;

namespace NewsNode.Services.Notifications.Notifications;

public class SendNotificationsJob : BackgroundService
{
    private const string ReceiveNotification = nameof(ReceiveNotification);

    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHubConnectionService _notificationsConnectionService;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger<SendNotificationsJob> _logger;

    public SendNotificationsJob(IServiceScopeFactory serviceScopeFactory, IHubConnectionService notificationsConnectionService,
        IHubContext<NotificationHub> hubContext, ILogger<SendNotificationsJob> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _notificationsConnectionService = notificationsConnectionService;
        _hubContext = hubContext;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await SendFollowNotification(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(120), stoppingToken);
        }
    }

    private async Task SendFollowNotification(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NotificationsDbContext>();

        var connectedUsersIds = _notificationsConnectionService.GetActiveConnectionsUserIds();

        var notifications = await context.Notifications
            .Where(x => connectedUsersIds.Contains(x.ReceiverId) &&
                x.Status == NotificationStatus.Pending || x.Status == NotificationStatus.Failed)
            .OrderBy(x => x.CreatedAt)
            .Take(100)
            .ToListAsync(cancellationToken);

        if (notifications.Count == 0)
            return;

        foreach (var notification in notifications)
        {
            try
            {
                _logger.LogInformation("Sending notification {Id} to {ReceiverId}", notification.Id, notification.ReceiverId);
                await _hubContext.Clients.User(notification.ReceiverId.ToString())
                    .SendAsync(ReceiveNotification, notification.Message, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send notification {Id} to {ReceiverId}, {Error}", notification.Id, notification.ReceiverId, ex.Message);
                notification.MarkAsFailed();
                continue;
            }

            _logger.LogInformation("Notification {Id} sent to {ReceiverId}", notification.Id, notification.ReceiverId);
            notification.MarkAsSent();
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    // private async Task SendPostNotification(CancellationToken cancellationToken)
    // {
    //     using var scope = _serviceScopeFactory.CreateScope();
    //     var context = scope.ServiceProvider.GetRequiredService<NotificationsDbContext>();
    //
    //     var connectedUsersIds = _notificationsConnectionManager.GetActiveConnectionsUserIds();
    // }
}