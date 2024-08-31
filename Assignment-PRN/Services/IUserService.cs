using Assignment_PRN.Models;

namespace Assignment_PRN.Services
{
    public interface IUserService
    {
        Page<UserList> GetUserList(string? search, int? pageNo);

        bool DisableUserLockout(string userId);

        bool EnableUserLockout(string userId);
    }
}
