using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.DataModels
{
    public class ContactDataModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Category { get; set; }

        public string Message { get; set; }

        public DateTime DateTime { get; set; }
    }
}