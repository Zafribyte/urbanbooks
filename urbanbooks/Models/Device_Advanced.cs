using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class Device_Advanced
    {
        [ScaffoldColumn(false)]
        [Key]
        public int Id 
        { get; set; }
        [Display(Name="Device Category")]
        public bool Category 
        { get; set; }
        [Display(Name="Device Name")]
        public bool ModelName 
        { get; set; }
        [Display(Name="Model Number")]
        public bool ModelNumber 
        { get; set; }
        public bool Manufacturer 
        { get; set; }
        [Display(Name="Search Term")]
        public string Query 
        { get; set; }
        [Display(Name="From")]
        public double? RangeFrom 
        { get; set; }
        [Display(Name="To")]
        public double? RangeTo 
        { get; set; }

    }
}