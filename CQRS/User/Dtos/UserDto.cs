using MongoDB.Bson;

namespace Server.CQRS.User.Dtos
{
    public class UserDto
    {
        public ObjectId UserId {  get; set; }
        public string Name {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
