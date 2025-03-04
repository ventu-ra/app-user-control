using Communication.Response;
using Sistema.API.Infrastructure.DataAccess;

namespace Sistema.API.UseCase.Users;

public class GetAllUsersUseCase
{
    public ResponseAllUsersJson Execute()
    {
        var dbContext = new AppDbContext();
        var users = dbContext.Users.ToList();
        
        return new ResponseAllUsersJson
        {
            Users = users.Select(user => new ResponseUserJson
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                CPF = user.CPF,
                Endereco = user.Endereco,
                Telefone = user.Telefone,
                
            }).ToList()
        };
    }
}