using finapp_backend.Data;
using finapp_backend.Helpers;
using finapp_backend.Interfaces;
using finapp_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace finapp_backend.Repository;
public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;
    public CommentRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        if (commentModel == null)
        {
            return null;
        }
        _context.Comments.Remove(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
    {
        var comment = _context.Comments.Include(c => c.AppUser).AsQueryable();
        if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
        {
            comment = comment.Where(s => s.Stock.Symbol.Contains(queryObject.Symbol));
        }
        if (queryObject.IsDescending)
        {
            comment = comment.OrderByDescending(c => c.CreatedOn);
        }
        return await comment.ToListAsync();

    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.Include(x => x.AppUser).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment?> UpdateAsync(Comment commentModel, int id)
    {
        var exists = await _context.Comments.FindAsync(id);
        if (exists == null)
        {
            return null;
        }
        exists.Title = commentModel.Title;
        exists.Content = commentModel.Content;
        await _context.SaveChangesAsync();
        return commentModel;
    }
}