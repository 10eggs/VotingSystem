using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using VotingSystem.Models;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Test
{
    public class CounterTests
    {
        //BDD
        //You are not allowed to write any production code unless it is to make a failing unit test pass.
        //You are not allowed to write any more of a unit test than is sufficient to fail; and compilation failures are failures.
        //You are not allowed to write any more production code than is sufficient to pass the one failing unit test.

        public static Random rnd = new Random();
        public const string CounterName = "CounterName";
        public Counter _counter = new Counter() { Name = CounterName, Count = 5 };



        [Fact]
        public void GetStatistics_IncludesCounterName()
        {
            //var statistics = _counter.GetStatistics();
            //Equal(CounterName, statistics.Name); 
        }


    }
}
