using BestFinanceTracker.Application.Features.Summary.Queries.GetMonthlySummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BestFinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SummaryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SummaryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetMonthlySummary([FromQuery] int year, [FromQuery] int month)
    {
        var summary = await _mediator.Send(new GetMonthlySummaryQuery(year, month));
        return Ok(summary);
    }
}