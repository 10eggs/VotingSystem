using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VotingSystem.Models;
using Xunit;

namespace VotingSystem.Application.Tests
{
    public class VotingInteractorTests
    {
        private readonly Mock<IVotingPollPersistance> _mockPersistance = new Mock<IVotingPollPersistance>();
        private readonly VotingInteractor _interactor;
        private readonly Vote _vote = new Vote { UserId= "Name", CounterId = 1 };

        public VotingInteractorTests()
        {
            _interactor= new VotingInteractor(_mockPersistance.Object);
        }

        [Fact]
        public void Vote_PersistsVoteWhenUserHasntVote()
        {

            _interactor.Vote(_vote);

            _mockPersistance.Verify(x => x.SaveVote(_vote));
        }

        [Fact]
        public void Vote_DoesntPersistVoteWhenUserAlreadyVoted()
        {
            var vote = new Vote { UserId = "user", CounterId = 1 };

            _mockPersistance.Setup(x => x.VoteExist(vote)).Returns(false);

            _interactor.Vote(_vote);

            _mockPersistance.Verify(x => x.SaveVote(vote),Times.Never);
        }
    }
}
