using Domain.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public string Value { get; }

        private Email() { }

        public Email(string value)
        {
            if (!value.Contains("@"))
                throw new ArgumentException("Invalid email format");

            Value = value.ToLowerInvariant();
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
