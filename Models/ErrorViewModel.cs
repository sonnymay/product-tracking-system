namespace MySimpleWebApp.Models;

public class ErrorViewModel
{
    // The RequestId associated with the current error.
    public string RequestId { get; set; } = string.Empty;

    // Indicates whether the RequestId field should be shown.
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    // The exact UTC time when the error occurred.
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // A detailed error message.
    public string ErrorMessage { get; set; } = "An unexpected error occurred.";
}
