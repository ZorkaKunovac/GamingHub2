using System;
using Xunit;

namespace GamingHub2UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public void TestSabiranje()
        {
            var expected = 5;

            var actual = 4;

            Assert.NotEqual(expected, actual);
        }
    }
}
