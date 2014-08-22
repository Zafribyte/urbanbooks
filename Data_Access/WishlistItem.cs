using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class WishlistItem
    {
        [ScaffoldColumn(false)]
        public int WishlistID 
        { get; set; }
        [ScaffoldColumn(false)]
        public int WishlistItemID 
        { get; set; }
        [ScaffoldColumn(false)]
        public int ProductID 
        { get; set; }
        [Display(Name="Date Added")]
        public DateTime DateAdded { get; set; }//
    }
}
