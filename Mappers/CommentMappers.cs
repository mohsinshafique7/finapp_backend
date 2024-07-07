using finapp_backend.Dtos.Comment;
using finapp_backend.Models;

namespace finapp_backend.Mappers;
public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            CreatedBy = comment.AppUser.UserName,
            StockId = comment.StockId
        };
    }
    public static Comment ToCommentFromCreateDto(this CreateCommentDto commentDto, int stockId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            StockId = stockId
        };
    }
    public static Comment ToCommentFromUpdateDto(this UpdateCommentRequestDto commentDto)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
        };
    }
}