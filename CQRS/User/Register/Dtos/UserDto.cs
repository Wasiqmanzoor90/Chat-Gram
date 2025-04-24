using MongoDB.Bson;

namespace Server.CQRS.User.Register.Dtos
{
    public class UserDto
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
