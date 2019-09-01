using System.Collections.Generic;

using Collections;


namespace CustomCollections.Tests
{
    internal static class TestHelper
    {
        private static readonly List<User> Users =
                new List<User>
                {
                        new User {Id = new UserId(1, "none"), Name = "John"},
                        new User {Id = new UserId(2, "none"), Name = "Jane"},
                        new User {Id = new UserId(1, "one"), Name  = "Jane"},
                        new User {Id = new UserId(2, "one"), Name  = "Will"}
                };

        /// <summary>
        ///     Creates new instatnce of <see cref="CompositeKeyDictionary{UserId,string,User}" /> that contains 4 elements.
        /// </summary>
        /// <returns></returns>
        public static CompositeKeyDictionary<UserId, string, User> CreateDictionary()
        {
            var dictionary = new CompositeKeyDictionary<UserId, string, User>();
            foreach (var user in Users)
            {
                dictionary.Add(user);
            }
            return dictionary;
        }
    }
}