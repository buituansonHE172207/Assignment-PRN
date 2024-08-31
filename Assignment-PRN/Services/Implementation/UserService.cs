using Assignment_PRN.Data;
using Assignment_PRN.Entities;
using Assignment_PRN.Models;
using Assignment_PRN.Repositories;
using System.Linq.Expressions;

namespace Assignment_PRN.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Page<UserList> GetUserList(string? search, int? pageNo)
        {
            var pageRequest = new PageRequest<User>
            {
                PageNumber = pageNo ?? 1,
                Size = 10,
                Sort = ""
            };

            // x => x.RoleName != "Admin" || x.RoleName == null
            return _userRepository.GetUserList(pageRequest, search, pageNo);
        }

        public bool DisableUserLockout(string userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null) return false;

            user.LockoutEnabled = false;
            _userRepository.DisableUserLockout(user);
            return true;
        }

        public bool EnableUserLockout(string userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null) return false;

            user.LockoutEnabled = true;
            _userRepository.EnableUserLockout(user);
            return true;
        }
    }
}