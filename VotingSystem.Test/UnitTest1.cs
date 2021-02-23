using Moq;
using System;
using Xunit;

namespace VotingSystem.Test
{
    public class UnitTest1
    {
        public interface ITestOne
        {
            public int Add(int i, int b);
            public void Out(string msg);

        }

        public class TestOne:ITestOne 
        {
            public int Add(int a, int b) => a + b;

            public void Out(string msg)
            {
                Console.WriteLine(msg);
            }

        }

        public class MathOne
        {
            public ITestOne testOne { get; set; }
            public MathOne(ITestOne testOne)
            {
                this.testOne = testOne;

            }
            public void Out(string msg) => testOne.Out(msg);
            public int Add(int a, int b) => testOne.Add(a, b);
        }

        public class MathOneTest
        {
            [Fact]
            public void MathOneAddTwoNumbers()
            {
                var testOneMock = new Mock<ITestOne>();
                testOneMock.Setup(x => x.Add(1, 1)).Returns(2);

                var mathOne = new MathOne(testOneMock.Object);

                Assert.Equal(2, mathOne.Add(1, 1));
            }
            [Fact]

            public void VerifyFunctionHasBeenCalled()
            {

                var testOneMock = new Mock<ITestOne>();
                var mathOne = new MathOne(testOneMock.Object);
                var msg = "Anything";
                mathOne.Out(msg);
                testOneMock.Verify(x => x.Out("Anything"), Times.Once);

            }

        }
        public class TestOneTests
        {
            [Fact]
            public void Add_AddsTwoNumbersTogether()
            {
                var results = new TestOne().Add(1, 1);
                Assert.Equal(2, results);
            }

            [Theory]
            [InlineData(1,2,3)]
            [InlineData(3,5,8)]
            [InlineData(2,2,4)]
            [InlineData(3,2,5)]
            [InlineData(5,5,0)]
            public void Add_AddsTwoNumbersTogether_Theory(int a,int b, int expected)
            {
                var result = new TestOne().Add(a, b);
                Assert.Equal(expected, result);
            }

        }
    }
}
