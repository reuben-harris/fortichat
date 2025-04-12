namespace FortiChat.Models;

public class ChatMessage
{
    public int ChatMessageId { get; set; }
    public string UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Message { get; set; }
}