using System.Collections.Generic;

namespace VotingSystem.Models
{
    public class VotingPoll
    {
        public VotingPoll()
        {
            //This approach is better than new List, it doesnt
            //Occupied memory and its easier for garbage collecion
            //This line was Enumberable.Empty or something like that 
            Counters = new List<Counter>();
        }
        public List<Counter> Counters { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
