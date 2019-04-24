using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ContactViewModel
    {
        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Your Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Query Type")]
        public ICollection<System.Web.Mvc.SelectListItem> Categories { get; set; }
        [Required]
        [Display(Name = "Query Type")]
        public string SelectedCategory { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}