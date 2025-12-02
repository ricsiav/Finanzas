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
    public sealed class User : AggregateRoot
    {
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public UserStatus Status { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private User() { }

        public User(string firstName, string lastName, Email email, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            Status = UserStatus.Active;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void Login()
        {
            LastLogin = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeStatus(UserStatus newStatus)
        {
            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
