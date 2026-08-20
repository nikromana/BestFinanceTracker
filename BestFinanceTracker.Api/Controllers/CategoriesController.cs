using BestFinanceTracker.Application.DTOs.Category;
using BestFinanceTracker.Application.Features.Categories.Commands.CreateCategory;
using BestFinanceTracker.Application.Features.Categories.Commands.DeleteCategory;
using BestFinanceTracker.Application.Features.Categories.Commands.UpdateCategory;
using BestFinanceTracker.Application.Features.Categories.Queries.GetAllCategories;
using BestFinanceTracker.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BestFinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));

        return category is null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var created = await _mediator.Send(new CreateCategoryCommand(dto.Name, dto.TransactionType));

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        var success = await _mediator.Send(new UpdateCategoryCommand(id, dto.Name, dto.TransactionType));

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteCategoryCommand(id));

        return success ? NoContent() : NotFound();
    }
}