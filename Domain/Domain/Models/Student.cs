using MongoDB.Bson.Serialization.Attributes;
namespace Domain.Models;

public class Student
{
   [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }

    public string GuidId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Birthday { get; set; }
    public string Course { get; set; }
    public string Group { get; set; }
}