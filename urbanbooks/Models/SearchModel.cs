using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class SearchModel
    {
        [Key]
        public int theKey { get; set; }
        public String Query { get; set; }
    }
}