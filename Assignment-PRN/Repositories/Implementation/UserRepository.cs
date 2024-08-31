using AutoMapper.QueryableExtensions;
using Assignment_PRN.Entities;
using Assignment_PRN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Assignment_PRN.Data;
using System.Linq.Expressions;

namespace Assignment_PRN.Repositories.Implementation;
public class UserRepository : RepositoryBase<User, string>, IUserRepository
{
    public UserRepository(AssignmentPRNContext appDbContext) : base(appDbContext)
    {
    }

    public User? GetByIdWithFavoriteFilms(string userId)
    {
        return DbSet.Include(u => u.FavoriteFilms).FirstOrDefault(u => u.Id == userId);
    }

    public Page<UserList> GetUserList(PageRequest<User> pageRequest, string? search, int? page)
    {
        var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;

        // Truy vấn dữ liệu cơ sở dữ liệu không có điều kiện lọc
        var rawData = DbSet
            .Include(u => u.Comments)
            .GroupJoin(
                DbContext.Set<IdentityUserRole<string>>(),
                user => user.Id,
                userRole => userRole.UserId,
                (user, userRoles) => new { User = user, UserRoles = userRoles })
            .SelectMany(
                x => x.UserRoles.DefaultIfEmpty(),
                (x, userRole) => new { x.User, RoleId = userRole != null ? userRole.RoleId : null })
            .GroupJoin(
                DbContext.Set<IdentityRole>(),
                x => x.RoleId,
                role => role.Id,
                (x, roles) => new { x.User, Roles = roles })
            .SelectMany(
                x => x.Roles.DefaultIfEmpty(),
                (x, role) => new { x.User, RoleName = role != null ? role.Name : null })
            .Where(x => x.RoleName != "Admin" || x.RoleName == null)
            .AsQueryable(); 
        if (!string.IsNullOrEmpty(search))
        {
            rawData = rawData
                .Where(x => x.User.UserName.Contains(search)
                            || x.User.Email.Contains(search));
        }

        var totalElement = rawData.Count();

        var data = rawData
            //.OrderBy(pageRequest.Sort ?? "Id desc")
            .Skip(skip).Take(pageRequest.Size)
            .Select(u => new UserList
            {
                Id = u.User.Id,
                UserName = u.User.UserName,
                Email = u.User.Email,
                PhoneNumber = u.User.PhoneNumber,
                LockoutEnabled = u.User.LockoutEnabled,
                totalComment = u.User.Comments.Count
            });

        return new Page<UserList>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = totalElement,
            Data = totalElement == 0 ? new List<UserList>() : data.ToList()
        };
    }


    //public User GetUserById(string userId)
    //{
    //    return DbSet.FirstOrDefault(u => u.Id == userId);
    //}
    public void DisableUserLockout(User user)
    {
        DbSet.Update(user);
        DbContext.SaveChanges();
    }

    public void EnableUserLockout(User user)
    {
        DbSet.Update(user);
        DbContext.SaveChanges();
    }
}