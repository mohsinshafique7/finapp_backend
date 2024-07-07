using finapp_backend.Helpers;
using finapp_backend.Models;

namespace finapp_backend.Interfaces;
public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment commentModel);
    Task<Comment?> UpdateAsync(Comment commentModel, int id);
    Task<Comment?> DeleteAsync(int id);
}