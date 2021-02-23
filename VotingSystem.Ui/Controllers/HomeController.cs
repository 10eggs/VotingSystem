using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Models;

namespace VotingSystem.Ui.Controllers
{

    //controller name
    [Route("[controller]")]
    public class HomeController:Controller
    {
        private readonly IVotingPollFactory _pollFactory;

        public HomeController(IVotingPollFactory pollFactory)
        {
            _pollFactory = pollFactory;
        }

        //Even if we change route for this action it will return Index View
        [HttpGet]
        public IActionResult Index()
        {
            var info = new Info { Msg = "Hello" };
            return View(info);
        }

        [HttpPost]
        public VotingPoll Create(VotingPollFactory.Request request)
        {
            return _pollFactory.Create(request);
        }

        public class Info
        {   
            public string Msg { get; set; }
        }



        //action
        //To preserve original convention we can use
        //[Route("home")]
        //public string Index()
        //{
        //    return "Hello index page";
        //}


        //this attribute override orignal convention
        //If you preserve original pattern by using [Route] attribute, but want to use customize way of routing, use slash "/" before path
        [HttpGet("about-page")]
        public string about()
        {
            return "hello";
        }

        //[HttpGet("[action]/{word}")]
        //public string About(string word)
        //{
        //    return word;
        //}
    }
}
