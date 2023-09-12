using Fischless.ViewModels;

namespace Fischless.Models.Message;

public class ContactMessage
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
