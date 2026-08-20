using BestFinanceTracker.Application.DTOs.Transactions;
using BestFinanceTracker.Application.Features.Transactions.Commands.CreateTransaction;
using BestFinanceTracker.Application.Features.Transactions.Commands.DeleteTransaction;
using BestFinanceTracker.Application.Features.Transactions.Commands.UpdateTransaction;
using BestFinanceTracker.Application.Features.Transactions.Queries.GetAllTransactions;
using BestFinanceTracker.Application.Features.Transactions.Queries.GetTransactionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BestFinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? year, [FromQuery] int? month)
    {
        var transactions = await _mediator.Send(new GetAllTransactionsQuery(year, month));
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var transaction = await _mediator.Send(new GetTransactionByIdQuery(id));
        return transaction is null ? NotFound() : Ok(transaction);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionDto dto)
    {
        var created = await _mediator.Send(new CreateTransactionCommand(
            dto.Amount, dto.Date, dto.Type, dto.Description, dto.CategoryId));
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTransactionDto dto)
    {
        var success = await _mediator.Send(new UpdateTransactionCommand(
            id, dto.Amount, dto.Date, dto.Type, dto.Description, dto.CategoryId));
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteTransactionCommand(id));
        return success ? NoContent() : NotFound();
    }
}