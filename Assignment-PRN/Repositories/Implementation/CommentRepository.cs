using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Assignment_PRN.Entities;
using Assignment_PRN.Models;
using Microsoft.EntityFrameworkCore;
using Assignment_PRN.Data;
using AutoMapper;

namespace Assignment_PRN.Repositories.Implementation;

public class CommentRepository : RepositoryBase<Comment, int>, ICommentRepository
{
    private readonly AutoMapper.IConfigurationProvider _mapper;

    public CommentRepository(AssignmentPRNContext appDbContext, AutoMapper.IConfigurationProvider mapper) : base(appDbContext)
    {
            _mapper = mapper;
    }

    public void RemoveById(int commentId)
    {
        var comment = new Comment { Id = commentId };
        DbSet.Remove(comment);
    }

    public Page<CommentItem> GetByFilmId(int filmId, PageRequest<Comment> pageRequest)
    {
        var rawData = DbSet
            .Include(c => c.User)
            .Where(c => c.FilmId == filmId);
        var total = rawData.Count();
        var data = rawData
            .OrderBy(pageRequest.Sort ?? "Time Desc")
            .Skip((pageRequest.PageNumber - 1) * pageRequest.Size).Take(pageRequest.Size)
            .Select(c => new CommentItem
            {
                Content = c.Content,
                Time = c.Time,
                UserName = c.User.UserName
            });
        return new Page<CommentItem>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = total,
            Data = total == 0 ? new List<CommentItem>() : data.ToList(),
            Sort = pageRequest.Sort
        };
    }

    public Page<CommentList> GetAllBy(PageRequest<Comment> pageRequest, Expression<Func<Comment, bool>>? predicate)
    {
        var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
        var rawData = DbSet
            .Include(c => c.User)
            .Include(c => c.Film)
            .Where(predicate ?? (_ => true));
        var totalElement = rawData.Count();
        var data = rawData
            .OrderBy(pageRequest.Sort ?? "Time desc")
            .Skip(skip).Take(pageRequest.Size)
            .Select(c => new CommentList
            {
                Id = c.Id,
                UserId = c.UserId,
                FilmId = c.FilmId,
                Content = c.Content,
                Time = c.Time,
                UserName = c.User.UserName,
                FilmTitle = c.Film.Title
            });
        return new Page<CommentList>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = totalElement,
            Data = totalElement == 0 ? new List<CommentList>() : data.ToList(),
            Sort = pageRequest.Sort
        };
    }
}