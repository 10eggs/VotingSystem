using System;
using VotingSystem.Models;

namespace VotingSystem.Application
{
    public class VotingPollInteractor
    {
        private IVotingPollFactory _factory;
        private IVotingPollPersistance _persistance;
        public VotingPollInteractor(IVotingPollFactory factory, IVotingPollPersistance persistance)
        {
            _factory = factory;
            _persistance = persistance;
        }

        public void CreateVotingPoll(VotingPollFactory.Request request)
        {
             var pool=_factory.Create(request);
            _persistance.SaveVotingPoll(pool);
        }

        public VotingPoll GetVotingPoll(int id)
        {
            return _persistance.GetPoll(id);
        }
    }
}
