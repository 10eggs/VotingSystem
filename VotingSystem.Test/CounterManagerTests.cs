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
    public class CounterManagerTests
    {
        static Random rnd = new Random();

        public const string CounterName = "Counter Name";
        public Counter _counter = new Counter { Name = CounterName, Count = 5 };



        [Fact]
        public void GetStatistics_IncludesCounterName()
        {
            var statistics = new CounterManager().GetStatistics(new[] { _counter }).First();
            Equal(CounterName, statistics.Name);
        }

        [Fact]

        public void GetStatistics_IncludesCounterCount()
        {
            var statistics = new CounterManager().GetStatistics(new[] { _counter }).First();
            Equal(5, statistics.Count);
        }

        [Theory]
        [InlineData(5,10,50)]
        [InlineData(1,3,33.33)]
        [InlineData(0,0,0)]
        public void GetStatistics_ShowPercentageUpTwoDecimalBasedOnTotalCount(int count,int total, double expected)
        {
            _counter.Count = count;
            var counter = new Counter { Count = total - count };
            var statistics = new CounterManager().GetStatistics(new[] { _counter, counter }).First();
            Equal(expected, statistics.Percent);

        }

        [Theory]
        [InlineData(33.33)]
        [InlineData(0)]
        public void ResolveExcess_DoesntAddExcessWhenAllCountersAreEqual(double percent)
        {
            var counter1 = new CounterStatistics {  Percent = percent };
            var counter2 = new CounterStatistics {  Percent = percent };
            var counter3 = new CounterStatistics {  Percent = percent };

            List<CounterStatistics> counters = new List<CounterStatistics> { counter1, counter2, counter3 };

            new CounterManager().ResolveExcess(counters);

            Equal(percent, counter1.Percent);
            Equal(percent, counter2.Percent);
            Equal(percent, counter3.Percent);
        }



        [Fact]
        public void ResolveExcess_AddExcessToHighestCounter()
        {
            var counter1 = new CounterStatistics { Percent = 66.66 };
            var counter2 = new CounterStatistics { Percent = 33.33 };
            List<CounterStatistics> counters = new List<CounterStatistics> { counter1, counter2 };

            new CounterManager(new Counter()).ResolveExcess(counters);

            Equal(66.67, counter1.Percent);
            Equal(33.33, counter2.Percent);

        }

        [Fact]
        public void ResolveExcess_AddsExcessTOLowestCounterWhenMoreThanOneHighestCounters()
        {
            var counter1 = new CounterStatistics { Percent = 44.44 };
            var counter2 = new CounterStatistics { Percent = 44.44 };
            var counter3 = new CounterStatistics { Percent = 11.11 };

            List<CounterStatistics> counters = new List<CounterStatistics> { counter1, counter2, counter3 };

            new CounterManager(new Counter()).ResolveExcess(counters);

            Equal(44.44, counter1.Percent);
            Equal(44.44, counter2.Percent);
            Equal(11.12, counter3.Percent);
        }



        [Fact]
        public void ResolveExcess_DoesntAddExcessIfTotalPercentIs100()
        {

            var counter1 = new CounterStatistics { Percent = 80 };
            var counter2 = new CounterStatistics { Percent = 20 };

            List<CounterStatistics> counters = new List<CounterStatistics> { counter1, counter2 };

            new CounterManager(new Counter()).ResolveExcess(counters);

            Equal(80, counter1.Percent);
            Equal(20, counter2.Percent);


        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public void GetTotal_ReturnCorrectTotalIfThereIsOneCounter(int num)
        {
            var counter = new Counter() { Count = num };
            var counterManager = new CounterManager(counter);
            var total = counterManager.GetTotal();
            Equal(num, total);
        }

        //[Theory]
        //[InlineData(10,1)]
        //[InlineData(3,30)]
        //[InlineData(2,1)]
        //[InlineData(7,14)]
        //public void GetTotal_ReturnCorrectTotalIfThereIsMoreThanOneCounter(int numOfCounters,int votes)
        //{
        //    List<Counter> counters = new List<Counter>();

        //    for(int i = 0; i < numOfCounters; i++)
        //    {
        //        counters.Add(new Counter() { Count = votes });
        //    }

        //    //Do I have to mock list ?

        //    var counterManager = new CounterManager(counters);

        //    var expectedTotal = numOfCounters * votes;

        //    var total = counterManager.GetTotal();

        //    Equal(expectedTotal, total);
        //}

        //[Theory]
        //[InlineData(1)]
        //[InlineData(5)]
        //[InlineData(10)]
        //[InlineData(15)]
        //[InlineData(20)]
        //public void GetWinner_WinnerHasHighestCounterNumber(int countersNumber)
        //{

        //    var counters = Enumerable.Repeat(new Counter(), countersNumber).ToList();

        //    foreach(Counter c in counters)
        //    {
        //        c.Count = rnd.Next(100);
        //    }

        //    int expectedWinnerCount = counters.Select(n => n.Count).Max();

        //    Debug.WriteLine("Highest number: " + expectedWinnerCount);

        //    CounterManager counterManager = new CounterManager(counters);

        //    var winners = counterManager.GetWinner();
        //    foreach(Counter c in winners)
        //    {
        //        Debug.WriteLine("Winner counts returned by GetWinner method: " + c.Count);
        //        Equal(expectedWinnerCount, c.Count);
        //    }
        //}


    }
}
