using Data.Dapper.Models;
using Data.Dapper.Repositories.Interfaces;

namespace Data.Dapper.Repositories
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        public UsersRepository() : base() { }
    }
}
