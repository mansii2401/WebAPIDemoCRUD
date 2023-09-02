using System.Text.Json.Serialization;

namespace WebAPIDemo.Entities.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Student,
        Employee,

    }
}
