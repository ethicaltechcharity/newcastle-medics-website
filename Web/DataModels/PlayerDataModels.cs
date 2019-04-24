using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.DataModels
{
    public class ExpressInterestDataModel
    {
        [Key]
        public int ExpressInterestId { get; set; }

        public bool PlayedBefore { get; set; }
        public string Position { get; set; }

        [ForeignKey("Identity")]
        public int IdentityId { get; set; }
        public virtual MemberIdentityDataModel Identity { get; set; }
    }

    public class NumberAssignmentDataModel
    {
        [Key]
        public int NumberAssignmentId { get; set; }

        [ForeignKey("Identity")]
        public int IdentityId { get; set; }
        public virtual MemberIdentityDataModel Identity { get; set; }

        public int AssignedNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfAssignment { get; set; }
    }

    public class TrialAssessmentDataModel
    {
        [Key]
        public int TrialAssessmentId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int AssignedNumber { get; set; }

        public string Drill { get; set; }

        public int Rating { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfAssessment { get; set; }
    }

    public class PlayingHistoryDataModel
    {
        [Key]
        public int PlayingHistoryId { get; set; }

        [ForeignKey("Identity")]
        public int IdentityId { get; set; }
        public virtual MemberIdentityDataModel Identity { get; set; }

        public bool NewPlayer { get; set; }

        public string Team { get; set; }

        public string Club { get; set; }

        public string TimeFrame { get; set; }
    }

    public class PlayingShirtDataModel
    {
        [Key]
        public int PlayingShirtId { get; set; }

        public int PlayingShirtNumber { get; set; }

        [ForeignKey("Identity")]
        public int IdentityId { get; set; }
        public virtual MemberIdentityDataModel Identity { get; set; }
    }
}