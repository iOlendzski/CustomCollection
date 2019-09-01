using Collections;


namespace CustomCollections.Tests
{
    public class User : ICompositeKey<UserId, string>
    {
        /// <inheritdoc />
        public UserId Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        public string Description { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{Id} : {Name}]";
        }
    }
}