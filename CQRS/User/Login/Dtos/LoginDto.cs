using MongoDB.Bson;

namespace Server.CQRS.User.Login.Dtos
{
    public class LoginDto
    {
       public ObjectId Id { get; set; }
        public string Name { get; set; }
       public string Email { get; set; } = string.Empty;
       public string Password { get; set; } = string.Empty;
       public string Token { get; set; } = string.Empty;  // Add Token property

    }
}
