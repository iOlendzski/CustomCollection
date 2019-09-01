using Shouldly;

using Xunit;


namespace CustomCollections.Tests
{
    public class CompositeKeyDictionaryTests
    {
        [Fact]
        public void AddShouldWork()
        {
            var dict = TestHelper.CreateDictionary();

            dict.Count.ShouldBe(4);
        }
    }
}