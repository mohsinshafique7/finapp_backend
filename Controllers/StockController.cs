using finapp_backend.Data;
using finapp_backend.Dtos.Stock;
using finapp_backend.Helpers;
using finapp_backend.Interfaces;
using finapp_backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace finapp_backend.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IStockRepository _stockRepository;
    public StockController(ApplicationDBContext context, IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stocks = await _stockRepository.GetAllAsync(query);
        var stockDtos = stocks.Select(x => x.ToStockDto()).ToList();
        return Ok(stockDtos);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stock = await _stockRepository.GetByIdAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockModel = createStockDto.ToStockFromCreateDto();
        await _stockRepository.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
    [HttpPatch]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockModel = await _stockRepository.UpdateAsync(id, updateStockDto);
        if (stockModel == null)
        {
            return NotFound();
        }

        return Ok(stockModel.ToStockDto());
    }
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockModel = await _stockRepository.DeleteAsync(id);
        if (stockModel == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}