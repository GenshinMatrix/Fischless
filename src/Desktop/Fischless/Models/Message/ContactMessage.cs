namespace Fischless.Models.Message;

public sealed class ContactMessage
{
    public ContactMessageType Type { get; set; } = ContactMessageType.Added;
    public Contact Contact { get; set; } = null!;
}

public enum ContactMessageType
{
    Added,
    Removed,
    Edited,
}
