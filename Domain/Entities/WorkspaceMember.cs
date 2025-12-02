using Domain.BuildingBlocks;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class WorkspaceMember : Entity
    {
        public Guid WorkspaceId { get; private set; }
        public Guid UserId { get; private set; }
        public WorkspaceRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private WorkspaceMember() { }

        internal WorkspaceMember(Guid workspaceId, Guid userId, WorkspaceRole role, string? permissionsJson = null)
        {
            WorkspaceId = workspaceId;
            UserId = userId;
            Role = role;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        internal void UpdateRole(WorkspaceRole role)
        {
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
