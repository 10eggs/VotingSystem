using VotingSystem.Models;

namespace VotingSystem.Application
{
    public class VotingInteractor
    {
        private IVotingPollPersistance _persistance;

        public VotingInteractor(IVotingPollPersistance persistance)
        {
            this._persistance= persistance;
        }

        public void Vote(Vote vote)
        {
            if(!_persistance.VoteExist(vote))
                _persistance.SaveVote(vote);
        }
    }
}
