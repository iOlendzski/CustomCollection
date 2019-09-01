using System;


namespace CustomCollections.Tests
{
    public class UserId : IEquatable<UserId>
    {
        public UserId(int id, string tenant)
        {
            Id = id;
            if (string.IsNullOrEmpty(tenant))
            {
                throw new ArgumentException(nameof(tenant));
            }
        }

        public int Id { get; set }

        public string Tenant { get; set; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as UserId);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            const int MULTI = 23;
            var       hash  = 17;

            hash = hash * MULTI + Id.GetHashCode();
            hash = hash * MULTI + Tenant?.GetHashCode() ?? 0;

            return hash;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Id}@{Tenant}";
        }

        #region IEquatable<UserId> members
        /// <inheritdoc />
        public bool Equals(UserId other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Id == other.Id
                   && string.Equals(Tenant, other.Tenant);
        }
        #endregion
    }
}