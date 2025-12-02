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
    public sealed class Account : AggregateRoot
    {
        public Guid WorkspaceId { get; private set; }
        public string Name { get; private set; } = default!;
        public string Type { get; private set; } = default!;
        public Money InitialBalance { get; private set; } = default!;
        public Money CurrentBalance { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Account() { }

        public Account(Guid workspaceId, string name, string type, Money initialBalance)
        {
            WorkspaceId = workspaceId;
            Name = name;
            Type = type;
            InitialBalance = initialBalance;
            CurrentBalance = initialBalance;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void Apply(TransactionType type, Money amount)
        {
            CurrentBalance = type == TransactionType.Ingreso
                ? CurrentBalance + amount
                : CurrentBalance - amount;

            UpdatedAt = DateTime.UtcNow;
        }

        public void Rename(string newName)
        {
            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeType(string newType)
        {
            Type = newType;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
