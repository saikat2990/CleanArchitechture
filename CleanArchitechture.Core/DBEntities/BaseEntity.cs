namespace CleanArchitechture.Core.DBEntities;

public class BaseEntity<T>
{
    public T Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}
public class BaseEntity : BaseEntity<int> { }