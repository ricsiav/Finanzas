using Domain.BuildingBlocks;
using Domain.Utils;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregateRoots
{
    public sealed class RecurrenceRule : AggregateRoot
    {
        public Guid WorkspaceId { get; private set; }
        public Guid AccountId { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid SourceId { get; private set; }
        public RecurrenceFrequency Frequency { get; private set; }
        public Money Amount { get; private set; } = default!;
        public DateOnly NextRun { get; private set; }
        public string? DescriptionTemplate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private RecurrenceRule() { }

        public RecurrenceRule(
            Guid workspaceId,
            Guid accountId,
            Guid categoryId,
            Guid sourceId,
            RecurrenceFrequency frequency,
            Money amount,
            DateOnly nextRun,
            string? descriptionTemplate = null)
        {
            WorkspaceId = workspaceId;
            AccountId = accountId;
            CategoryId = categoryId;
            SourceId = sourceId;
            Frequency = frequency;
            Amount = amount;
            NextRun = nextRun;
            DescriptionTemplate = descriptionTemplate;

            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void MoveToNextOccurrence()
        {
            NextRun = Frequency switch
            {
                RecurrenceFrequency.Daily => NextRun.AddDays(1),
                RecurrenceFrequency.Weekly => NextRun.AddDays(7),
                RecurrenceFrequency.Monthly => NextRun.AddMonths(1),
                RecurrenceFrequency.Yearly => NextRun.AddYears(1),
                _ => NextRun
            };
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(
            RecurrenceFrequency frequency,
            Money amount,
            DateOnly nextRun,
            string? descriptionTemplate)
        {
            Frequency = frequency;
            Amount = amount;
            NextRun = nextRun;
            DescriptionTemplate = descriptionTemplate;
            UpdatedAt = DateTime.UtcNow;
        }

        public Transaction GenerateTransaction(Guid createdBy)
        {
            return new Transaction(
                workspaceId: WorkspaceId,
                accountId: AccountId,
                categoryId: CategoryId,
                sourceId: SourceId,
                type: Amount.Amount >= 0 ? TransactionType.Egreso : TransactionType.Ingreso, // regla simple, se puede mejorar
                amount: Amount,
                date: NextRun,
                createdBy: createdBy,
                isRecurring: true,
                recurrenceRuleId: Id,
                description: DescriptionTemplate);
        }
    }
}
