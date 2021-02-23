using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotingSystem.Database.Testss.Infrastructure;
using VotingSystem.Models;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Database.Testss
{
    public class AppDbContextTests
    {
        //We need to specify name of DB to avoid problem with sharing state of db between tests during parallel execution


        [Fact]
        public void SaveCountersToDatabase()
        {
            

            var counter = new Counter { Name = "New counter" };
            using (var ctx = DbContextFactory.Create(nameof(SaveCountersToDatabase)))
            {
                ctx.Counters.Add(counter);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(SaveCountersToDatabase)))
            {
                var savedCounter = ctx.Counters.Single();
                Equal(counter.Name, savedCounter.Name);
            }
        }

        [Fact]
        public void SaveVotingPollToDatabase()
        {

            var votingPoll = new VotingPoll { Title = "New VotingPoll" };
            using (var ctx = DbContextFactory.Create(nameof(SaveCountersToDatabase)))

            {
                ctx.VotingPolls.Add(votingPoll);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(SaveVotingPollToDatabase)))
            {
                var savedPoll = ctx.VotingPolls.Single();
                Equal(votingPoll.Title, savedPoll.Title);
            }
        }

        //[Fact]
        //public void SaveCounterToVotingPoll()
        //{
        //    int votingPollId;
        //    using (var ctx = CreateDbContext(nameof(SaveCounterToVotingPoll)))

        //    {
        //        var votingPoll = new VotingPoll { Title = "New VotingPoll" };
        //        votingPollId = ctx.Entry(votingPoll).Property<int>("Id").CurrentValue;

        //        ctx.VotingPolls.Add(votingPoll);
        //        ctx.SaveChanges();
        //    }
   
        //    using (var ctx = CreateDbContext(nameof(SaveCounterToVotingPoll)))
        //    {
        //        var counter = new Counter { Name = "New counter" };
        //        ctx.Counters.Add(counter);

        //        ctx.Entry(counter).Property<int>("VotingPollId").CurrentValue = votingPollId;
        //        ctx.SaveChanges();
            
        //    }
        //    using (var ctx = CreateDbContext(nameof(SaveCounterToVotingPoll)))
        //    {
        //        var savedPoll = ctx.VotingPolls.Single();
        //        //Equal(votingPoll.Title, savedPoll.Title);
        //    }
        //}

    }
}
