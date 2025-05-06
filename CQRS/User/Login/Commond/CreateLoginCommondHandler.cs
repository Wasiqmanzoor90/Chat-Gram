using MediatR;
using MongoDB.Driver;
using Server.Application.Interface;
using Server.CQRS.User.Login.Dtos;
using Server.Data;

namespace Server.CQRS.User.Login.Commond
{
    public class CreateLoginCommondHandler : IRequestHandler<CreateLoginCommond, LoginDto>
    {
        private readonly IJToken _jtoken;
        private MongoDbService _dbService;
        public CreateLoginCommondHandler(MongoDbService dbService, IJToken jtoken)
        {
            _dbService = dbService;
            _jtoken = jtoken;
        }

        public async Task<LoginDto> Handle(CreateLoginCommond request, CancellationToken cancellationToken)
        {
           var finduser = await _dbService.Users.Find(u => u.Email == request.Email).FirstOrDefaultAsync();
            if(finduser == null)
            {
                throw new Exception("User Don't Exists!");
            }
            bool verify = BCrypt.Net.BCrypt.Verify(request.Password, finduser.Password);
            if (!verify)
            {
                throw new Exception("Invalid Password");
            }
            //Genrate Jwt Token
            var token = _jtoken.GenrateToken(finduser);

            //Return with JWT Token
            return new LoginDto
            {
                Token = token,
                Id = finduser.Id.ToString(),
                Email = finduser.Email,
                Name = finduser.Name
            };


        }
    }
}
