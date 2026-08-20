using Shared;

namespace BestFinanceTracker.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public TransactionType Type { get; set; }
        public string? Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
