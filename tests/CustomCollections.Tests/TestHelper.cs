using System.Collections.Generic;

using Collections;


namespace CustomCollections.Tests
{
    internal static class TestHelper
    {
        public static IReadOnlyList<User> Users => UserList?.AsReadOnly();

        private static readonly List<User> UserList =
                new List<User>
                {
                        new User {Id = "1@none", Name = "John"},
                        new User {Id = "2@none", Name = "Jane"},
                        new User {Id = "2@none", Name = "Will"},
                        new User {Id = "1@one", Name  = "Jane"},
                        new User {Id = "2@one", Name  = "Will"}
                };

        /// <summary>
        ///     Creates new instatnce of <see cref="CompositeKeyDictionary{UserId,string,User}" /> that contains 5 elements.
        /// </summary>
        /// <returns></returns>
        public static CompositeKeyDictionary<UserId, string, User> CreateDictionary()
        {
            var dictionary = new CompositeKeyDictionary<UserId, string, User>();
            foreach (var user in UserList)
            {
                dictionary.Add(user);
            }
            return dictionary;
        }
    }
}