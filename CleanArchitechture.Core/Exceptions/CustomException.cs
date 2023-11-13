using CleanArchitechture.Core.Enums;

namespace CleanArchitechture.Core.Exceptions;

public class CustomException : Exception
{
    private const string DefaultMessage = "An Error occured.";
    public List<string> Errors { get; } = null!;

    public MessageDisplayType MessageDisplayType { get; set; }

    public CustomException() : base(DefaultMessage)
    {
    }

    public CustomException(string message) : base(message)
    {
    }

    public CustomException(string message, MessageDisplayType messageDisplayType) : base(message)
    {
        MessageDisplayType = messageDisplayType;
    }

    public CustomException(string message, List<string> errors) : base(message)
    {
        Errors = new List<string>();
        foreach (var error in errors)
        {
            Errors.Add(error);
        }
    }

    public CustomException(string message, Exception innerException) : base(message, innerException)
    {
    }
}