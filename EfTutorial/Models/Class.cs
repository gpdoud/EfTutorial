using System;
using System.Collections.Generic;

#nullable disable

namespace EfTutorial.Models
{
    public partial class Class
    {
        public Class()
        {
            Assignments = new HashSet<Assignment>();
            MajorClasses = new HashSet<MajorClass>();
            StudentClasses = new HashSet<StudentClass>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public int Section { get; set; }
        public int? InstructorId { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<MajorClass> MajorClasses { get; set; }
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
    }
}
