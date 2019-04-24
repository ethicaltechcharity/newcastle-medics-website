using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Helpers.Interfaces;
using Web.Models;

namespace Web.Helpers.Classes
{
    public class SkillHelper : ISkillHelper
    {
        private readonly IPlayerHelper playerHelper;
        private readonly ApplicationDbContext dbContext;

        public SkillHelper(IPlayerHelper playerHelper, ApplicationDbContext dbContext)
        {
            this.playerHelper = playerHelper;
            this.dbContext = dbContext;
        }

        public TrialAssessmentViewModel BuildTrialAssessmentViewModel(TrialAssessmentViewModel model)
        {
            model = model ?? new TrialAssessmentViewModel
            {
                SelectedDrill = "",
                SelectedRating = null,
                DateOfAssessment = DateTime.Now
            };

            model.Drills = playerHelper.BuildDrillList();
            model.Ratings = playerHelper.BuildRatingList();

            return model;
        }

        public ViewAssessmentViewModel BuildViewAssessmentViewModel(ViewAssessmentViewModel model)
        {
            model.Assessees = new List<AssessmentSummaryViewModel>();
            model.Drills = new List<string>();

            var assessees = new List<AssessmentSummaryViewModel>();

            var assessmentsOnDate = dbContext
                .TrialAssessments.Where(a => DbFunctions.TruncateTime(a.DateOfAssessment) == model.Datefilter.Date).ToList();

            var groupedByNumber = assessmentsOnDate.GroupBy(a => a.AssignedNumber);

            model.TotalAssessees = groupedByNumber.Count();
            model.PageCount = model.TotalAssessees / 15 + 1;
            model.Genders = playerHelper.BuildGendersList();

            model.PageCount = Math.DivRem(model.TotalAssessees, 15, out int rem);

            if (rem != 0)
            {
                model.PageCount++;
            }

            foreach (var assessment in groupedByNumber)
            {
                var assessee = new AssessmentSummaryViewModel
                {
                    Number = assessment.Key,
                    TotalAssessments = assessment.Count(),
                    DrillAssessments = new Dictionary<string, DrillAssessment>(),
                };

                foreach (var drill in assessment.GroupBy(a => a.Drill))
                {
                    if (!model.Drills.Contains(drill.Key))
                    {
                        model.Drills.Add(drill.Key);
                    }

                    assessee.DrillAssessments.Add(drill.Key, new DrillAssessment
                    {
                        DrillName = drill.Key,
                        AverageRating = Math.Round(drill.Average(d => d.Rating), 3, MidpointRounding.AwayFromZero),
                        TotalRatings = drill.Count()
                    });
                }

                assessee.AverageRating = Math.Round(assessee.DrillAssessments
                    .Average(kvp => kvp.Value.AverageRating), 3, MidpointRounding.AwayFromZero);

                assessees.Add(assessee);
            }
            
            var returnedAssessees = assessees
                .OrderByDescending(assessee => assessee.AverageRating)
                .Skip((model.PageNumber - 1) * 15)
                .Take(15);
            
            foreach (var assessee in returnedAssessees) { 

                var assesseeNumberAssignment = dbContext.NumberAssignments
                    .Where(assignment => DbFunctions.TruncateTime(assignment.DateOfAssignment) == model.Datefilter.Date
                    && assignment.AssignedNumber == assessee.Number);
                
                if (assesseeNumberAssignment.Count() > 1)
                {
                    throw new Exception();
                }

                if (assesseeNumberAssignment.Count() == 0)
                {
                    assessee.Name = "Unknown";
                }
                else
                {
                    var memberIdentity = assesseeNumberAssignment.First().Identity;

                    assessee.MemberIdentityId = memberIdentity.MemberIdentityId;
                    assessee.Name = memberIdentity.FirstName + " " + memberIdentity.LastName;
                }

                model.Assessees.Add(assessee);
            }

            model.Assessees = model.Assessees.ToList();

            return model;
        }
    }
}