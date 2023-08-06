using Application.Common.interfaces.Enum;

namespace Application.Consumer.Models;

public class BaseModel
{
    public string Id { get; set; }
    public string Type { get; set; }
    public EntityChangeEventType EntityChangeEventType { get; set; }
}