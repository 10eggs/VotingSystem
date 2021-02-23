using Moq;
using System.Collections.Generic;
using VotingSystem.Models;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Application.Tests
{
    public class StatisticsInteractorTests
    {
        private Mock<IVotingPollPersistance> _mockPersistance = new Mock<IVotingPollPersistance>();
        private Mock<ICounterManager> _mockCounterManager = new Mock<ICounterManager>();


        [Fact]
        public void DisplayPollStatistics()
        {

            var pollId = 1;

            var counter1 = new Counter { Name = "One",Count=2, Percentage=60 };
            var counter2 = new Counter { Name = "Two",Count=1, Percentage=40 };

            var counterStatistics1 = new CounterStatistics { Name = "One", Count = 2, Percent = 60};
            var counterStatistics2 = new CounterStatistics { Name = "Two", Count = 1, Percent =40};

            var counterStats = new List<CounterStatistics> { counterStatistics1, counterStatistics2 };

            var poll = new VotingPoll
            {
                Title = "title",
                Description = "Desc",
                Counters = new List<Counter> { counter1, counter2 }
            };


            _mockPersistance.Setup(x => x.GetPoll(pollId)).Returns(poll);
            _mockCounterManager.Setup(x => x.GetStatistics(poll.Counters)).Returns(counterStats);
            //_mockCounterManager.Setup(x => x.ResolveExcess(counterStats));


            var interactor = new StatisticsInteractor(_mockPersistance.Object,_mockCounterManager.Object);

            var pollStatistics = interactor.GetStatistics(pollId);
            
            Assert.Equal(poll.Title, pollStatistics.Title);
            Assert.Equal(poll.Description, pollStatistics.Description);

            var stats1 = pollStatistics.Counters[0];
            Equal(counterStatistics1.Name, stats1.Name);
            Equal(counterStatistics1.Count, stats1.Count);
            Equal(counterStatistics1.Percent, stats1.Percent);
            //Equal(counterStatistics1.Percent, counter1.Percent);

            var stats2 = pollStatistics.Counters[1];
            Equal(counterStatistics2.Name, stats2.Name);
            Equal(counterStatistics2.Count, stats2.Count);
            Equal(counterStatistics2.Percent, stats2.Percent);

            _mockCounterManager.Verify(x => x.ResolveExcess(counterStats),Times.Once);

        }
    }

}
