using System.Web.Mvc;
using Web.Models;
using Microsoft.AspNet.Identity;
using Web.DataModels;
using Web.Helpers.Interfaces;
using System;
using System.Collections.Generic;

namespace Web.Controllers.Player
{
    [Authorize]
    public class SkillController : Controller
    {
        private readonly IPlayerHelper playerHelper;
        private readonly ISkillHelper skillHelper;
        private readonly ApplicationDbContext dbContext;

        public SkillController(
            IPlayerHelper playerHelper, 
            ISkillHelper skillHelper,
            ApplicationDbContext dbContext)
        {
            this.playerHelper = playerHelper;
            this.skillHelper = skillHelper;
            this.dbContext = dbContext;
        }

        public ActionResult Assess()
        {
            return View("~/Views/Player/Skill/Assess.cshtml");
        }

        [AllowAnonymous]
        public ActionResult TrialAssessment()
        {
            return View("~/Views/Player/Skill/TrialAssessment.cshtml",
                skillHelper.BuildTrialAssessmentViewModel(null));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult TrialAssessment(TrialAssessmentViewModel model)
        {
            TrialAssessmentViewModel returnModel = null;

            if (!ModelState.IsValid)
            {
                returnModel = skillHelper.BuildTrialAssessmentViewModel(model);

                return View("~/Views/Player/Skill/TrialAssessment.cshtml", returnModel);
            }

            var dataModel = new TrialAssessmentDataModel
            {
                AssignedNumber = (int) model.AssignedNumber,
                Drill = model.SelectedDrill,
                Rating = (int) model.SelectedRating,
                UserId = User.Identity.GetUserId(),
                DateOfAssessment = model.DateOfAssessment
            };

            dbContext.TrialAssessments.Add(dataModel);
            dbContext.SaveChanges();

            ViewBag.Saved = "Assessment Submitted!";

            returnModel = skillHelper.BuildTrialAssessmentViewModel(null);

            return View("~/Views/Player/Skill/TrialAssessment.cshtml",
                returnModel);
        }

        public ActionResult MatchAssessment()
        {
            return View("~/Views/Player/Skill/MatchAssessment.cshtml");
        }
        

        public ActionResult ViewAssessment(int pageNumber = 1)
        {
            return View("~/Views/Player/Skill/ViewAssessment.cshtml",
                skillHelper.BuildViewAssessmentViewModel(new ViewAssessmentViewModel
                {
                    PageNumber = pageNumber,
                    Datefilter = DateTime.Now.Date,
                    Assessees = new List<AssessmentSummaryViewModel>(),
                    Drills = new List<string>()
                }));
        }

        [HttpPost]
        public PartialViewResult ViewAssessment(ViewAssessmentViewModel model)
        {
            model = skillHelper.BuildViewAssessmentViewModel(model);

            return PartialView("~/Views/Player/Skill/_ResultsPartial.cshtml", model);
        }
    }
}