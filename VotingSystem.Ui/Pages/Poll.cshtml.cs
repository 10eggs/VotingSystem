using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VotingSystem.Application;

namespace VotingSystem.Ui.Pages
{
    public class PollModel : PageModel
    {
        public PollStatistics Statistics { get; private set; }

        public void OnGet(int id, [FromServices] StatisticsInteractor interactor)
        {
            Statistics = interactor.GetStatistics(id);
        }
    }
}
