using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VotingSystem.Models;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Application.Test
{
    public class VotingPollInteractorTests
    {
        private VotingPollFactory.Request _request = new VotingPollFactory.Request();
        private Mock<IVotingPollFactory> _mockFactory = new Mock<IVotingPollFactory>();
        private Mock<IVotingPollPersistance> _mockPersistance = new Mock<IVotingPollPersistance>();
        private VotingPollInteractor _interactor;
        public VotingPollInteractorTests()
        {
            //Can't use initialization of Interactor outside constructor
            _interactor = new VotingPollInteractor(_mockFactory.Object, _mockPersistance.Object);
        }

        [Fact]
        public void CreateVotingPoll_UseVotingPollFactoryToCreateVotingPoll()
        {
            //Moq should be use always agains interfaces


            _interactor.CreateVotingPoll(_request);

            _mockFactory.Verify(m => m.Create(_request));

        }

        [Fact]

        public void CreateVotingPoll_PersistCreatedPoll()
        {
            var poll = new VotingPoll();

            _mockFactory.Setup(m => m.Create(_request)).Returns(poll);
            //interactor.SavePoll();
            _interactor.CreateVotingPoll(_request);

            _mockPersistance.Verify(x => x.SaveVotingPoll(poll));

        }

        [Fact]
        public void GetVotingPoll_GetPersitedPollWithSelectedId()
        {
            //Arrange
            var id = 1;

            var poll = new VotingPoll();
            _mockFactory.Setup(m => m.Create(_request)).Returns(poll);
            _interactor.CreateVotingPoll(_request);
            
            //Act
            var votingPoll = _interactor.GetVotingPoll(id);

            //Assert
            Equals(poll, votingPoll);

        }

    }
}
