using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class MemberIndexViewModel
    {
        public ICollection<MemberSummaryViewModel> Members { get; set; }

        [Display(Name = "Name")]
        public string NameFilter { get; set; }

        [Display(Name = "Gender")]
        public string GenderFilter { get; set; }
        [Display(Name = "Gender")]
        public ICollection<System.Web.Mvc.SelectListItem> Genders { get; set; }

        [Display(Name = "Role")]
        public string RoleFilter { get; set; }
        [Display(Name = "Role")]
        public ICollection<System.Web.Mvc.SelectListItem> Roles { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageCount { get; set; } = 1;

        public int TotalResults { get; set; }
    }

    public class MemberSummaryViewModel
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string Roles { get; set; }
    }

    public class MemberRegistrationViewModel
    {
        public bool PreRegistered { get; set; }
        public bool PlayedBefore { get; set; }
        public bool PlayedMedics { get; set; }
        public bool InterestedInUmpiring { get; set; }
        public bool UmpireQualified { get; set; }
        public bool KnowsUmpireNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public ICollection<System.Web.Mvc.SelectListItem> Genders { get; set; }
        
        [Display(Name = "Gender")]
        public string SelectedGender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Day")]
        public ICollection<System.Web.Mvc.SelectListItem> Days { get; set; }
        [Display(Name = "Day")]
        public string SelectedDay { get; set; }

        [Display(Name = "Month")]
        public ICollection<System.Web.Mvc.SelectListItem> Months { get; set; }
        [Display(Name = "Month")]
        public string SelectedMonth { get; set; }

        [Display(Name = "Year")]
        public ICollection<System.Web.Mvc.SelectListItem> Years { get; set; }
        [Display(Name = "Year")]
        public string SelectedYear { get; set; }
        
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Team")]
        public ICollection<System.Web.Mvc.SelectListItem> PreviousTeams { get; set; }
        [Display(Name = "Team")]
        public string SelectedPreviousTeam { get; set; }

        [Display(Name = "Team")]
        public ICollection<System.Web.Mvc.SelectListItem> CurrentTeams { get; set; }
        [Display(Name = "Team")]
        public string SelectedCurrentTeam { get; set; }

        [Display(Name = "Member Type")]
        public ICollection<System.Web.Mvc.SelectListItem> MemberTypes { get; set; }
        [Display(Name = "Member Type")]
        public string SelectedMemberType { get; set; }

        [Display(Name = "Shirt Number")]
        public int? ShirtNumber { get; set; }

        [Display(Name = "Previous Club")]
        public string PreviousClub { get; set; }

        [Display(Name = "Previous Team")]
        public string PreviousTeam { get; set; }

        [Display(Name = "Umpiring Qualification")]
        public ICollection<System.Web.Mvc.SelectListItem> UmpiringQualifications { get; set; }
        [Display(Name = "Umpiring Qualification")]
        public string SelectedUmpiringQualification { get; set; }

        [Display(Name = "Umpire Number")]
        public string UmpireNumber { get; set; }

        [Required]
        public bool AgreesCodeOfConduct { get; set; }

        [Required]
        public bool DataConsent { get; set; }

        public string SubscriptionFee { get; set; }

        public string PaymentReference { get; set; }
    }

    public class MemberDetailsViewModel
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool PlayedBefore { get; set; }

        public string Position { get; set; }

        public bool Permission { get; set; }
    }
}