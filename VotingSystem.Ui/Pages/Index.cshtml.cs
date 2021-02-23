using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Application;
using VotingSystem.Database;
using VotingSystem.Models;

namespace VotingSystem.Ui.Pages
{
    public class IndexModel : PageModel
    {
        private VotingPollInteractor _votingPollInteractor;

        public ICollection<VotingPollVM> VotingPolls { get; set; }

        /**
         * Following lines of code were added just to verify how to handle with data from form using [FromForm] attribute
         * So basically we need to put one of this variables into razor pages to be able to grab values from form
        // */
        //public string ReqTitle => (string)TempData[nameof(ReqTitle)];

        //public string ReqDes => (string)TempData[nameof(ReqDes)];
        //public string[] ReqNames => (string[])TempData[nameof(ReqNames)];


        [BindProperty]
        public VotingPollFactory.Request Form { get; set; }
        public IndexModel(VotingPollInteractor votingPollInteractor)
        {
            _votingPollInteractor = votingPollInteractor;
        }
        public int Id { get; set; }


        //Handler
        //This need to be rebuilded to use Interactor interface rather than injecting DbContext
        public void OnGet([FromServices] AppDbContext ctx)
        {
            VotingPolls=ctx.VotingPolls.Select(x => new VotingPollVM
            {
                Id = EF.Property<int>(x, "Id"),
                Title = x.Title,
                Description = x.Description

            }).ToList();
        }

        public IActionResult OnPost()
        {
            _votingPollInteractor.CreateVotingPoll(Form);

            return RedirectToPage("/Index");
        }
    }

    public class VotingPollVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}