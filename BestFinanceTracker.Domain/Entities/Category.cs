using Shared;

namespace BestFinanceTracker.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TransactionType TransactionType { get; set; }
    }
}
