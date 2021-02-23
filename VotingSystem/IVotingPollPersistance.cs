using VotingSystem.Models;

namespace VotingSystem
{
    public interface IVotingPollPersistance
    {
        void SaveVotingPoll(VotingPoll votingPoll);
        void SaveVote(Vote vote);
        bool VoteExist(Vote vote);
        VotingPoll GetPoll(int pollId);
    }
}
