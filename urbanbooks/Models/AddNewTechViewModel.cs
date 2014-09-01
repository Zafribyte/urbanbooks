
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Models
{
    public class AddNewTechViewModel
    {
        public List<SelectListItem> techCategories { get; set; }
        public List<SelectListItem> suppliers { get; set; }
        public List<SelectListItem> manufacturers { get; set; }
        public IEnumerable<TechCategory> techCategory { get; set; }
        public IEnumerable<Manufacturer> manufacturer { get; set; }
        public IEnumerable<Technology> tech { get; set; }
        public Technology techs { get; set; }
        public Manufacturer mans { get; set; }
    }
}