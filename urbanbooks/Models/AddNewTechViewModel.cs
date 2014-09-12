
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Models
{
    public class AddNewTechViewModel
    {
        [Key]
        public int ID { get; set; }
        [NotMapped]
        public List<SelectListItem> techCategories { get; set; }
        [NotMapped]
        public List<SelectListItem> suppliers { get; set; }
        [NotMapped]
        public List<SelectListItem> manufacturers { get; set; }
        [NotMapped]
        public IEnumerable<TechCategory> techCategory { get; set; }
        [NotMapped]
        public IEnumerable<Manufacturer> manufacturer { get; set; }
        [NotMapped]
        public IEnumerable<Technology> tech { get; set; }
        public Technology techs { get; set; }
        public Manufacturer mans { get; set; }
        public TechCategory Category { get; set; }
    }
}