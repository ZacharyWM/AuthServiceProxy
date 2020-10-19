using System;
using Xunit;

namespace myAuthApp.Tests {
    public class UnitTest1 {
        [Fact]
        public void TestFalse() {
            Assert.False("a" == "b", "They should not be equal");
        }

        [Fact]
        public void TestTrue() {
            Assert.True("a" == "a", "They should be equal!");
        }
    }
}
