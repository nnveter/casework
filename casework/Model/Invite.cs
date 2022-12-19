using System;

namespace CaseWork.Model;

public class Invite
{
    public int id { get; set; }
    public InviteType inviteType { get; set; }
    public string? iinkHash { get; set; }
    public int inviteEntityId { get; set; }

    public bool isAccepted { get; set; } = false;
    public bool isDenied { get; set; } = false;

    public long createdAt { get; set; } = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();

    public User initiator { get; set; }
    public User iarget { get; set; }
}

public enum InviteType
{
    ToTask,
    ToCompany
}