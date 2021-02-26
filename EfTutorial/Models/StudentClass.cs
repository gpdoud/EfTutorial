using System;
using System.Collections.Generic;

#nullable disable

namespace EfTutorial.Models
{
    public partial class StudentClass
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public string ClassGradeValue { get; set; }

        public virtual Class Class { get; set; }
        public virtual ClassGrade ClassGradeValueNavigation { get; set; }
        public virtual Student Student { get; set; }
    }
}
