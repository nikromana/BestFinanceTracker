using BestFinanceTracker.Application.DTOs.Budgets;
using BestFinanceTracker.Application.Features.Budgets.Commands.CreateBudget;
using BestFinanceTracker.Application.Features.Budgets.Commands.DeleteBudget;
using BestFinanceTracker.Application.Features.Budgets.Commands.UpdateBudget;
using BestFinanceTracker.Application.Features.Budgets.Queries.GetAllBudgets;
using BestFinanceTracker.Application.Features.Budgets.Queries.GetBudgetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BestFinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BudgetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? year, [FromQuery] int? month)
    {
        var budgets = await _mediator.Send(new GetAllBudgetsQuery(year, month));

        return Ok(budgets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var budget = await _mediator.Send(new GetBudgetByIdQuery(id));
        return budget is null ? NotFound() : Ok(budget);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBudgetDto dto)
    {
        var created = await _mediator.Send(new CreateBudgetCommand(dto.CategoryId, dto.Year, dto.Month, dto.Limit));
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBudgetDto dto)
    {
        var success = await _mediator.Send(new UpdateBudgetCommand(id, dto.CategoryId, dto.Year, dto.Month, dto.Limit));
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteBudgetCommand(id));
        return success ? NoContent() : NotFound();
    }
}