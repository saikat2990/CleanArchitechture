namespace CleanArchitechture.Core.DBEntities;

public class BaseEntity<T>
{
    public T Id { get; set; }
}
public class BaseEntity : BaseEntity<int> { }