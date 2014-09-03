using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Publisher : Book
    {
        [Key]
        public int PublisherID { get; set; }
        public string Name { get; set; }

    }
}
