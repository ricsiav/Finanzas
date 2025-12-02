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
    public sealed class Transaction : AggregateRoot
    {
        public Guid WorkspaceId { get; private set; }
        public Guid AccountId { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid SourceId { get; private set; }
        public TransactionType Type { get; private set; }
        public Money Amount { get; private set; } = default!;
        public DateOnly Date { get; private set; }
        public string? Description { get; private set; }
        public bool IsRecurring { get; private set; }
        public Guid? RecurrenceRuleId { get; private set; }
        public Guid? InvoiceId { get; private set; }
        public Guid CreatedBy { get; private set; }
        public Guid UpdatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Transaction() { }

        public Transaction(
            Guid workspaceId,
            Guid accountId,
            Guid categoryId,
            Guid sourceId,
            TransactionType type,
            Money amount,
            DateOnly date,
            Guid createdBy,
            bool isRecurring = false,
            Guid? recurrenceRuleId = null,
            Guid? invoiceId = null,
            string? description = null)
        {
            WorkspaceId = workspaceId;
            AccountId = accountId;
            CategoryId = categoryId;
            SourceId = sourceId;
            Type = type;
            Amount = amount;
            Date = date;
            Description = description;
            IsRecurring = isRecurring;
            RecurrenceRuleId = recurrenceRuleId;
            InvoiceId = invoiceId;
            CreatedBy = UpdatedBy = createdBy;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void AttachInvoice(Guid invoiceId, Guid updatedBy)
        {
            InvoiceId = invoiceId;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDescription(string? description, Guid updatedBy)
        {
            Description = description;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(Money amount, DateOnly date, string? description, Guid updatedBy)
        {
            Amount = amount;
            Date = date;
            Description = description;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reassign(Guid accountId, Guid categoryId, Guid sourceId, TransactionType type, Guid updatedBy)
        {
            AccountId = accountId;
            CategoryId = categoryId;
            SourceId = sourceId;
            Type = type;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
