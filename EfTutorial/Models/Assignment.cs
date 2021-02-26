using System;
using System.Collections.Generic;

#nullable disable

namespace EfTutorial.Models
{
    public partial class Assignment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ClassId { get; set; }

        public virtual Class Class { get; set; }
    }
}
