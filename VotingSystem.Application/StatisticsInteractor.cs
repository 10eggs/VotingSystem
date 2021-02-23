namespace VotingSystem.Application
{
    public class StatisticsInteractor
    {
        private IVotingPollPersistance _persistance;
        private ICounterManager _counterManager;

        public StatisticsInteractor(IVotingPollPersistance persistance, ICounterManager counterManager)
        {
            _persistance = persistance;
            _counterManager = counterManager;
        }


        public PollStatistics GetStatistics(int pollId)
        {
            var poll = _persistance.GetPoll(pollId);
            var counters = _counterManager.GetStatistics(poll.Counters);

            _counterManager.ResolveExcess(counters);

            return new PollStatistics
            {
                Title = poll.Title,
                Description = poll.Description,
                Counters = counters

            };
        }

    }

}
