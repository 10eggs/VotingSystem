using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox
{

    public class Counter
    {

        private double? _percentage;

        public Counter(string name, int count)
        {
            Name = name;
            Count = count;
        }
        public string Name { get; }
        public int Count { get; }

        //If count will change, then percentage is not, should be improved
        //GetPercentage need to be explained shortly because of ?? nullable

        public double GetPercent(int total) =>_percentage ?? (_percentage=Math.Round(Count* 100.0 / total, 2)).Value;

        public void AddExcess(double excess) => _percentage += excess;
    }

    public class CounterManager
    {
        public List<Counter> Counters { get; set; }
        public CounterManager(List<Counter> counters)
        {
            Counters = counters;
        }

        public CounterManager(params Counter[] counters)
        {
            Counters = new List<Counter>(counters);
        }

        public int Total() => Counters.Sum(c => c.Count);
        public double TotalPercentage() => Counters.Sum(c => c.GetPercent(Total()));


        public void AnnounceWinner()
        {
            var excess = Math.Round(100.0 - TotalPercentage(), 2);

            var max = Counters.Max(c=>c.Count);

            var winners = Counters.Where(c => c.Count == max).ToList();

            if (winners.Count == 1)
            {
                var winner = winners.First();
                winner.AddExcess(excess);
                Console.WriteLine($"And the winner is...{winner.Name}");
            }
            else
            {
                if(winners.Count != Counters.Count)
                {
                    var lowestAmountOfVotes = Counters.Min(x => x.Count);
                    var loser = Counters.First(x => x.Count == lowestAmountOfVotes);
                    loser.AddExcess(excess);
                }

                Console.WriteLine(string.Join(" - DRAW - ", winners.Select(x => x.Name))); 
            }
            Console.WriteLine($"Excess: {excess}");
            foreach(var c in Counters)
            {
                Console.WriteLine($"{c.Name} counts={c.Count}, Percentage={c.GetPercent(Total())}%");
            }
            Console.WriteLine($"Total Percentage: {TotalPercentage()}%");
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            var yes = new Counter("Yes", 4);
            var no = new Counter("No", 4);
            var maybe = new Counter("Maybe", 3);
            var hopefully = new Counter("Hopefuly", 33);

            var manager = new CounterManager(yes, no, maybe, hopefully);

            int total = yes.Count + no.Count + maybe.Count;

            var yesPercent = yes.GetPercent(manager.Total());
            var noPercent = no.GetPercent(manager.Total());
            var maybePercent = maybe.GetPercent(manager.Total());



            manager.AnnounceWinner();
        }
    }

}