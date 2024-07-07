using finapp_backend.Dtos.Comment;
using finapp_backend.Extentions;
using finapp_backend.Helpers;
using finapp_backend.Mappers;
using finapp_backend.Models;
using finapp_backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace finapp_backend.Controllers;
public class CommentController : ControllerBase
{
    private readonly CommentRepository _repository;
    private readonly StockRepository _stockRepository;
    private readonly UserManager<AppUsers> _userManager;
    public CommentController(CommentRepository repository, StockRepository stockRepository, UserManager<AppUsers> userManager)
    {
        _repository = repository;
        _stockRepository = stockRepository;
        _userManager = userManager;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject query)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comments = await _repository.GetAllAsync(query);
        var commentDto = comments.Select(x => x.ToCommentDto());
        return Ok(commentDto);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = await _repository.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }
    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto createCommentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _stockRepository.stockExists(stockId))
        {
            return BadRequest("Stock not found");
        }
        var username = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(username);
        var commentModel = createCommentDto.ToCommentFromCreateDto(stockId);
        commentModel.AppUserId = appUser.Id;
        await _repository.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());
    }
    [HttpPatch]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var commentModel = await _repository.UpdateAsync(updateCommentDto.ToCommentFromUpdateDto(), id);
        if (commentModel == null)
        {
            return NotFound("Comment not found");
        }
        return Ok(commentModel.ToCommentDto());

    }
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var commentModel = await _repository.DeleteAsync(id);
        if (commentModel == null)
        {
            return NotFound("Comment not found");
        }
        return Ok(commentModel);
    }
}