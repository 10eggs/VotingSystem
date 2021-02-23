using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.Models;

namespace VotingSystem
{
    public class CounterManager : ICounterManager
    {

        public List<Counter> Counters { get; internal set; }

        public CounterManager(params Counter[] counters)
        {
            Counters = new List<Counter>(counters);
        }
        public CounterManager()
        {
            Counters = new List<Counter>();
        }
        public CounterManager(List<Counter> counters)
        {
            Counters = counters;
        }
        //public Counter GetStatistics(Counter counter,int totalCount)
        //{
        //    counter.Percentage = RoundUp((counter.Percentage * 100.0) / totalCount);
        //    return counter;
        //}

        public int GetTotal()
        {
            return Counters.Select(c => c.Count).Sum();
        }
        public IEnumerable<Counter> GetWinner()
        {
            int maxCounter = Counters.Select(c => c.Count).Max();
            return Counters.Where(c => c.Count == maxCounter);

        }
        private static double RoundUp(double num) => Math.Round(num,2);

        public void ResolveExcess(List<CounterStatistics> counters)
        {
            var totalPercent = counters.Sum(x => x.Percent);

            if (totalPercent == 100) return;

            var excess = 100 - totalPercent;

            var highestPercentage = counters.Max(c => c.Percent);
            var highestCounters = counters.Where(c => c.Percent == highestPercentage);
            
            if(highestCounters.Count() == 1)
            {
                highestCounters.First().Percent += excess;
            }
            else if(highestCounters.Count() < counters.Count())
            {
                var lowestPercent = counters.Min(x => x.Percent);
                var lowestCounter = counters.First(x => x.Percent ==lowestPercent);
                lowestCounter.Percent = RoundUp(lowestPercent + excess);

            }

        }

        public List<CounterStatistics> GetStatistics(ICollection<Counter> counters)
        {
            var totalCount = counters.Sum(x => x.Count);

            return counters.Select(x => new CounterStatistics
            {
                Name = x.Name,
                Count = x.Count,
                Percent = totalCount>0 ? RoundUp((x.Count * 100.0) / totalCount) : 0

            }).ToList();
        }

    }
}
