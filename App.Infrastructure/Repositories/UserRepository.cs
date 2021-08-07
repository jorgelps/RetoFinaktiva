using App.Infrastructure.Base;
using App.Infrastructure.Database.Context;
using App.Infrastructure.Database.Entities;

namespace App.Infrastructure.Repositories
{
    public interface IUserRepository : IBaseRepository<Users>
    {

    }

    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public UserRepository(DataContext database) : base(database)
        {

        }
    }
}
