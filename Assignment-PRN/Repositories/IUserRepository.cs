using Assignment_PRN.Entities;
using Assignment_PRN.Models;
using System.Linq.Expressions;


namespace Assignment_PRN.Repositories;

public interface IUserRepository : IRepositoryBase<User, string>
{
    User? GetByIdWithFavoriteFilms(string userId);
    Page<UserList> GetUserList(PageRequest<User> pageRequest, string? search, int? page);
    void DisableUserLockout(User user);
    void EnableUserLockout(User user);
}
