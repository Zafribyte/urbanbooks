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
        public int ID { get; set; }
        public string Query { get; set; }
    }
}