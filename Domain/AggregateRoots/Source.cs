using Domain.BuildingBlocks;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregateRoots
{
    public sealed class Source : AggregateRoot
    {
        public Guid WorkspaceId { get; private set; }
        public string Name { get; private set; } = default!;
        public SourceType Type { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Source() { }

        public Source(Guid workspaceId, string name, SourceType type)
        {
            WorkspaceId = workspaceId;
            Name = name;
            Type = type;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }
        public void Rename(string newName)
        {
            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
