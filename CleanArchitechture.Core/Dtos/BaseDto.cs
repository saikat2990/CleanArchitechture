namespace CleanArchitechture.Core.Dtos;

public class BaseDto<T>
{
    public T Id { get; set; }
}

//donot add properties/fields/methods to this class. Do that in the above class.
public class BaseDto:BaseDto<int>{ }