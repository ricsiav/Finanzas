using Domain.BuildingBlocks;
using Domain.Entities;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregateRoots
{
    public sealed class Workspace : AggregateRoot
    {
        private readonly List<WorkspaceMember> _members = new();
        public Guid OwnerId { get; private set; }
        public string Name { get; private set; } = default!;
        public WorkspaceType Type { get; private set; }
        public IReadOnlyCollection<WorkspaceMember> Members => _members;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Workspace() { }

        public Workspace(Guid ownerId, string name, WorkspaceType type)
        {
            OwnerId = ownerId;
            Name = name;
            Type = type;
            CreatedAt = UpdatedAt = DateTime.UtcNow;

            _members.Add(new WorkspaceMember(Id, ownerId, WorkspaceRole.Owner));
        }

        public void Rename(string newName)
        {
            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
