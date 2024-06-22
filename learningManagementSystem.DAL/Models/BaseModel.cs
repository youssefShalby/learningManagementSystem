

namespace learningManagementSystem.DAL.Models;

public class BaseModel<T>
{
    public T Id { get; set; } = default!;
}
