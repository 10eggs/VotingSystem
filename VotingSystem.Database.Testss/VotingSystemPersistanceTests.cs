using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.Application;
using VotingSystem.Database.Testss.Infrastructure;
using VotingSystem.Models;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Database.Testss
{
    public class VotingSystemPersistanceTests
    {
        [Fact]
        public void PersistVotingPoll()
        {
            var poll = new VotingPoll
            {
                Title = "title",
                Description = "Desc",
                Counters = new List<Counter>
                {
                    new Counter{Name = "One"},
                    new Counter {Name = "Two"}
                }
            };

            //ctrl+shift+arrow
            //alt+arrow
            using (var ctx = DbContextFactory.Create(nameof(PersistVotingPoll)))
            {
                IVotingPollPersistance persistance = new VotingSystemPersistance(ctx);
                persistance.SaveVotingPoll(poll);
            }

            using (var ctx = DbContextFactory.Create(nameof(PersistVotingPoll)))
            {
                IVotingPollPersistance persistance = new VotingSystemPersistance(ctx);
                var savedPoll = ctx.VotingPolls
                    .Include(x => x.Counters)
                    .Single();

                Assert.Equal(poll.Title, savedPoll.Title);
                Assert.Equal(poll.Description, savedPoll.Description);
                Assert.Equal(poll.Counters.Count(), savedPoll.Counters.Count());

                foreach(var name in poll.Counters.Select(x => x.Name))
                {
                    Assert.Contains(name, savedPoll.Counters.Select(x => x.Name));
                }

            }

        }

        [Fact]
        public void PersistVote()
        {
            var vote = new Vote { UserId = "name", CounterId = 1 };
            using(var ctx = DbContextFactory.Create(nameof(PersistVote)))
            {
                IVotingPollPersistance persistance = new VotingSystemPersistance(ctx);
                persistance.SaveVote(vote);
            }

            using (var ctx = DbContextFactory.Create(nameof(PersistVote)))
            {
                var savedVotes = ctx.Votes.Single();
                Equals(vote.UserId, savedVotes.UserId);
                Equals(vote.CounterId, savedVotes.CounterId);
            }
        }

        [Fact]
        public void VoteExist_ReturnFalseWhenNoVote()
        {
            var vote = new Vote { UserId = "name", CounterId = 1 };
            
            using(var ctx = DbContextFactory.Create(nameof(VoteExist_ReturnFalseWhenNoVote)))
            {
                var persistance = new VotingSystemPersistance(ctx);
                False(persistance.VoteExist(vote));
            }

        }

        [Fact]
        public void VoteExist_ReturnTrueWhenVoteExists()
        {
            var vote = new Vote { UserId= "name", CounterId = 1 };

            using (var ctx = DbContextFactory.Create(nameof(VoteExist_ReturnFalseWhenNoVote)))
            {
                ctx.Votes.Add(vote);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(VoteExist_ReturnFalseWhenNoVote)))
            {
                var persistance = new VotingSystemPersistance(ctx);
                True(persistance.VoteExist(vote));
            }
        }

        [Fact]
        public void GetPoll_ReturnSavedPollWithCounters_AndVotesAsCount()
        {
            var poll = new VotingPoll
            {
                Title = "title",
                Description = "Desc",
                Counters = new List<Counter>
                {
                    new Counter{Name = "One"},
                    new Counter {Name = "Two"}
                }
            };

            using (var ctx = DbContextFactory.Create(nameof(GetPoll_ReturnSavedPollWithCounters_AndVotesAsCount)))
            {
                ctx.VotingPolls.Add(poll);
                ctx.Votes.Add(new Vote { CounterId = 1, UserId = "a" });
                ctx.Votes.Add(new Vote { CounterId = 1, UserId = "a" });
                ctx.Votes.Add(new Vote { CounterId = 2, UserId = "b" });
                ctx.SaveChanges();

            }

            using (var ctx = DbContextFactory.Create(nameof(GetPoll_ReturnSavedPollWithCounters_AndVotesAsCount)))
            {
                var persistance = new VotingSystemPersistance(ctx);
                var savedPoll=persistance.GetPoll(1);

                Equal(poll.Title, savedPoll.Title);
                Equal(poll.Description, savedPoll.Description);
                Equal(poll.Counters.Count(), savedPoll.Counters.Count());

                foreach (var name in poll.Counters.Select(x=>x.Name))
                {
                    Contains(name, savedPoll.Counters.Select(x => x.Name));
                }

                var counter1 = savedPoll.Counters[0];
                Equal("One", counter1.Name);
                Equal(2, counter1.Count);
                //Equal(counterStatistics1.Percent, counter1.Percent);

                var counter2 = savedPoll.Counters[1];
                Equal("Two", counter2.Name);
                Equal(1, counter2.Count);

            }

        }

    }
}
