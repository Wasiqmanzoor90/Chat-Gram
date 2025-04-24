using Server.Domain.Model;

namespace Server.Application.Interface
{
    public interface IJToken
    {
        string GenrateToken(UserDetail user);
    }
}
