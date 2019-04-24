using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ExpressInterestViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public ICollection<System.Web.Mvc.SelectListItem> Genders { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string SelectedGender { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Day")]
        public ICollection<System.Web.Mvc.SelectListItem> Days { get; set; }
        [Required]
        [Display(Name = "Day")]
        public string SelectedDay { get; set; }

        [Display(Name = "Month")]
        public ICollection<System.Web.Mvc.SelectListItem> Months { get; set; }
        [Required]
        [Display(Name = "Month")]
        public string SelectedMonth { get; set; }

        [Display(Name = "Year")]
        public ICollection<System.Web.Mvc.SelectListItem> Years { get; set; }
        [Required]
        [Display(Name = "Year")]
        public string SelectedYear { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        
        [Display(Name = "Have you played before?")]
        public ICollection<System.Web.Mvc.SelectListItem> HaveYouPlayedBefore { get; set; }
        
        [Required]
        [Display(Name = "Played before")]
        public bool? PlayedBeforeAnswer { get; set; }

        [Display(Name = "Where on the pitch to you play?")]
        public ICollection<System.Web.Mvc.SelectListItem> Positions { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string SelectedPosition { get; set; }

        public bool Submitted { get; set; } = false;
    }

    public class InterestedViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [Display(Name = "Played Before")]
        public bool PlayedBefore { get; set; }

        public string Position { get; set; }
    }
    
    public class AssignNumberViewModel
    {
        [Required]
        [Display(Name = "Has player pre-registered through the expression of interest form?")]
        public bool PreRegistered { get; set; }

        public int? UserId { get; set; }

        [Required]
        [Display(Name = "Assign number to player")]
        public int AssignedNumber { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Number Assignment")]
        public DateTime DateOfAssignment { get; set; }

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

        [Display(Name = "Have you played before?")]
        public ICollection<System.Web.Mvc.SelectListItem> HaveYouPlayedBefore { get; set; }
        
        [Display(Name = "Played before")]
        public bool? PlayedBeforeAnswer { get; set; }

        [Display(Name = "Where on the pitch to you play?")]
        public ICollection<System.Web.Mvc.SelectListItem> Positions { get; set; }
        
        [Display(Name = "Position")]
        public string SelectedPosition { get; set; }

        public bool Submitted { get; set; } = false;
    }

}