using System;
using System.Collections.Generic;

#nullable disable

namespace EfTutorial.Models
{
    public partial class Major
    {
        public Major()
        {
            MajorClasses = new HashSet<MajorClass>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int MinSat { get; set; }

        public virtual ICollection<MajorClass> MajorClasses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
