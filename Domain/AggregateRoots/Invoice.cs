using Domain.BuildingBlocks;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregateRoots
{
    public sealed class Invoice : AggregateRoot
    {
        public Guid WorkspaceId { get; private set; }
        public string FileUrl { get; private set; } = default!;
        public Money Total { get; private set; } = default!;
        public DateOnly Date { get; private set; }
        public bool Processed { get; private set; }
        public string MetadataJson { get; private set; } = "{}";
        public Guid UploadedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Invoice() { }

        public Invoice(
            Guid workspaceId,
            string fileUrl,
            Money total,
            DateOnly date,
            Guid uploadedBy)
        {
            WorkspaceId = workspaceId;
            FileUrl = fileUrl;
            Total = total;
            Date = date;
            UploadedBy = uploadedBy;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void MarkProcessed(string metadataJson)
        {
            MetadataJson = metadataJson;
            Processed = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
