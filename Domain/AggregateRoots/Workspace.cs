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

        public void AddMember(Guid userId, WorkspaceRole role)
        {
            if (_members.Any(member => member.UserId == userId))
                throw new InvalidOperationException("El usuario ya es miembro del workspace.");

            _members.Add(new WorkspaceMember(Id, userId, role));
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveMember(Guid userId)
        {
            var member = _members.SingleOrDefault(member => member.UserId == userId);

            if (member is null)
                throw new InvalidOperationException("El miembro no existe en el workspace.");

            if (member.Role == WorkspaceRole.Owner)
                throw new InvalidOperationException("No se puede eliminar al propietario del workspace.");

            _members.Remove(member);
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeMemberRole(Guid userId, WorkspaceRole newRole)
        {
            var member = _members.SingleOrDefault(member => member.UserId == userId);

            if (member is null)
                throw new InvalidOperationException("El miembro no existe en el workspace.");

            member.UpdateRole(newRole);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
