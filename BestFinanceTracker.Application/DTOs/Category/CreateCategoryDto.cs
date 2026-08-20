using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestFinanceTracker.Application.DTOs.Category
{
    public record CreateCategoryDto(string Name, TransactionType Type);
}
