using Domain.BuildingBlocks;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregateRoots
{

    public sealed class Category : AggregateRoot
    {
        public Guid? WorkspaceId { get; private set; }
        public string Name { get; private set; } = default!;
        public CategoryType Type { get; private set; }
        public string Icon { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public Guid? ParentCategoryId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Category() { }

        public Category(Guid? workspaceId, string name, CategoryType type, string icon, string color, Guid? parentCategoryId = null)
        {
            WorkspaceId = workspaceId;
            Name = name;
            Type = type;
            Icon = icon;
            Color = color;
            ParentCategoryId = parentCategoryId;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }
        public void Rename(string newName)
        {
            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
