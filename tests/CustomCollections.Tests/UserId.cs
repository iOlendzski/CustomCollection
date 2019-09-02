using System;
using System.Text.RegularExpressions;


namespace CustomCollections.Tests
{
    public class UserId : IEquatable<UserId>
    {
        private static readonly Regex ParseRegex = new Regex(@"(\d+)@(\S+)", RegexOptions.Compiled);

        public UserId(int id, string tenant)
        {
            Id = id;
            if (string.IsNullOrEmpty(tenant))
            {
                throw new ArgumentException(nameof(tenant));
            }
            Tenant = tenant;
        }

        public int Id { get; set; }

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

        public static UserId Parse(string userIdAsString)
        {
            if (string.IsNullOrEmpty(userIdAsString))
            {
                throw new ArgumentException(nameof(userIdAsString));
            }

            if (!ParseRegex.IsMatch(userIdAsString))
            {
                throw new FormatException("Неверный формат строки.");
            }

            var match = ParseRegex.Matches(userIdAsString)[0];
            var id    = int.Parse(match.Groups[1].Value);
            var name  = match.Groups[2].Value;

            return new UserId(id, name);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Id}@{Tenant}";
        }

        public static implicit operator string(UserId userId)
        {
            return userId?.ToString();
        }

        public static implicit operator UserId(string userIdAsString)
        {
            return Parse(userIdAsString);
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