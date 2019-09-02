using System;
using System.Collections.Generic;
using System.Linq;

using Shouldly;

using Xunit;


namespace CustomCollections.Tests
{
    public class CompositeKeyDictionaryTests
    {
        [Fact]
        public void ShouldAddItem()
        {
            var dict = TestHelper.CreateDictionary();

            dict.Count.ShouldBe(5);
        }

        [Fact]
        public void ShouldBeEnumerable()
        {
            var dictionary = TestHelper.CreateDictionary();

            var count = 0;
            foreach (var _ in dictionary)
            {
                count++;
            }
            count.ShouldBe(5);
        }

        [Fact]
        public void ShouldGetValue()
        {
            var dictionary = TestHelper.CreateDictionary();

            var user = dictionary.GetValue("1@one", "Jane");

            user.ShouldNotBeNull();
            user.Id.ShouldBe((UserId) "1@one");
            user.Name.ShouldBe("Jane");
        }

        [Fact]
        public void ShouldGetValuesById()
        {
            var dictionary = TestHelper.CreateDictionary();

            var users = dictionary.GetValuesById("2@none");
            users.Count.ShouldBe(2);

            var userNames = users.Select(x => x.Name).ToArray();
            userNames.ShouldContain("Jane");
            userNames.ShouldContain("Will");
        }

        [Fact]
        public void ShouldGetValuesByName()
        {
            var dictionary = TestHelper.CreateDictionary();

            var users = dictionary.GetValuesByName("Will");
            users.Count.ShouldBe(2);

            var userIds = users.Select(x => x.Id.ToString()).ToArray();
            userIds.ShouldContain("2@none");
            userIds.ShouldContain("2@one");
        }

        [Fact]
        public void ShouldRemoveAllById()
        {
            var dictionary = TestHelper.CreateDictionary();

            var removed = dictionary.RemoveAllById("2@none");

            removed.ShouldBe(2);
        }

        [Fact]
        public void ShouldRemoveAllByName()
        {
            var dictionary = TestHelper.CreateDictionary();

            var removed = dictionary.RemoveAllByName("Will");

            removed.ShouldBe(2);
        }

        [Fact]
        public void ShouldRemoveByCompositeKey()
        {
            var dictionary = TestHelper.CreateDictionary();

            var removed = dictionary.Remove("2@none", "Jane");

            removed.ShouldBeTrue();
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWhenAddExistingItem()
        {
            var dict = TestHelper.CreateDictionary();

            Action check = () => dict.Add(new User("1@none", "John"));

            check.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ShouldThrowKeyNotFoundExceptionWhenGetValueByUnknownKey()
        {
            var dictionary = TestHelper.CreateDictionary();

            Action check = () => dictionary.GetValue("5@two", "Mark");

            check.ShouldThrow<KeyNotFoundException>();
        }

        [Fact]
        public void ShouldThrowKeyNotFoundExceptionWhenGetValuesByName()
        {
            var dictionary = TestHelper.CreateDictionary();

            Action check = () => dictionary.GetValuesByName("Mark");

            check.ShouldThrow<KeyNotFoundException>();
        }

        [Fact]
        public void ShouldThrowKeyNotFoundExceptionWhenGetValuesByUnknownId()
        {
            var dictionary = TestHelper.CreateDictionary();

            Action check = () => dictionary.GetValuesById("5@two");

            check.ShouldThrow<KeyNotFoundException>();
        }

        [Fact]
        public void ShouldTryGetValue()
        {
            var dictionary = TestHelper.CreateDictionary();

            var result1 = dictionary.TryGetValue("1@one", "Jane", out var user1);
            var result2 = dictionary.TryGetValue("1@none", "Will", out var user2);

            result1.ShouldBeTrue();
            user1.ShouldNotBeNull();
            user1.Id.ShouldBe((UserId) "1@one");
            user1.Name.ShouldBe("Jane");

            result2.ShouldBeFalse();
            user2.ShouldBe(default);
        }

        [Fact]
        public void ShouldTryGetValuesById()
        {
            var dictionary = TestHelper.CreateDictionary();

            var result1 = dictionary.TryGetValuesById("2@none", out var users1);
            var result2 = dictionary.TryGetValuesById("1@two", out _);

            result1.ShouldBeTrue();
            users1.ShouldNotBeNull();
            users1.Count.ShouldBe(2);

            result2.ShouldBeFalse();
        }

        [Fact]
        public void ShouldTryGetValuesByName()
        {
            var dictionary = TestHelper.CreateDictionary();

            var result1 = dictionary.TryGetValuesByName("John", out var users1);
            var result2 = dictionary.TryGetValuesByName("Mark", out _);

            result1.ShouldBeTrue();
            users1.ShouldNotBeNull();
            users1.Count.ShouldBe(1);

            result2.ShouldBeFalse();
        }
    }
}