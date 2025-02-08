namespace NewsNode.Shared.Abstractions.Services;

public interface IHubConnectionService
{
    void Connect(string connectionId, string userId);
    void Disconnect(string connectionId, string userId);
    List<Guid> GetActiveConnectionsUserIds();
}