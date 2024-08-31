using System.Linq.Expressions;
using Assignment_PRN.Entities;
using Assignment_PRN.Models;

namespace Assignment_PRN.Repositories;

public interface ICommentRepository : IRepositoryBase<Comment, int>
{
    void RemoveById(int commentId);
    public Page<CommentList> GetAllBy(PageRequest<Comment> commentPageNo, Expression<Func<Comment, bool>>? predicate);
    Page<CommentItem> GetByFilmId(int filmId, PageRequest<Comment> pageRequest);
}