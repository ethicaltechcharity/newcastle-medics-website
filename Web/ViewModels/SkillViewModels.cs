using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TrialAssessmentViewModel
    {
        [Required]
        [Display(Name = "Number")]
        public int? AssignedNumber { get; set; }

        [Display(Name = "Drill")]
        public ICollection<System.Web.Mvc.SelectListItem> Drills { get; set; }

        [Required]
        [Display(Name = "Drill")]
        public string SelectedDrill { get; set; }

        [Display(Name = "Rating")]
        public ICollection<System.Web.Mvc.SelectListItem> Ratings { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int? SelectedRating { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Assessment")]
        public DateTime DateOfAssessment { get; set; }
    }

    public class ViewAssessmentViewModel
    {
        public ICollection<AssessmentSummaryViewModel> Assessees { get; set; }

        public List<string> Drills { get; set; }

        [Display(Name = "Name")]
        public string NameFilter { get; set; }

        [Display(Name = "Date of Assessment")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Datefilter { get; set; }

        [Display(Name = "Gender")]
        public string GenderFilter { get; set; }
        [Display(Name = "Gender")]
        public ICollection<System.Web.Mvc.SelectListItem> Genders { get; set; }

        [Display(Name = "Order")]
        public string OrderBy { get; set; }
        [Display(Name = "Order")]
        public ICollection<System.Web.Mvc.SelectListItem> OrderByOptions { get; set; }

        public int TotalAssessees { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageCount { get; set; } = 1;
    }

    public class AssessmentSummaryViewModel
    {
        public string Name { get; set; }

        public int MemberIdentityId { get; set; }

        public int Number { get; set; }

        public double AverageRating { get; set; }

        public Dictionary<string, DrillAssessment> DrillAssessments { get; set; }

        public int TotalAssessments { get; set; }
    }

    public class DrillAssessment
    {
        public string DrillName { get; set; }

        public double AverageRating { get; set; }

        public int TotalRatings { get; set; }
    }
}