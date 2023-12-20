using TheXDS.Triton.Models;

namespace TheXDS.Ivie.Models.Local;

public class LocalSession
{
    public string DisplayName { get; init; }

    public DateTime LogonTimestamp { get; init; }

    public Guid RemoteSessionId { get; init; }

    public static implicit operator LocalSession(Session session)
    {
        return new()
        {
            DisplayName = session.Credential.Username,
            LogonTimestamp = session.Timestamp,
            RemoteSessionId = session.Id
        };
    }
}
