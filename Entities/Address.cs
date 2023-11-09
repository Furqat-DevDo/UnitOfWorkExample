using System.Text.Json.Serialization;

namespace UnitOfWorkExample.Entities;

public class Address
{
    public int Id { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore]
    public virtual User?  User { get; set; }
}