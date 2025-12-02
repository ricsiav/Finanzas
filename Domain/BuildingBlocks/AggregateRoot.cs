using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BuildingBlocks
{
    public abstract class AggregateRoot:Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IDomainEvent evt) => _domainEvents.Add(evt);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
