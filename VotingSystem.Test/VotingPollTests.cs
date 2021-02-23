using System;
using System.Collections;
using System.Linq;
using VotingSystem.Models;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Test
{
    public class VotingPollTests
    {
        [Fact]
        public void ZeroCounterWhenCreated()
        {
            var poll = new VotingPoll();
            Empty(poll.Counters);
        }
    }

    public class VotingPollFactoryTests
    { 
        private VotingPollFactory _factory = new VotingPollFactory();
        private VotingPollFactory.Request _request = new VotingPollFactory.Request
        {
            Names = new[] { "name1", "name2" },
            Title = "PoolName",
            Description = "Description"
        };


        [Fact]
       public void Create_ThrowIfLessThanTwoCounterNames()
        {
            _request.Names = new string[] { };
            Throws<ArgumentException>(()=>_factory.Create(_request));
            _request.Names = new string[] { "CoutnerName" };
            Throws<ArgumentException>(()=>_factory.Create(_request));
        }

        [Fact]
        public void Create_CreatesCounterToThePollForEachName()
        {

            var poll = _factory.Create(_request);

            foreach(var name in _request.Names)
            {
                Contains(name, poll.Counters.Select(c => c.Name));
            }
        }

        [Fact]
        public void Create_AddTitleToThePoll()
        {
            var title = "Title";
            _request.Title = title;
            var poll = _factory.Create(_request);

            Equal(poll.Title, _request.Title);

        }

        [Fact]
        public void Create_AddDescriptionToThePoll()
        {
            var description = "Description";
            _request.Description = description;
            var poll = _factory.Create(_request);


            Equal(poll.Description, _request.Description);

        }
    }
}
