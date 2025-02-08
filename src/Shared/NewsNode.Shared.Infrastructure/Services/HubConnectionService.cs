using System.Collections.Concurrent;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Shared.Infrastructure.Services;

public sealed class HubConnectionService : IHubConnectionService
{
    private static readonly ConcurrentDictionary<string, string> Connections = [];

    public void Connect(string connectionId, string userId)
        => Connections.TryAdd(connectionId, userId);

    public void Disconnect(string connectionId, string userId)
        => Connections.TryRemove(new KeyValuePair<string, string>(connectionId, userId));

    public List<Guid> GetActiveConnectionsUserIds()
    {
        var userIds = Connections.Values.Distinct();
        return userIds.Select(Guid.Parse).ToList();
    }
}