using System.Collections.Generic;
using VotingSystem.Models;

namespace VotingSystem
{
    public interface ICounterManager
    {
        List<CounterStatistics> GetStatistics(ICollection<Counter> poll);
        void ResolveExcess(List<CounterStatistics> counterStats);
    }

}
