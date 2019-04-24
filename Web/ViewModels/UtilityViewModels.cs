using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SelectItemsViewModel
    {
        public SelectResultViewModel[] results { get; set; }

        public class SelectResultViewModel
        {
            public int id { get; set; }
            public string text { get; set; }
        }
    }
}